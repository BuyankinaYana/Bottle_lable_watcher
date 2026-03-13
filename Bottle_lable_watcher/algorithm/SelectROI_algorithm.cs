using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static OpenCvSharp.ML.DTrees;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Bottle_lable_watcher.algorithm
{
    internal class SelectROI_algorithm
    {
        //Выделение прямоугольником
        public void Rectangle_selection()
        {
            //------------------------------- Считывание изображения, получение основных параметров ---------------------------------------------
            Mat img_orig = Cv2.ImRead("1.jpg");
            Mat img_draw = new Mat();
            img_draw = img_orig;
            float max_height = 700;
            int h_orig = img_orig.Height;
            int w_orig = img_orig.Width;
            double diagonal = Math.Sqrt(h_orig * h_orig + w_orig * w_orig);

            float scale_w = max_height / w_orig;
            float scale_h = max_height / h_orig;
            float scale = Math.Min(scale_w, scale_h);
            Mat display_img = new Mat();
            Cv2.Resize(img_orig, display_img, new OpenCvSharp.Size(w_orig * scale, h_orig * scale));     //изменение масштаба (для удобства)

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

            double etalon_center_x = x_crop + w_crop / 2;
            double etalon_center_y = y_crop + h_crop / 2;
            double min_area_lable = 0.4 * w_crop * h_crop;
            double max_area_lable = 0.98 * w_crop * h_crop;

            Mat roi_crop = img_orig[new Rect(x_crop, y_crop, w_crop, h_crop)];
            //Cv2.ImShow("Область интереса", roi_crop);
            //Mat roi_rgb = img_orig[new Rect(x_crop, y_crop, w_crop, h_crop)];

            //--------------------------------- Преобразование цвета, шумоподавление, кэнни, морфология -------------------------------
            Cv2.CvtColor(roi_crop, roi_crop, ColorConversionCodes.BGR2GRAY);
            //Cv2.MedianBlur(roi_crop, roi_crop, 9);
            //Cv2.FastNlMeansDenoising(roi_crop, roi_crop, 17, 13, 25);
            Cv2.Canny(roi_crop, roi_crop, 50, 150);
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Mat kernel_2 = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(8, 8));
            Cv2.MorphologyEx(roi_crop, roi_crop, MorphTypes.Close, kernel, null, 5);
            Cv2.MorphologyEx(roi_crop, roi_crop, MorphTypes.Dilate, kernel_2, null, 3);

            //------------------------------ Выделение контура, фильтрация по площади и заполненности сегментация ----------------------------
            OpenCvSharp.Point[][] points;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(roi_crop, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int num_labels = Cv2.ConnectedComponentsWithStats(roi_crop, labels, stats, centroids);

            int height, width, area;
            float extension;
            for (int i = 1; i < num_labels; i++)
            {
                height = stats.At<int>(i, 3);
                width = stats.At<int>(i, 2);
                area = height * width;
                extension = stats.At<int>(i, 4);
                extension = extension / area * 100F;
                area = height * width;
                Mat mask = new Mat();

                Cv2.Compare(labels, i, mask, CmpTypes.EQ);
                if ((area > min_area_lable) && (extension > 5))
                {
                    Cv2.FindContours(mask, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    OpenCvSharp.Point[] largestContour = points.OrderByDescending(c => Cv2.ContourArea(c)).First();

                    Rect boundingRect = Cv2.BoundingRect(largestContour);
                    int x_rect = boundingRect.X;
                    int y_rect = boundingRect.Y;
                    int w_rect = boundingRect.Width;
                    int h_rect = boundingRect.Height;

                    double lable_center_x = x_rect + x_crop + w_rect / 2;
                    double lable_center_y = y_rect + y_crop + h_rect / 2;
                    double error_x = etalon_center_x - lable_center_x;
                    double error_y = etalon_center_y - lable_center_y;

                    double Delta = Math.Sqrt(error_x * error_x + error_y * error_y);
                    double Error_Rate = Delta / diagonal * 100;

                    Cv2.Rectangle(img_draw, new Rect(x_rect + x_crop, y_rect + y_crop, w_rect, h_rect), new Scalar(0, 255, 0), 5);
                }
            }
            Cv2.ImShow("Найдена этикетка", img_draw);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

        // Выделение по точкам
        public void Selection_by_points()
        {
            List<OpenCvSharp.Point> points_draw = new List<OpenCvSharp.Point>();
            List<OpenCvSharp.Point> point_orig = new List<OpenCvSharp.Point>();
            List<OpenCvSharp.Point> point_orig_error = new List<OpenCvSharp.Point>();

            //------------------------------- Считывание изображения, получение основных параметров ---------------------------------------------
            Mat img_orig = Cv2.ImRead("2.jpg");
            Mat img_draw = new Mat();
            img_draw = img_orig;
            float max_height = 700;
            int h_orig = img_orig.Height;
            int w_orig = img_orig.Width;
            double diagonal = Math.Sqrt(h_orig * h_orig + w_orig * w_orig);

            float scale_w = max_height / w_orig;
            float scale_h = max_height / h_orig;
            float scale = Math.Min(scale_w, scale_h);
            Mat display_img = new Mat();
            Cv2.Resize(img_orig, display_img, new OpenCvSharp.Size(w_orig * scale, h_orig * scale));     //изменение масштаба (для удобства)

            //--------------------------------- Рисование кривого контура с точками и линиями -----------------------------
            Cv2.NamedWindow("Выберите область интереса");
            Cv2.SetMouseCallback("Выберите область интереса", DrawRoi);

            while (true)
            {
                if (points_draw.Count > 0)
                {
                    for (int i = 0; i < points_draw.Count; i++)
                    {
                        Cv2.Circle(display_img, points_draw[i], 3, Scalar.Red);
                        if (i > 0)
                        {
                            Cv2.Line(display_img, points_draw[i - 1], points_draw[i], Scalar.Red, 2);
                        }
                    }
                }
                Cv2.ImShow("Выберите область интереса", display_img);
                int key = Cv2.WaitKey(1);
                if ((key == 13) && (points_draw.Count > 2))
                {
                    break;
                }
            }

            Cv2.DestroyWindow("Выберите область интереса");
            float error = 0.1F;

            //----------------------------- Пересчёт координат и обрезка области интерес с помощью маски -----------------------
            double centerX = point_orig.Select(p => p.X).Average();
            double centerY = point_orig.Select(p => p.Y).Average();

            foreach (var i in point_orig)
            {
                int x_error = (int)(centerX + (i.X - centerX) * (1 + error));
                int y_error = (int)(centerY + (i.Y - centerY) * (1 + error));
                point_orig_error.Add(new OpenCvSharp.Point(x_error, y_error));
            }

            Mat mask = new Mat(img_orig.Size(), MatType.CV_8UC1, Scalar.All(0));
            OpenCvSharp.Point[][] roi_corners = new OpenCvSharp.Point[][] { point_orig_error.ToArray() };
            Cv2.FillPoly(mask, roi_corners, Scalar.All(255));
            Mat res = new Mat();
            Cv2.BitwiseAnd(img_orig, img_orig, res, mask);
            Rect rect = Cv2.BoundingRect(point_orig_error);
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }
            Mat roi_crop = new Mat(res, rect).Clone();
            double min_area_lable = 0.1 * roi_crop.Width * roi_crop.Height;

            //--------------------------------- Преобразование цвета, шумоподавление, кэнни, морфология -------------------------------
            Cv2.CvtColor(roi_crop, roi_crop, ColorConversionCodes.BGR2GRAY);
            Cv2.MedianBlur(roi_crop, roi_crop, 9);
            //Cv2.FastNlMeansDenoising(roi_crop, roi_crop, 17, 13, 25);
            Cv2.Canny(roi_crop, roi_crop, 50, 150);
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Mat kernel_2 = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(2, 2));
            //Cv2.MorphologyEx(roi_crop, roi_crop, MorphTypes.Close, kernel, null, 5);
            Cv2.MorphologyEx(roi_crop, roi_crop, MorphTypes.Dilate, kernel_2, null, 1);

            //------------------------------ Выделение контура, фильтрация по площади и заполненности сегментация ----------------------------
            OpenCvSharp.Point[][] points;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(roi_crop, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int num_labels = Cv2.ConnectedComponentsWithStats(roi_crop, labels, stats, centroids);

            int height, width, area;
            float extension;
            for (int i = 1; i < num_labels; i++)
            {
                height = stats.At<int>(i, 3);
                width = stats.At<int>(i, 2);
                area = height * width;
                extension = stats.At<int>(i, 4);
                extension = extension / area * 100F;
                area = height * width;
                Mat mask_crop = new Mat();

                Cv2.Compare(labels, i, mask, CmpTypes.EQ);
                if ((area > min_area_lable) && (extension > 2))
                {
                    Cv2.FindContours(mask, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    OpenCvSharp.Point[] largestContour = points.OrderByDescending(c => Cv2.ContourArea(c)).First();

                    Rect boundingRect = Cv2.BoundingRect(largestContour);
                    int x_rect = boundingRect.X;
                    int y_rect = boundingRect.Y;
                    int w_rect = boundingRect.Width;
                    int h_rect = boundingRect.Height;

                    double lable_center_x = x_rect + rect.X + w_rect / 2;
                    double lable_center_y = y_rect + rect.Y + h_rect / 2;
                    double error_x = (rect.X + rect.Width / 2) - lable_center_x;
                    double error_y = (rect.Y + rect.Height / 2) - lable_center_y;

                    double Delta = Math.Sqrt(error_x * error_x + error_y * error_y);
                    double Error_Rate = Delta / diagonal * 100;

                    Cv2.Rectangle(img_draw, new Rect(x_rect + rect.X, y_rect + rect.Y, w_rect, h_rect), new Scalar(0, 255, 0), 5);
                    
                }
            }

            //Cv2.ImShow("Морфология", roi_crop);
            Cv2.ImShow("Найдена этикетка", img_draw);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();

            //Функция для рисования кривого контура
            void DrawRoi (MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
            {
                if (@event == MouseEventTypes.LButtonDown)
                {
                    points_draw.Add(new OpenCvSharp.Point(x, y));
                    point_orig.Add(new OpenCvSharp.Point((int)(x / scale), (int)(y / scale)));
                }
            }
        }
    }
}
