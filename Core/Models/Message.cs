using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter.Socket.Core.Models
{
    public class Message
    {

		public enum Types
		{
			Position,
			Status
		}

		public Device Device { get; set; }
		public Position Position { get; set; }
		public Status Status { get; set; }
		public DateTime DateTime { get; set; }

		public Message()
		{
		}

    }
}
