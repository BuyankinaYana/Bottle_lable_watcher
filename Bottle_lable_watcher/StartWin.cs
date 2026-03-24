using Bottle_lable_watcher.Camera;
using Bottle_lable_watcher.Modules;
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
        private HikCamera cam_1;
        private HikCamera cam_2;
        private Modbus_TCP client_tcp;

        public StartWin()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            await Task.Delay(100);
            initialized();         
        }

        //Функция инициализации подключения
        private void initialized()
        {
            Task.Delay(1000);

            client_tcp = new Modbus_TCP("10.10.69.228", 502);
            if (client_tcp.Connect())
            {
                L_module.ForeColor = Color.ForestGreen;
            }
            else { L_module.ForeColor = Color.Red; }

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
                    Logger_class.LogInfo($"Камера {id} подключена");
                    labels_name[id - 1].ForeColor = Color.ForestGreen;
                    c.Close();
                }
                else
                {
                    
                    int.TryParse(camera.ID_num, out int id);
                    labels_name[id - 1].ForeColor = Color.Red;
                    Logger_class.LogError($"Камера {id} не удалось подключить");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void B_cancel_Click(object sender, EventArgs e)
        {
            client_tcp.Disconnect();
            Application.Exit();
        }

        private void B_update_Click(object sender, EventArgs e)
        {
            initialized();
        }
    }
}
