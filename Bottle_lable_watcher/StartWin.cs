using Bottle_lable_watcher.Camera;
using Kvantron.Hardware.Cameras;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static Bottle_lable_watcher.Camera.Camera_serial_number;
using static System.Windows.Forms.DataFormats;

namespace Bottle_lable_watcher
{
    public partial class StartWin : Form
    {
        //private Camera_connection _cameraController;
        //private Camera_connection _cameraController_2;
        //private List<HikCamera> list_camera = new List<HikCamera>();
        private HikCamera cam;
        public StartWin()
        {
            InitializeComponent();
            //_cameraController = new Camera_connection("Vir74501888", this);

            /*if (_cameraController.Camera_connect())
            {
                L_camera_1.ForeColor = Color.ForestGreen;
                L_camera_1.BackColor = Color.PaleGreen;
            }
            _cameraController.Camera_disconnect();
            */
            /*bool[] flags = Camera_connection.Load_camera();
            //this.FormClosing += MainForm_FormClosing;
            

            string filePath = "AppSettings.json";
            CameraManager manager = new CameraManager(filePath);
            List<Json_reader> cameras = manager.LoadCameras();
            Label[] labels_name = new Label[] { L_camera_1, L_camera_2 };
            foreach (var camera in cameras)
            {
                var c = new HikCamera(camera.Serial_number);
                if (c.Open())
                {
                    int.TryParse(camera.ID_num, out int id);
                    list_camera.Add(c);
                    labels_name[id-1].ForeColor = Color.ForestGreen;
                }
                c.Close();
            }*/
        }

        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
        //_cameraController?.Dispose();
        //base.OnFormClosing(e);
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void B_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
