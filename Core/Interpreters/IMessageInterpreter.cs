using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Interpreters
{
    internal interface IMessageInterpreter
    {

		bool Test(string data);
		Models.Message Decode(string data);
		string Encode(Models.Message message);

	}
}
