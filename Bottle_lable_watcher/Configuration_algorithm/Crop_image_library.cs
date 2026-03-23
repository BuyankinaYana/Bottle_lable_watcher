using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bottle_lable_watcher.Configuration_algorithm
{
    internal class Crop_image_library
    {
        private static Rect roi_save = new Rect(0,0,0,0);
        private static float scale;
        private static List<OpenCvSharp.Point> point_orig = new List<OpenCvSharp.Point>();
        private static List<OpenCvSharp.Point> points_draw = new List<OpenCvSharp.Point>();
        private static double centerX;
        private static double centerY;

        //Функция для выделения прямоугольной области
        public static Bitmap Rectangle_selection_roi_crop(Bitmap image_orig_form, int crop_procent_error, bool flag)
        {
            Mat img_orig = BitmapConverter.ToMat(image_orig_form);
            if (flag==false)
            {
                float max_height = 900;
                int h_orig = img_orig.Height;
                int w_orig = img_orig.Width;
                double diagonal = Math.Sqrt(h_orig * h_orig + w_orig * w_orig);

                float scale_w = max_height / w_orig;
                float scale_h = max_height / h_orig;
                scale = Math.Min(scale_w, scale_h);
                Mat display_img = new Mat();
                Cv2.Resize(img_orig, display_img, new OpenCvSharp.Size(w_orig * scale, h_orig * scale));     //изменение масштаба (для удобства)

                Rect roi = Cv2.SelectROI("Выберите область интереса", display_img);
                roi_save = roi;
                Cv2.DestroyWindow("Выберите область интереса");
            }
           
            int x = roi_save.Left;
            int y = roi_save.Top;
            int w = roi_save.Width;
            int h = roi_save.Height;

            if (w == 0 || h == 0)       //если пользователь ничего не выбрал
            {
                MessageBox.Show("Не выбрана зона поиска!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return image_orig_form;
            }
            float error = crop_procent_error/100F;
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
            return BitmapConverter.ToBitmap(roi_crop);
        }

        //Функция выделения произовльной области
        public static Bitmap Choosen_selection_roi_crop(Bitmap image_orig_form, int crop_procent_error, bool flag)
        {
            Mat img_orig = BitmapConverter.ToMat(image_orig_form);
            if (flag == false)
            {
                point_orig.Clear();
                float max_height = 900;
                int h_orig = img_orig.Height;
                int w_orig = img_orig.Width;
                double diagonal = Math.Sqrt(h_orig * h_orig + w_orig * w_orig);

                float scale_w = max_height / w_orig;
                float scale_h = max_height / h_orig;
                scale = Math.Min(scale_w, scale_h);
                Mat display_img = new Mat();
                Cv2.Resize(img_orig, display_img, new OpenCvSharp.Size(w_orig * scale, h_orig * scale));

                //Рисование кривого контура
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

                centerX = point_orig.Select(p => p.X).Average();
                centerY = point_orig.Select(p => p.Y).Average();
            }

            List<OpenCvSharp.Point> point_orig_error = new List<OpenCvSharp.Point>();
            foreach (var i in point_orig)
            {
                int x_error = (int)(centerX + (i.X - centerX) * (1 + crop_procent_error/100F));
                int y_error = (int)(centerY + (i.Y - centerY) * (1 + crop_procent_error/100F));
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
                MessageBox.Show("Не выбрана зона поиска!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return image_orig_form;
            }
            Mat roi_crop = new Mat(res, rect).Clone();
            points_draw.Clear();
            

            return BitmapConverter.ToBitmap(roi_crop);

            //Функция для рисования кривого контура
            void DrawRoi(MouseEventTypes @event, int x, int y, MouseEventFlags flags, IntPtr userData)
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
