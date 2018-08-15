using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Proxies
{
    internal class MessageInterpreterProxy
    {

		private List<Interpreters.IMessageInterpreter> _interpreters;

		public MessageInterpreterProxy()
		{
			_interpreters = new List<Interpreters.IMessageInterpreter>();
			_interpreters.Add(new Interpreters.GPRS3GElecInterpreter());
		}

		public Interpreters.IMessageInterpreter GetInterpreter(string data)
		{
			Interpreters.IMessageInterpreter interpreter = null;
			foreach(var i in _interpreters)
			{
				if (i.Test(data))
				{
					interpreter = i;
					break;
				}
			}

			if(interpreter == null)
			{
				throw new Exception("No interpreter found!");
			}

			return interpreter;
		}

	}
}
