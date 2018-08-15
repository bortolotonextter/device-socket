using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Singletons
{
    public class SocketSingleton
    {

		private SocketSingleton _socket;
		private Proxies.MessageInterpreterProxy _messageInterpreterProxy;

		public SocketSingleton Instance()
		{
			if(_socket == null)
			{
				_socket = new SocketSingleton();
			}
			return _socket;
		}

		public SocketSingleton()
		{
			_messageInterpreterProxy = new Proxies.MessageInterpreterProxy();
		}

		public string Process(string data)
		{
			try
			{
				var interpreter = _messageInterpreterProxy.GetInterpreter(data);

				var message = interpreter.Decode(data);

				if (message != null)
				{

				}
			}
			catch (Exception e)
			{

			}
			return String.Empty;
		}

    }
}
