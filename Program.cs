///
/// .net core socket demo
///
///
/// aaa
///
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace socket
{
  class Program
    {
        private static byte[] result = new byte[1024];
        private static int myProt = 5678;   //?port
        static Socket serverSocket;
        static void Main(string[] args)
        {
            //bind server ip  in docker must be 0.0.0.0
            IPAddress ip = IPAddress.Parse("0.0.0.0");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));
            serverSocket.Listen(10);
            Console.WriteLine("Server started. Listening to TCP clients at {0}", serverSocket.LocalEndPoint.ToString());
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        /// <summary>
        /// Listening Client connect
        /// </summary>
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello\r\n"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }

        /// <summary>
        /// Receive message
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {

                    int receiveNumber = myClientSocket.Receive(result);
                    Console.WriteLine("Receive client:{0} message:{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
            }
        }
  }
}
