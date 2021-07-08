using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Linq;

namespace socket_server_programming
{
    class SocketServer
    {
        static TcpListener Listener;
        static TcpClient tcpClients;
        static List<TcpClient> clientsList = new List<TcpClient>();
        private List<string> messages = new List<string>();
        static Int32 port = 5000;
        static IPAddress localAddress = IPAddress.Parse("127.0.0.1");

        public void Start()
        {
            Listener = new TcpListener(localAddress, port);
            Listener.Start();
            Console.WriteLine("Server created...");
            int clientCounter = 0;
      
            try
            {
                while (true)
                {
                    tcpClients = Listener.AcceptTcpClient();
                    clientsList.Add(tcpClients);
                    Console.WriteLine(" New Client Accepted...!");

                    //Server Thread
                    Thread serverThread = new Thread(() => Announce(tcpClients));
                    serverThread.Start();

                    //start listener
                    Thread clientThread = new Thread(() => ClientListener(tcpClients, clientCounter));
                    clientThread.Start();

                    clientCounter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Announce(object argument)
        {
            TcpClient tcpClient = (TcpClient)argument;
            StreamReader reader = new StreamReader(tcpClient.GetStream());

            try
            {
                while (true)
                {
                    string message = Console.ReadLine();
                    BroadCast(message, null, tcpClient, 0);
                    string chat = "Announce : " + message;
                    Console.WriteLine(chat);

                    writeMessage(chat);
                }
            }
            catch (IOException e)
            {
                Console.Write(e.ToString());
            }
        }

        public void ClientListener(object argument, int num)
        {
            TcpClient tcpClient = (TcpClient)argument;
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            StreamWriter writer = new StreamWriter(tcpClient.GetStream());

            Console.WriteLine("Client " + num.ToString() + " connected...");
                      
            try
            {
                string name = reader.ReadLine();

                while (true)
                {
                    string message = reader.ReadLine();
                    BroadCast(message, name, tcpClient, num);
                    string chat = name + " : " + message;
                    Console.WriteLine(chat);

                    writeMessage(chat);
                }
            }
            catch(IOException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(" ");
            }
            finally
            {
                clientsList.Remove(tcpClient);
                Console.WriteLine("Client " + num.ToString() + " Disconnected...");

                if (clientsList?.Any() == false)
                {
                    tcpClients.Client.Disconnect(true);
                    tcpClients.Client.Close();
                    Console.WriteLine("Server Closed...");
                    System.Environment.Exit(1);  
                }
            }
            
        }

        public void BroadCast(string msg, string nam, TcpClient currentClient, int n)
        {
            foreach (TcpClient client in clientsList)
            {
                //broadcast to all client except sender
                StreamWriter writer = new StreamWriter(client.GetStream());
                if (n == 0)
                {
                    //broadcast from server to all client
                    writer.WriteLine("Server : " + msg);
                }
                else if (client != currentClient)
                {
                    //broadcast from client to all client
                    writer.WriteLine(nam + " : " + msg);
                }
                writer.Flush();
            }
        }

        public void writeMessage(string chat)
        {
            try
            {
                messages.Add(chat);
                File.WriteAllLines("chat.txt", messages);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(" ");
            }
            
        }
    }
}