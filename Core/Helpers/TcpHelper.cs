using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Nextter.Socket.Core.Helpers
{
    public class TcpHelper
    {

		private const int DEFAULT_PORT = 8000;
		private const int BACKLOG = 100;

		private System.Net.Sockets.Socket _socket { get; set; }
		private ManualResetEvent _event { get; set; }
		private List<Handlers.SocketHandler> _clients { get; set; }

		public TcpHelper()
		{
			_event = new ManualResetEvent(false);
			_clients = new List<Handlers.SocketHandler>();
		}

		public void Start(IPAddress address, int? port)
		{
			var localEndPoint = new IPEndPoint(address, port.HasValue ? port.Value : DEFAULT_PORT);
			_socket = new System.Net.Sockets.Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				_socket.Bind(localEndPoint);
				_socket.Listen(BACKLOG);

				Console.WriteLine("Server started. Listening to TCP clients at {0}:{1}", address.ToString(), localEndPoint.Port);

				while (true)
				{
					_event.Reset();
					_socket.BeginAccept(new AsyncCallback(AcceptCallback), this);
					Console.WriteLine("Waiting for a connection...");
					_event.WaitOne();
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void AcceptCallback(IAsyncResult result)
		{
			var helper = (TcpHelper)result.AsyncState;
			helper._event.Set();

			var client = helper._socket.EndAccept(result);
			var handler = new Handlers.SocketHandler(client);
			helper._clients.Add(handler);
		}

	}
}
