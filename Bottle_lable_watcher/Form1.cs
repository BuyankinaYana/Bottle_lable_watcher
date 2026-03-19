using Bottle_lable_watcher.Camera;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Media.Media3D;

namespace Bottle_lable_watcher
{
    public partial class Form1 : Form
    {
        private Camera_connection _cameraController;
        public HikCamera camera_1;
        private bool isCameraRunning = false;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox1.Paint += PictureBox1_Paint;
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
                MessageBox.Show("Камера подключена");
                camera_1.StartStream();
                camera_1.SendImage += Image_camera_received;
                Thread.Sleep(1000);
                camera_1.EndStream();
            }
            else
            {
                MessageBox.Show("Не удалось подключиться к камере.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isCameraRunning = true;
            pictureBox1.Invalidate();
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            camera_1.StartStream();
            camera_1.SendImage += Image_camera_received;
            isCameraRunning = true;
            pictureBox1.Invalidate();
        }

        private void Image_camera_received(Mat picture)
        {
            Bitmap bitmap = null;
            bitmap = picture.ToBitmap();
            pictureBox1.Image = bitmap;
        }

        private void b_stop_Click(object sender, EventArgs e)
        {
            camera_1.EndStream();
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
            Form form2 = new StartWin();
            form2.Show();
        }
//--------------------------------------------- ОТЛАДКА ----------------------------------------------------------
        private void b_roi_kv_Click(object sender, EventArgs e)
        {

        }
    }
}
