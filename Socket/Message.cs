using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter
{
    class Message
    {

		public enum Types
		{
			LinkKeep,
			Position,
			Alarm
		}

		public string Manufacturer { get; set; }
		public string ID { get; set; }
		public string Length { get; set; }
		public string Content { get; set; }
		public Types Type { get; set; }

		public Message(string data)
		{
			var fields = data.Replace("[", "").Replace("]", "").Split("*");
			this.Manufacturer = fields[0];
			this.ID = fields[1];
			this.Length = fields[2];
			this.Content = fields[3];

			var _data = this.Content.Split(",");
			switch (_data[0])
			{
				case "LK":
					this.Type = Types.LinkKeep;
					break;
				case "UD":
				case "UD2":
				case "WAD":
				case "RAD":
				case "WG":
					this.Type = Types.Position;
					break;
				case "AL":
					this.Type = Types.Alarm;
					break;
			}
		}

	}
}
