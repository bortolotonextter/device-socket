using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Handlers
{
    public class SocketHandler
    {

		public const int BUFFER_SIZE = 1024;

		private System.Net.Sockets.Socket _socket;
		private Proxies.MessageInterpreterProxy _proxy;
		private Interpreters.IMessageInterpreter _interpreter;
		private byte[] _buffer = new byte[BUFFER_SIZE];

		public SocketHandler(System.Net.Sockets.Socket socket)
		{
			_socket = socket;
			_proxy = new Proxies.MessageInterpreterProxy();

			_socket.BeginReceive(this._buffer, 0, BUFFER_SIZE, System.Net.Sockets.SocketFlags.None, new AsyncCallback(ReadCallback), this);

			Console.WriteLine("Client connected!");
		}

		private void Receive(string data)
		{
			try
			{
				Console.WriteLine("Data Received: {0}", data);
				var interpreter = GetInterpreter(data);
				var message = interpreter.Process(data);
				if (!String.IsNullOrEmpty(message))
				{
					Send(message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void Send(string data)
		{
			Console.WriteLine("Data Sent: {0}", data);
			byte[] byteData = Encoding.ASCII.GetBytes(data);
			_socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), this);
		}

		internal static void ReadCallback(IAsyncResult result)
		{
			var handler = (SocketHandler)result.AsyncState;
			int bytesRead = handler._socket.EndReceive(result);
			if (bytesRead > 0)
			{
				var data = Encoding.ASCII.GetString(handler._buffer, 0, bytesRead);
				handler.Receive(data);
			}
		}

		internal static void SendCallback(IAsyncResult result)
		{
			try
			{
				SocketHandler handler = (SocketHandler)result.AsyncState;
				int bytesSent = handler._socket.EndSend(result);
				handler._socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
				handler._socket.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private Interpreters.IMessageInterpreter GetInterpreter(string data)
		{
			if(_interpreter == null)
			{
				_interpreter = _proxy.GetInterpreter(data);
			}
			return _interpreter;
		}

	}
}
