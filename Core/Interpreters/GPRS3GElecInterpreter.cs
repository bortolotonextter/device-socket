using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nextter.Socket.Core.Interpreters
{
    internal class GPRS3GElecInterpreter : IMessageInterpreter
    {

		public bool Test(string data)
		{
			return Regex.IsMatch(data, @"\[[A-Za-z0-9]{2}\*[0-9]{10}\*[0-9ABCDEF]{4}\**\]");
		}

		public Models.Message Interpret(string data)
		{
			return new Models.Message();
		}

		public string Encript(Models.Message message)
		{
			return String.Empty;
		}

	}
}
