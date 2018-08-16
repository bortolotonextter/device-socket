using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nextter.Socket.Core.Interpreters
{
    internal class GPRS3GElecInterpreter : BaseInterpreter, IMessageInterpreter
    {

		public override bool Test(string data)
		{
			return Regex.IsMatch(data, @"\[[A-Za-z0-9]{2}\*[0-9]{10}\*[0-9ABCDEF]{4}\**.+\]");
		}

		public override string Process(string data)
		{
			var message = Decode(data);
			return String.Empty;
		}

		public override Models.Message Decode(string data)
		{
			var match = Regex.Match(data, @"\[([A-Za-z0-9]{2})\*([0-9]{10})\*([0-9ABCDEF]{4})\*(*.+)\]");
			return new Models.Message();
		}

		public override string Encode(Models.Message message)
		{
			return String.Empty;
		}

	}
}
