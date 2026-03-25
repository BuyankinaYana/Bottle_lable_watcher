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
        private List<HikCamera> camera_list = new List<HikCamera>();
        private List<string> status_element = new List<string>();
        private Modbus_TCP client_tcp;

        public StartWin()
        {
            InitializeComponent();
            Logger_class.LogInfo("Начало сеанса работы");
            Shown += Form1_Shown;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            await Task.Delay(50);
            initialized();         
        }

        //Функция инициализации подключения
        private void initialized()
        {   
            string filePath = "AppSettings.json";
            CameraManager manager = new CameraManager(filePath);
            List<Json_reader> cameras = manager.LoadCameras();
            Label[] labels_name = new Label[] { L_camera_1, L_camera_2 };
            foreach (var camera in cameras)
            {
                var c = new HikCamera(camera.Serial_number);
                if (c.Open())
                {
                    camera_list.Add(c);
                    int.TryParse(camera.ID_num, out int id);
                    Logger_class.LogInfo($"Камера {id} подключена");
                    labels_name[id - 1].ForeColor = Color.ForestGreen;
                    status_element.Add("Подключено");
                }
                else
                {
                    uint Error = c.LastErrorCode;
                    string errorDescription = GetErrorDescription(Error);
                    int.TryParse(camera.ID_num, out int id);
                    labels_name[id - 1].ForeColor = Color.Red;
                    Logger_class.LogWarning(errorDescription);
                    status_element.Add(errorDescription);
                }
            }
            client_tcp = new Modbus_TCP("10.10.69.228", 502);
            if (client_tcp.Connect())
            {
                L_module.ForeColor = Color.ForestGreen;
                Logger_class.LogInfo("Модуль OVEN I/O подключён");
                status_element.Add("Подключено");
            }
            else
            {
                L_module.ForeColor = Color.Red;
                Logger_class.LogError("Не удалось подключиться к модулю OVEN I/O");
                status_element.Add("Не подключено");
            }

        }

        //Функция, хранящая коды ошибок
        private string GetErrorDescription(uint errorCode)
        {
            return errorCode switch
            {
                2147484169 => "Нет данных",
                2147484163 => "Нет доступа к камере. Камера используется другим приложением",
                2147484166 => "Не поддерживается",
                _ => $"Неизвестная ошибка: 0x{errorCode:X8}"
            };
        }

        // Кнопка продолжить
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.CameraList = camera_list;
            form1.StatusList = status_element;
            form1.Show();
            this.Hide();
        }

        //Кнопка выхода
        private void B_cancel_Click(object sender, EventArgs e)
        {
            client_tcp.Disconnect();
            camera_list[0].Close();
            camera_list[1].Close();
            Logger_class.LogInfo("Завершение сеанса работы");
            Application.Exit();
        }

        //Кнопка перезагрузки
        private void B_update_Click(object sender, EventArgs e)
        {
            initialized();
        }
    }
}
