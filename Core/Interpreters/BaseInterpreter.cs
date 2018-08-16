using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nextter.Socket.Core.Interpreters
{
    internal abstract class BaseInterpreter : IMessageInterpreter
    {

		public virtual bool Test(string data)
		{
			return false;
		}

		public virtual string Process(string data)
		{
			return String.Empty;
		}

		public virtual Models.Message Decode(string data)
		{
			return new Models.Message();
		}

		public virtual string Encode(Models.Message message)
		{
			return String.Empty;
		}

	}
}
