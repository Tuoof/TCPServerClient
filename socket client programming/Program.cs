using System;

namespace socket_client_programming
{
    class Program
    {
        static void Main()
        {
            var socket = new SocketClient();

            socket.Start();
        }
    }
}