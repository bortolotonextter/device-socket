using System;
using System.Collections.Generic;
using System.Text;

namespace Nextter
{
    class Position
    {

		public DateTime DateTime { get; set; }
		public bool Positioning { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public decimal Speed { get; set; }
		public decimal Direction { get; set; }
		public decimal Altitude { get; set; }
		public int Satellites { get; set; }
		public int Signal { get; set; }
		public int Power { get; set; }

		public Position(string data)
		{
			var fields = data.Split(",");

			var date = fields[1];
			var time = fields[2];
			this.DateTime = new DateTime(2000 + int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(2, 2)), int.Parse(date.Substring(0, 2)), int.Parse(time.Substring(0, 2)), int.Parse(time.Substring(2, 2)), int.Parse(time.Substring(4, 2)));

			var culture = new System.Globalization.CultureInfo("en-US");

			this.Positioning = fields[3] == "A";
			this.Latitude = decimal.Parse(fields[4], culture) * (fields[5] == "S" ? -1 : 1);
			this.Longitude = decimal.Parse(fields[6], culture) * (fields[7] == "W" ? -1 : 1);
			this.Speed = decimal.Parse(fields[8], culture);
			this.Direction = decimal.Parse(fields[9], culture);
			this.Altitude = decimal.Parse(fields[10], culture);
			this.Satellites = int.Parse(fields[11]);
			this.Signal = int.Parse(fields[12]);
			this.Power = int.Parse(fields[13]);
		}

	}
}
