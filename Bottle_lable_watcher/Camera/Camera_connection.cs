using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bottle_lable_watcher.Camera.Camera_serial_number;

namespace Bottle_lable_watcher.Camera
{
    internal class Camera_connection
    {
        private HikCamera _camera;
        public HikCamera Received_camera => _camera;
        private PictureBox _pictureBox;
        private Thread _imageThread;
        private bool _isStreaming = false;
        private readonly Form _form;
        private readonly object _lock = new object();

        public event Action<Bitmap> OnImageReceived;
        public bool IsConnected => _camera?.Connected ?? false;
        public bool IsStreaming => _isStreaming;

        public Camera_connection(string serialNumber)
        {
            _camera = new HikCamera(serialNumber);
            //_form = form;
            //_form.FormClosing += OnFormClosing; // Подписка на закрытие формы
        }

        //Пересылка
        public HikCamera Camera_receive()
        {
            return _camera;
        }

        public static string[] Serial_number_reader()
        {
            string file_path = "AppSettings.json";
            List<string> list = new List<string>();
            CameraManager manager = new CameraManager(file_path);
            List<Json_reader> list_camera = manager.LoadCameras();
            int i = 0;
            foreach (var camera in list_camera)
            {
                list.Add(camera.Serial_number);
            }

            return list.ToArray();
        }
        
        //Проверка камеры на подключение
        public bool Camera_connect()
        {
            if (_camera?.Open()==true)
            {
                _camera.AcquisitionMode = AcquisitionMode.Continuous;
                _camera.TriggerMode = false;
                _camera.ExposureTime = 800;
                _camera.Gain = 0;
                _camera.SetSettings();
                return true;
            }
            return false;
            
        }

        //Подписка на содержимое камеры
        public bool Start_stream_camera()
        {
            if (!IsConnected) return false;
            if (_isStreaming) return true;
            if (_camera.StartStream())
            {
                _isStreaming = true;
                //Image_from_camera();
                _imageThread = new Thread(Image_from_camera);   //Многопоточность (на будущее)
                _imageThread.IsBackground = true;
                _imageThread.Start();
                return true;

            }
            return false;
        }

        //Прекращение вещания с камеры
        public bool  Stop_stream_camera()
        {
            if (!_isStreaming) return true;
            _isStreaming = false;
            _camera.EndStream();
            _imageThread?.Join(2000);   //Ожидание завершения работы потока (на будущее)
            return true;
        }

        //Отправление изображения с камеры
        private void Image_from_camera()
        {
            _camera.SendImage += Image_camera_received;
            while (_isStreaming)
            {
                Thread.Sleep(1);    //Задержка, для устранения накладок
            }
            _camera.SendImage -= Image_camera_received;
        }

        //Обработка изображения для отправки на форму
        private void Image_camera_received(Mat picture)
        {
            if (picture.Empty()) return;
            Bitmap bitmap = null;
            try
            {
                if (picture.Channels()==3)
                {
                    bitmap = picture.ToBitmap();
                }
                else
                {
                    return; //Не поддерживаемый формат
                }
                if (_pictureBox!=null && _pictureBox.InvokeRequired)
                {
                    _pictureBox.Invoke(new Action(() => OnImageReceived?.Invoke(bitmap)));
                }
                else
                {
                    OnImageReceived?.Invoke(bitmap);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при конвертации изображения");
            }
            finally
            {
                bitmap?.Dispose();
                picture?.Dispose();
            }
        }

        //Вывод картинки в PB
        public void Set_PictureBox(PictureBox pb)
        {
            _pictureBox = pb;
        }

        //Остановка вещания при закрытии формы
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Stop_stream_camera();
            Camera_disconnect();
        }

        //Отключение камеры
        public void Camera_disconnect()
        {
            Stop_stream_camera();
            if (_camera != null)
            {
                _camera.Close();
                _camera = null;
            }
        }

        //Подписка на закрытие формы
        public void Dispose()
        {
            Camera_disconnect();
            _form.FormClosing -= OnFormClosing;
            GC.SuppressFinalize(this);
        }
    }
}
