using OpenCvSharp;
using OpenCvSharp.Features2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Bottle_lable_watcher.algorithm
{
    internal class SIFT_features
    {
        //Нахождение этикетки с помощью особых точек. Алгоритм SIFT
        public void SIFT_features_with_crop()
        {
            Mat etalon = Cv2.ImRead("3.jpg");
            Mat defect = Cv2.ImRead("3_defect.jpg");
            float max_height = 700;
            int h_orig = etalon.Height;
            int w_orig = etalon.Width;
            double diagonal = Math.Sqrt(h_orig * h_orig + w_orig * w_orig);

            float scale_w = max_height / w_orig;
            float scale_h = max_height / h_orig;
            float scale = Math.Min(scale_w, scale_h);
            Mat display_img = new Mat();
            Cv2.Resize(etalon, display_img, new OpenCvSharp.Size(w_orig * scale, h_orig * scale));     //изменение масштаба (для удобства)

            //Допустим, что пользователь выбрал прямоугольную область
            Rect roi = Cv2.SelectROI("Выберите область интереса", display_img);
            Cv2.DestroyWindow("Выберите область интереса");

            int x = roi.Left;
            int y = roi.Top;
            int w = roi.Width;
            int h = roi.Height;

            if (w == 0 || h == 0)       //если пользователь ничего не выбрал
            {
                return;
            }
            float error = 0.1F;
            //------------------------------------ Пересчёт координат ---------------------------------------
            int x_crop = (int)((x / scale) - (w / scale) * error);
            int y_crop = (int)((y / scale) - (h / scale) * error);
            int w_crop = (int)((w / scale) * (2 * error + 1));
            int h_crop = (int)((h / scale) * (2 * error + 1));

            Mat etalon_crop = etalon[new Rect(x_crop, y_crop, w_crop, h_crop)];
            Mat defect_crop = defect[new Rect(x_crop, y_crop, w_crop, h_crop)];

            //--------------------------------------- Алгоритм SIFT
            Cv2.CvtColor(etalon_crop, etalon_crop, ColorConversionCodes.BGR2GRAY);
            Cv2.CvtColor(defect_crop, defect_crop, ColorConversionCodes.BGR2GRAY);

            SIFT sift = SIFT.Create();

            KeyPoint[] keypoint_1;
            KeyPoint[] keypoint_2;
            Mat des_1 = new Mat();
            Mat des_2 = new Mat();

            sift.DetectAndCompute(etalon_crop, null, out keypoint_1, des_1);
            sift.DetectAndCompute(defect_crop, null, out keypoint_2, des_2);

            using var matcher = new BFMatcher(NormTypes.L2);
            DMatch[] matches = matcher.Match(des_1, des_2);

            //Отбор по правилу Лоу
            double minDistance = matches.Min(m => m.Distance);
            var goodMatches = matches.Where(m => m.Distance <= Math.Max(5 * minDistance, 0.02f)).ToArray();

            if (goodMatches.Length > 4) //Требование к алгоритму точек SIFT
            {
                double delta_x = 0;
                double delta_y = 0;

                foreach (var match in goodMatches)
                {
                    Point2f pt1 = keypoint_1[match.QueryIdx].Pt;
                    Point2f pt2 = keypoint_2[match.TrainIdx].Pt;

                    delta_x += (pt2.X - pt1.X);
                    delta_y += (pt2.Y - pt1.Y);
                }

                double error_x = delta_x / goodMatches.Length;
                double error_y = delta_y / goodMatches.Length;

                //Console.WriteLine($"Найдено совпадений: {goodMatches.Length}");
                //Console.WriteLine($"Среднее смещение по X: {error_x:F2} px");
                //Console.WriteLine($"Среднее смещение по Y: {error_y:F2} px");

                // Визуализация совпадений
                Mat view = new Mat();
                Cv2.DrawMatches(etalon_crop, keypoint_1, defect_crop, keypoint_2, goodMatches, view);
                Cv2.ImShow("SIFT Matches", view);
                Cv2.WaitKey(0);
            }
            else
            {
                //Console.WriteLine("Недостаточно совпадений для вычисления смещения.");
                return;
            }
        }
    }
}
