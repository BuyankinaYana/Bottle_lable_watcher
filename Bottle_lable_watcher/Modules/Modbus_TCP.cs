using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace Bottle_lable_watcher.Modules
{
    internal class Modbus_TCP
    {
        private TcpClient tcpClient;              //TCP-клиент для сетевого соединения
        private NetworkStream stream;             //Сетевой поток для обмена данными
        private string ipAddress = "127.0.0.1";   //IP-адрес устройства
        private int port = 502;                   //TCP-порт
        private int connectTimeout = 1000;        //Таймаут подключения и чтения (мс)
        private bool connected = false;           //Текущее состояние соединения
        private int transactionNumber = 0;        //Счетчик транзакций
        private Timer pollTimer;                  //Таймер периодического опроса соединения
        private int pollInterval = 5000;          //Интервал опроса соединения (мс)
        private int connectTestRegister = 16406;  //Регистр для проверки доступности устройства
        public event Action<bool> ConnectionStatusChanged;  //Событие, уведомляющее подписчиков об изменении состояния соединения
        public bool Connected => connected; //Признак активного соединения с устройством

        //Создание экземпляра Modbus TCP
        public Modbus_TCP(string ip, int port)
        {
            ipAddress = ip;
            this.port = port;
        }

        //Подключение к устройству
        public bool Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                IAsyncResult asyncResult = tcpClient.BeginConnect(ipAddress, port, null, null);

                if (!asyncResult.AsyncWaitHandle.WaitOne(connectTimeout))
                    throw new Exception("connection timed out");

                tcpClient.EndConnect(asyncResult);
                stream = tcpClient.GetStream();
                stream.ReadTimeout = connectTimeout;

                connected = true;
                return true;
            }
            catch
            {
                connected = false;
                return false;
            }
        }

        //Разрыв соединения с утройством
        public void Disconnect()
        {
            try
            {
                tcpClient?.Close();
            }
            catch { }

            connected = false;
        }

        //Проверка наличия соединения
        public bool CheckConnection()
        {
            try
            {
                if (!connected)
                    Connect();

                // Чтение тестового регистра для проверки связи
                ReadSingleRegister(connectTestRegister);
                return true;
            }
            catch
            {
                connected = false;
                return false;
            }
        }

        //Периодический опрос состояния соединения
        public void StartPolling()
        {
            pollTimer = new Timer(_ => PollDevice(), null, 0, pollInterval);
        }

        //Остановка опроса состояния соединения
        public void StopPolling()
        {
            pollTimer?.Dispose();
        }

        //Генерация события при изменении состояния
        private void PollDevice()
        {
            bool prevConnected = connected;
            connected = CheckConnection();

            if (prevConnected != connected)
            {
                ConnectionStatusChanged?.Invoke(connected);
            }
        }

        //Чтение значения одиночного регистра
        public int ReadSingleRegister(int register)
        {
            try
            {
                ushort transactionId = (ushort)Interlocked.Increment(ref transactionNumber);
                byte[] transactionBytes = BitConverter.GetBytes(transactionId);

                byte[] reg = BitConverter.GetBytes((ushort)register);
                byte[] request = new byte[]
                {
                    transactionBytes[1], transactionBytes[0],
                    0x00, 0x00,
                    0x00, 0x06,
                    0x01,
                    0x03,
                    reg[1], reg[0],
                    0x00, 0x01
                };

                stream.Write(request, 0, request.Length);

                byte[] response = new byte[11];
                int bytesRead = stream.Read(response, 0, response.Length);

                ushort value = (ushort)(response[9] << 8 | response[10]);
                return value;
            }
            catch
            {
                connected = false;
                throw;
            }
        }

        //Запись значения в одиночный регистр для управления выключателем
        /*public void WriteSingleRegisterForBreaker(int register, int state)
        {
            try
            {
                byte[] reg = BitConverter.GetBytes((ushort)register);

                byte[] data = new byte[]
                {
                    0x00, 0x1C,
                    0x00, 0x00,
                    0x00, 0x06,
                    0x01,
                    0x06,
                    reg[1], reg[0],
                    (byte)((state >> 8) & 0xFF),
                    (byte)(state & 0xFF)
                };

                stream.Write(data, 0, data.Length);
                stream.Read(new byte[256], 0, 256);
            }
            catch
            {
                connected = false;
                throw;
            }
        }*/

        //Запись значения в одиночный регистр для управления подсветкой
        /*public void WriteSingleRegisterForFlash(int register, int state)
        {
            try
            {
                ushort transactionId = (ushort)new Random().Next(1, 65535);
                byte[] trans = BitConverter.GetBytes(transactionId);
                byte[] reg = BitConverter.GetBytes((ushort)register);
                byte[] val = BitConverter.GetBytes((ushort)state);

                byte[] data = new byte[]
                {
                    trans[1], trans[0],
                    0x00, 0x00,
                    0x00, 0x06,
                    0x01,
                    0x06,
                    reg[1], reg[0],
                    val[1], val[0]
                };

                stream.WriteTimeout = 1000;
                stream.ReadTimeout = 1000;

                stream.Write(data, 0, data.Length);

                var response = new byte[256];
                int bytesRead = stream.Read(response, 0, response.Length);
            }
            catch
            {
                connected = false;
                throw;
            }
        }*/

        //Запись состояния
        /*public void WriteSingleCoil(int coilAddress, bool state)
        {
            try
            {
                if (!connected || stream == null)
                    throw new InvalidOperationException("Modbus client is not connected.");

                byte[] coil = BitConverter.GetBytes((ushort)coilAddress);
                ushort coilValue = (ushort)(state ? 0xFF00 : 0x0000);
                byte[] valueBytes = BitConverter.GetBytes(coilValue);

                byte[] request = new byte[]
                {
                    0x00, 0x02,
                    0x00, 0x00,
                    0x00, 0x06,
                    0x01,
                    0x05,
                    coil[1], coil[0],
                    valueBytes[1], valueBytes[0]
                };

                stream.Write(request, 0, request.Length);

                byte[] response = new byte[8];
                int bytesRead = stream.Read(response, 0, response.Length);

                if (bytesRead != 8 || response[7] != 0x05)
                    throw new Exception("Invalid Modbus coil write response");
            }
            catch
            {
                connected = false;
                throw;
            }
        }*/
    }
}
