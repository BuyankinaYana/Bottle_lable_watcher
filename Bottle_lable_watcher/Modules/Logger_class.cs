using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bottle_lable_watcher.Modules
{
    internal class Logger_class
    {
        private static readonly object _lock = new object();
        private static string _filePath = "log.txt";
        private static DateTime _lastCleanupDate = DateTime.MinValue;

        public static void Log(string message, string level = "INFO")
        {
            //Временная метка
            string logRecord = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            //lock для обеспечения потокобезопасности при записи в файл
            lock (_lock)
            {
                try
                {
                    //Чистка логов в сутки
                    if (DateTime.Now.Date > _lastCleanupDate.Date)
                    {
                        CleanupOldLogs();
                        _lastCleanupDate = DateTime.Now;
                    }
                    using (StreamWriter writer = new StreamWriter(_filePath, true))
                    {
                        writer.WriteLine(logRecord);
                    }
                }
                catch (Exception ex)
                {
                    string msg = "Ошибка записи в лог: " + ex.Message;
                    MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Функция для возвращения лога
        public static string Return_message_log()
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                return reader.ReadToEnd();
            }
        }

        //Функция чистки логов
        private static void CleanupOldLogs()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return;

                var allLines = File.ReadAllLines(_filePath);
                var currentDate = DateTime.Now.Date;
                var filteredLines = new List<string>();

                foreach (var line in allLines)
                {
                    //Разделение строки с датой
                    if (line.Length >= 10 && line[0] == '[' && DateTime.TryParseExact(
                        line.Substring(1, 10),
                        "yyyy-MM-dd",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime logDate))
                    {
                        if (logDate.Date >= currentDate)
                        {
                            filteredLines.Add(line);
                        }
                    }
                }

                //Перезаписываем сегодняшние логами
                File.WriteAllLines(_filePath, filteredLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка очистки логов: " + ex.Message);
            }
        }

        public static void LogInfo(string message) => Log(message, "INFO");
        public static void LogWarning(string message) => Log(message, "WARNING");
        public static void LogError(string message) => Log(message, "ERROR");
    }
}
