using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bottle_lable_watcher.Modules
{
    internal class Logger_class
    {
        private static readonly object _lock = new object();
        private static string _filePath = "log.txt";

        public static void Log(string message, string level = "INFO")
        {
            //Временная метка
            string logRecord = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            //lock для обеспечения потокобезопасности при записи в файл
            lock (_lock)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(_filePath, true, Encoding.UTF8))
                    {
                        writer.WriteLine(logRecord);
                    }
                }
                catch (Exception ex)
                {
                    // В случае ошибки записи выводим информацию в консоль
                    Console.WriteLine("Ошибка записи в лог: " + ex.Message);
                }
            }
        }

        public static void LogInfo(string message) => Log(message, "INFO");
        public static void LogWarning(string message) => Log(message, "WARNING");
        public static void LogError(string message) => Log(message, "ERROR");
    }
}
