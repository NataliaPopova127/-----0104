using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TailoringLibrary.Server
{
    public class ServerClass
    {
        private static IPHostEntry s_iPHost;
        private static IPAddress s_iPAddress;
        private static IPEndPoint s_iPEndPoint;
        private static Socket s_socket;
        private static Socket s_s;
        private static string s_data;

        public static void Server(string hostname, int port)
        {
            int k = 0;
            Console.WriteLine("Однопоточный сервер запущен");
            MakeEndPoint(hostname, port);
            MakeSocket();
            ConnectWithEndPoint();
            ListenSocket();
            while (true)
            {
                Console.WriteLine("\nПрослушиваем {0} порт", s_iPEndPoint);
                MakeNewSocket();
                GiveClientData();
                SendDataToClient();
                SaveResultsInFile();
                if (FinishConnect())
                {
                    break;
                }
            }
            CleanAndCloseSocket();
        }
        public static void MakeEndPoint(string hostname, int port)
        {
            s_iPHost = Dns.GetHostEntry(hostname);
            s_iPAddress = s_iPHost.AddressList[0];
            s_iPEndPoint = new IPEndPoint(s_iPAddress, port);
        }
        public static void MakeSocket()
        {
            s_socket = new Socket(s_iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public static void ConnectWithEndPoint()
        {
            s_socket.Bind(s_iPEndPoint);
        }
        public static void ListenSocket()
        {
            s_socket.Listen(10);
        }
        public static void MakeNewSocket()
        {
            s_s = s_socket.Accept();
        }
        private static void GiveClientData() //////
        {
            byte[] bytes = new byte[1024];
            int bytesCount = s_s.Receive(bytes);
            s_data = null;
            s_data += Encoding.UTF8.GetString(bytes, 0, bytesCount);
            Console.WriteLine("Ответ клиента: " + s_data);
        }
        private static void SendDataToClient() //////
        {
            string reply;
            if (s_data != "")
                reply = "Успешно!";
            else reply = "Неуспешно";
            byte[] msg = Encoding.UTF8.GetBytes(reply);
            s_s.Send(msg);
        }
        private static void CleanAndCloseSocket() //////
        {
            s_s.Shutdown(SocketShutdown.Both);
            s_s.Close();
        }
        private static bool FinishConnect() //////
        {
            if (s_data == "<End>")
            {
                Console.WriteLine("Соединение завершено");
                return true;
            }
            else return false;
        }

        public static string date = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        public static string nameFile = $"registration_orders{date}.txt";
        private static void SaveResultsInFile() //////
        {
            StreamWriter file = new StreamWriter(nameFile, true);
            if (s_data == "<End>")
            {
                date = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
                file.WriteLine("Время окончания работы сервера: " + date);
            }
            else
            {
                file.WriteLine("Время начала работы сервера: " + date);
                file.WriteLine(s_data);
            }
            file.Close();
        }
    }
}
