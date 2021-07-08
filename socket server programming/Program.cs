using System;

namespace socket_server_programming
{
    class Program
    {
        static public void Main()
        {
            var socket = new SocketServer();

            socket.Start();
        }
    }
}