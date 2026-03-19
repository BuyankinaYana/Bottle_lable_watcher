using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bottle_lable_watcher.Camera
{
    internal class Camera_serial_number
    {
        public class Json_reader
        {
            [JsonPropertyName("id")]
            public string ID_num { get; set; }

            [JsonPropertyName("name")]
            public string Camera_name { get; set; }

            [JsonPropertyName("serial_number")]
            public string Serial_number { get; set; }
        }

        public class CameraManager
        {
            private readonly string _filePath;

            public CameraManager(string filePath)
            {
                _filePath = filePath;
            }

            // Метод для загрузки камер из JSON
            public List<Json_reader> LoadCameras()
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var camerasData = JsonSerializer.Deserialize<CamerasData>(json, options);
                    return camerasData?.Cameras ?? new List<Json_reader>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки JSON: {ex.Message}");
                    return new List<Json_reader>();
                }
            }

            // Вспомогательный класс для десериализации
            private class CamerasData
            {
                public List<Json_reader> Cameras { get; set; }
            }
        }
    }
}
