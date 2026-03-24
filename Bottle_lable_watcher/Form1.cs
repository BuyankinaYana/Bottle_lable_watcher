using Bottle_lable_watcher.Camera;
using Bottle_lable_watcher.Configuration_algorithm;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Media.Media3D;

namespace Bottle_lable_watcher
{
    public partial class Form1 : Form
    {
        public HikCamera camera_1, camera_2;

        private int number_procedure;
        private bool isCameraRunning = false;
        private bool Crop_flag_change = false;
        private bool Point_flag_change = false;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            combobox_method_detect.SelectedIndex = 0;

            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox1.Paint += PictureBox1_Paint;     //Отрисовка рамки

            n_error.ValueChanged += Numeric_changed;
            n_canny_down.ValueChanged += Numeric_changed;
            n_canny_up.ValueChanged += Numeric_changed;
            n_kernel.ValueChanged += Numeric_changed;
            n_iteration.ValueChanged += Numeric_changed;
            n_min_area.ValueChanged += Numeric_changed;
            n_max_area.ValueChanged += Numeric_changed;
            n_extension.ValueChanged += Numeric_changed;
            combobox_method_detect.SelectedValueChanged += Numeric_changed;

        }

        /*----------------------------------------------- ГЛАВНАЯ -------------------------------------------------------*/
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += Form1_FormClosing;
        }

        private void b_camera1_Click(object sender, EventArgs e)
        {
            string serialNumber = "Vir74501888";
            camera_1 = new HikCamera(serialNumber);
            if (camera_1.Open() == true)
            {
                camera_1.StartStream();
                camera_1.SendImage += Image_camera_received;
                Thread.Sleep(1000);
                camera_1.EndStream();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к камере.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isCameraRunning = true;
            pictureBox1.Invalidate();
        }

        private void b_camera2_Click(object sender, EventArgs e)
        {
            string serialNumber = "Vir74502060";
            camera_2 = new HikCamera(serialNumber);
            if (camera_2.Open() == true)
            {
                camera_2.StartStream();
                camera_2.SendImage += Image_camera_received;
                Thread.Sleep(1000);
                camera_2.EndStream();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к камере.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isCameraRunning = true;
            pictureBox2.Invalidate();
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            camera_1.StartStream();
            camera_1.SendImage += Image_camera_received;
            isCameraRunning = true;
            pictureBox1.Invalidate();

            camera_2.StartStream();
            camera_2.SendImage += Image_camera_received_2;
        }

        private void Start_stream_working(int id_camera)
        {
            if (id_camera == 1)
            {
                camera_1.StartStream();
                camera_1.SendImage += Image_camera_received;
                isCameraRunning = true;
                pictureBox1.Invalidate();
            }
            else
            {
                camera_2.StartStream();
                camera_2.SendImage += Image_camera_received_2;
                isCameraRunning = true;
                pictureBox2.Invalidate();   //Сделать переключение рамки между камерами
            }
        }

        private void Image_camera_received(Mat picture)
        {
            Bitmap bitmap = null;
            bitmap = picture.ToBitmap();
            pictureBox1.Image = bitmap;
        }

        private void Image_camera_received_2(Mat picture)
        {
            Bitmap bitmap = null;
            bitmap = picture.ToBitmap();
            pictureBox2.Image = bitmap;
        }

        private void b_stop_Click(object sender, EventArgs e)
        {
            camera_1.EndStream();
            isCameraRunning = false;

            camera_2.EndStream();
            isCameraRunning = false;

            pictureBox1.Invalidate();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null || !isCameraRunning) return;

            using (var pen = new Pen(Color.Red, 10)) // Жирная красная рамка (6 пикселей)
            {
                // Рисуем прямоугольник по краям PictureBox (с учётом толщины пера)
                e.Graphics.DrawRectangle(pen, 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы действительно хотите закрыть приложение?",
                "Подтверждение закрытия",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                // Отменяем закрытие формы
                e.Cancel = true;
                return;
            }
            camera_1.EndStream();
            camera_2.EndStream();
            camera_1.Close();
            camera_2.Close();
            Form form2 = new StartWin();
            form2.Show();
        }
        /*--------------------------------------------- ОТЛАДКА --------------------------------------------------------*/
        private void b_roi_kv_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Нет данных с камеры! Проверьте подключение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pb_crop.Image != null)
            {
                Crop_flag_change = true;
            }
            number_procedure = 1;
            Bitmap crop = null;
            crop = Configuration_algorithm.Crop_image_library.Rectangle_selection_roi_crop(new Bitmap(pictureBox1.Image), (int)n_error.Value, Crop_flag_change);
            if (crop != null)
            {
                pb_crop.Image = crop;
            }
        }

        private void b_roi_point_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Нет данных с камеры! Проверьте подключение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pb_crop.Image != null)
            {
                Point_flag_change = true;
            }
            number_procedure = 2;
            Bitmap crop = null;
            crop = Configuration_algorithm.Crop_image_library.Choosen_selection_roi_crop(new Bitmap(pictureBox1.Image), (int)n_error.Value, Point_flag_change);
            if (crop != null)
            {
                pb_crop.Image = crop;
            }
        }

        //При изменении погрешности
        private void Numeric_changed(object sender, EventArgs e)
        {
            if (pb_crop.Image == null)
            {
                return;
            }

            Bitmap crop = null;
            if (number_procedure == 1)
            {
                Crop_flag_change = true;
                crop = Configuration_algorithm.Crop_image_library.Rectangle_selection_roi_crop(new Bitmap(pictureBox1.Image), (int)n_error.Value, Crop_flag_change);
            }
            if (number_procedure == 2)
            {
                Point_flag_change = true;
                crop = Configuration_algorithm.Crop_image_library.Choosen_selection_roi_crop(new Bitmap(pictureBox1.Image), (int)n_error.Value, Point_flag_change);
            }

            if (crop != null)
            {
                pb_crop.Image = crop;
            }
            algorithm_segment_lable();

        }

        //Функция сброса
        private void b_stop_alg_Click(object sender, EventArgs e)
        {
            pb_crop.Image = null;
            pb_contour.Image = null;
            pb_morph.Image = null;
            pb_lable.Image = null;
            Crop_flag_change = false;
            Point_flag_change = false;
        }

        //Функция просмотра (основная на вкладке)
        private void b_start_alg_Click(object sender, EventArgs e)
        {
            if (pb_crop.Image != null)
            {
                algorithm_segment_lable();
            }
            else
            {
                MessageBox.Show("Не выбрана зона поиска!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        //Функция обработки изображения
        private void algorithm_segment_lable()
        {
            Bitmap contour_image = null;

            contour_image = Segmentation_lable.Contour_lable(new Bitmap(pb_crop.Image), (int)n_canny_down.Value, (int)n_canny_up.Value, number_procedure);
            pb_contour.Image = contour_image;

            Bitmap morph_image = null;
            morph_image = Segmentation_lable.Morph_lable(new Bitmap(pb_contour.Image), (int)n_kernel.Value, (int)n_iteration.Value);
            pb_morph.Image = morph_image;

            float h = contour_image.Height;
            float w = contour_image.Width;
            float min_area = (int)n_min_area.Value / 100F * h * w;
            float max_area = (int)n_max_area.Value / 100F * h * w;
            string method = combobox_method_detect.Text;
            bool flag;
            if (method =="Выделение прямоугольником")
            {
                flag = false;
            }
            else { flag = true; }
            Bitmap detect_image = null;
            detect_image = Segmentation_lable.Detect_lable(new Bitmap(pb_morph.Image), new Bitmap(pb_crop.Image), min_area, max_area, (int)n_extension.Value, flag);
            pb_lable.Image = detect_image;
        }
    }
}
