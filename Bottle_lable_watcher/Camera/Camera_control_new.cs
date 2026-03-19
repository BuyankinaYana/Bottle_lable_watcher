using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Bottle_lable_watcher.Camera
{
    internal class Camera_control_new
    {
        private HikCamera camera;
        private bool isStreaming = false;
        public bool IsConnected => camera?.Connected ?? false;
        public bool IsStreaming => isStreaming;

        //Создание и подключение камеры
        public bool Camera_connect(string serialNumber)
        {
            camera = new HikCamera(serialNumber);
            if (camera?.Open() == true)
            {
                //camera.AcquisitionMode = AcquisitionMode.Continuous;
                //camera.TriggerMode = false;
                //camera.ExposureTime = 800;
                //camera.Gain = 0;
                //camera.SetSettings();
                return true;
            }
            return false;

        }

        //Подписка на содержимое, возврат картинки
        public bool Start_stream_camera()
        {
            if (!IsConnected) return false;
            if (isStreaming) return true;
            if (camera.StartStream())
            {
                isStreaming = true;
                //camera.SendImage += Image_from_camera();
                //Image_from_camera();

                //_imageThread = new Thread(Image_from_camera);   //Многопоточность (на будущее)
                //_imageThread.IsBackground = true;
                //_imageThread.Start();
                return true;

            }
            return false;
        }
        private void Image_from_camera(Mat picture)
        {
            if (picture.Empty()) return;
            Bitmap bitmap = null;
            if (picture.Channels() == 3)
            {
                bitmap = picture.ToBitmap();
            }
            else
            {
                return; //Не поддерживаемый формат
            }
        }
    }
}
