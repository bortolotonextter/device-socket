using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
			var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5600);
			var socket = new Socket(remoteEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(remoteEndPoint);

			byte[] bytes = new byte[1024];

			string input = String.Empty;
			do
			{
				input = Console.ReadLine();

				byte[] data = Encoding.ASCII.GetBytes(input);
				int bytesSent = socket.Send(data);

				int bytesRec = socket.Receive(bytes);
				Console.WriteLine("Return: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
			}
			while (input != "q");

			socket.Shutdown(SocketShutdown.Both);
			socket.Close();
		}
	}
}
