using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Bottle_lable_watcher.Configuration_algorithm
{
    internal class Segmentation_lable
    {
        //Функция для выделения контура
        public static Bitmap Contour_lable(Bitmap image_orig_form, int down_threshold, int up_threshold, int procedure_id)
        {
            Mat img_orig = BitmapConverter.ToMat(image_orig_form);
            Cv2.CvtColor(img_orig, img_orig, ColorConversionCodes.BGR2GRAY);
            Cv2.MedianBlur(img_orig, img_orig, 9);
            Cv2.Canny(img_orig, img_orig, down_threshold, up_threshold);
            return BitmapConverter.ToBitmap(img_orig);
        }

        //Функция для морфологии
        public static Bitmap Morph_lable(Bitmap image_orig_form, int size_kernel, int num_iterations)
        {
            Mat img_orig = BitmapConverter.ToMat(image_orig_form);
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(size_kernel, size_kernel));
            Cv2.MorphologyEx(img_orig, img_orig, MorphTypes.Dilate, kernel, null, num_iterations);
            return BitmapConverter.ToBitmap(img_orig);
        }

        //Функция для нахождения объекта интереса
        public static Bitmap Detect_lable(Bitmap image_orig_form, Bitmap image_draw, float min_area, float max_area, int extension, bool flag)
        {
            Mat img_orig = BitmapConverter.ToMat(image_orig_form);
            Mat img_draw = BitmapConverter.ToMat(image_draw);

            Cv2.CvtColor(img_orig, img_orig, ColorConversionCodes.BGR2GRAY);
            OpenCvSharp.Point[][] points;
            HierarchyIndex[] hierarchies;
            //Cv2.FindContours(img_orig, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            Mat labels = new Mat();
            Mat stats = new Mat();
            Mat centroids = new Mat();
            int num_labels = Cv2.ConnectedComponentsWithStats(img_orig, labels, stats, centroids);

            int height, width, area;
            float ext;
            for (int i = 1; i < num_labels; i++)
            {
                height = stats.At<int>(i, 3);
                width = stats.At<int>(i, 2);
                area = height * width;
                ext = stats.At<int>(i, 4);
                ext = ext / area * 100F;
                Mat mask = new Mat();

                Cv2.Compare(labels, i, mask, CmpTypes.EQ);
                if ((area > min_area) && (area < max_area) && (ext > extension))
                {
                    Cv2.FindContours(mask, out points, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                    points.OrderByDescending(c => Cv2.ContourArea(c)).First();

                    if (flag==false)
                    {
                        Rect boundingRect = Cv2.BoundingRect(points[0]);
                        int x_rect = boundingRect.X;
                        int y_rect = boundingRect.Y;
                        int w_rect = boundingRect.Width;
                        int h_rect = boundingRect.Height;

                        Cv2.Rectangle(img_draw, new Rect(x_rect, y_rect, w_rect, h_rect), new Scalar(0, 255, 0), 9);
                        Cv2.CvtColor(img_draw, img_draw, ColorConversionCodes.BGR2RGB);
                        Cv2.CvtColor(img_draw, img_draw, ColorConversionCodes.RGB2BGR);
                    }
                    else
                    {
                        Cv2.DrawContours(img_draw, points, 0, new Scalar(0, 255, 0), thickness: 9);
                        Cv2.CvtColor(img_draw, img_draw, ColorConversionCodes.BGR2RGB);
                        Cv2.CvtColor(img_draw, img_draw, ColorConversionCodes.RGB2BGR);
                    }
                    
                }
            }
            return BitmapConverter.ToBitmap(img_draw);
        }
    }
}
