using Bottle_lable_watcher.Camera;
using Bottle_lable_watcher.Configuration_algorithm;
using Bottle_lable_watcher.Modules;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Media.Media3D;

namespace Bottle_lable_watcher
{
    public partial class Form1 : Form
    {
        public List<HikCamera> CameraList { get; set; }
        public List<string> StatusList { get; set; }

        private int number_procedure, number_chosen_camera;
        private PictureBox selected_pb = null;
        private PictureBox camera_crop_chose = null;

        private bool isCameraRunning_1 = false;
        private bool isCameraRunning_2 = false;
        private bool chose_camera1 = false;
        private bool chose_camera2 = false;
        private bool Crop_flag_change = false;
        private bool Point_flag_change = false;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            combobox_method_detect.SelectedIndex = 0;

            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox1.Paint += PictureBox1_Paint_Ramka;     //Отрисовка рамки

            pictureBox2.BorderStyle = BorderStyle.None;
            pictureBox2.Paint += PictureBox1_Paint_Ramka;     //Отрисовка рамки

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

        private void Form1_Load(object sender, EventArgs e)
        {
            l_camera1_status.Text = StatusList[0];
            l_camera2_status.Text = StatusList[1];
            l_modul_io_status.Text = StatusList[2];
            Log_text_box();
            this.FormClosing += Form1_FormClosing;
        }

        /*----------------------- Дополнительные функции для рисования, текстов, флгов и т.д. ---------------------------*/
        //Выбор камеры
        private void UpdateSelection(PictureBox newSelection)
        {
            selected_pb = newSelection;
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
        }
        //Вывод нового лога в textbox
        private void Log_text_box()
        {
            textBox1.Text = Logger_class.Return_message_log();
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
        }
        //Получение и вывод изображений с камеры на форму
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
        //Отрисовка рамки
        private void PictureBox1_Paint_Ramka(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb == selected_pb)
            {
                using (var pen = new Pen(Color.Red, 10))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, pb.Width - 1, pb.Height - 1);
                }
            }
        }
        //Выбор камеры для отладки
        private void Chose_camera_otladka()
        {
            string camera = comboBox2.Text;
            if (camera == "Камера 1")
            {
                camera_crop_chose = pictureBox1;
            }
            else
            {
                camera_crop_chose = pictureBox2;
            }
        }
        /*----------------------------------------------- ГЛАВНАЯ -------------------------------------------------------*/
        //Кнопка камера 1
        private void b_camera1_Click(object sender, EventArgs e)
        {
            number_chosen_camera = 1;
            UpdateSelection(pictureBox1);
        }
        //Кнопка камера 2
        private void b_camera2_Click(object sender, EventArgs e)
        {
            number_chosen_camera = 2;
            UpdateSelection(pictureBox2);
        }
        //Начало трансляции
        private void b_start_Click(object sender, EventArgs e)
        {
            if (number_chosen_camera == 1)
            {
                CameraList[0].StartStream();
                CameraList[0].SendImage += Image_camera_received;
                isCameraRunning_1 = true;
                Logger_class.LogInfo("Трансляция с камеры 1 запущена");
                Log_text_box();
            }
            else
            {
                CameraList[1].StartStream();
                CameraList[1].SendImage += Image_camera_received_2;
                isCameraRunning_2 = true;
                Logger_class.LogInfo("Трансляция с камеры 2 запущена");
                Log_text_box();
            }
        }

        //Приостановка вещания с камеры
        private void b_stop_Click(object sender, EventArgs e)
        {
            if (number_chosen_camera==1)
            {
                CameraList[0].EndStream();
                isCameraRunning_1 = false;
                Logger_class.LogInfo("Трансляция с камеры 1 остановлена");
                Log_text_box();
            }
            else
            {
                CameraList[1].EndStream();
                isCameraRunning_2 = false;
                Logger_class.LogInfo("Трансляция с камеры 2 остановлена");
                Log_text_box();
            }
        }

        //Закрытие приложения
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
            CameraList[0].EndStream();
            CameraList[1].EndStream();
            CameraList[0].Close();
            CameraList[1].Close();
            Form form2 = new StartWin();
            form2.Show();
        }
        /*--------------------------------------------- ОТЛАДКА --------------------------------------------------------*/
        //Выделение прямоугольником
        private void b_roi_kv_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }
            if (pb_crop.Image != null)
            {
                Crop_flag_change = true;
            }
            Chose_camera_otladka();
            number_procedure = 1;
            Bitmap crop = null;
            crop = Configuration_algorithm.Crop_image_library.Rectangle_selection_roi_crop(new Bitmap(camera_crop_chose.Image), (int)n_error.Value, Crop_flag_change);
            if (crop != null)
            {
                pb_crop.Image = crop;
            }
        }

        //Выделение по контуру
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
            Chose_camera_otladka();
            number_procedure = 2;
            Bitmap crop = null;
            crop = Configuration_algorithm.Crop_image_library.Choosen_selection_roi_crop(new Bitmap(camera_crop_chose.Image), (int)n_error.Value, Point_flag_change);
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
                crop = Configuration_algorithm.Crop_image_library.Rectangle_selection_roi_crop(new Bitmap(camera_crop_chose.Image), (int)n_error.Value, Crop_flag_change);
            }
            if (number_procedure == 2)
            {
                Point_flag_change = true;
                crop = Configuration_algorithm.Crop_image_library.Choosen_selection_roi_crop(new Bitmap(camera_crop_chose.Image), (int)n_error.Value, Point_flag_change);
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
