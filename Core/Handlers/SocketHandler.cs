using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Handlers
{
    public class SocketHandler
    {

		private System.Net.Sockets.Socket _socket;
		private Proxies.MessageInterpreterProxy _proxy;
		private Interpreters.IMessageInterpreter _interpreter;

		public SocketHandler(System.Net.Sockets.Socket socket)
		{
			_socket = socket;
			_proxy = new Proxies.MessageInterpreterProxy();
		}

		private void Process(string data)
		{
			try
			{
				var interpreter = GetInterpreter(data);
				var message = interpreter.Decode(data);
				if (message != null)
				{
				}
			}
			catch (Exception e)
			{
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
