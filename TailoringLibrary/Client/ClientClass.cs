using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TailoringLibrary.Client
{
    public class ClientClass
    {
        private static IPHostEntry s_iPHost;
        private static IPAddress s_iPAddress;
        private static IPEndPoint s_iPEndPoint;
        private static Socket s_socket;
        private static string s_message;

        public static void CommunicateClient(string hostname, int port)
        {
            try
            {
                MakeEndPoint(hostname, port);
                MakeSocket();
                ConnectWithServer();
                ClientMessage();
                AnswerOfServer();
                if (FinishConnect()) CleanAndCloseSocket();
                else
                {
                    CommunicateClient(hostname, port);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Client" + ex.Message);
            }
        }
        private static void MakeEndPoint(string hostname, int port)
        {
            s_iPHost = Dns.GetHostEntry(hostname);
            s_iPAddress = s_iPHost.AddressList[0];
            s_iPEndPoint = new IPEndPoint(s_iPAddress, port);
        }
        private static void MakeSocket()
        {
            s_socket = new Socket(s_iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        private static void ConnectWithServer()
        {
            s_socket.Connect(s_iPEndPoint);
        }
        private static void ClientMessage()
        {
            s_message = ClientInterface.MenuOfOrder();
            byte[] data = Encoding.UTF8.GetBytes(s_message);
            s_socket.Send(data);
        }
        private static void AnswerOfServer()
        {
            byte[] bytes = new byte[1024];
            int bytesRec = s_socket.Receive(bytes);
            Console.WriteLine("\nОтвет от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));
        }
        private static bool FinishConnect()
        {
            if (s_message == "<End>")
            {
                Console.WriteLine("Соединение завершено");
                return true;
            }
            else return false;
        }
        private static void CleanAndCloseSocket()
        {
            s_socket.Shutdown(SocketShutdown.Both);
            s_socket.Close();
        }

    }
}
