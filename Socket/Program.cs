using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Nextter
{
    class Program
    {
        static void Main(string[] args)
        {
			TcpHelper.StartServer(5600);
        }
    }

	class TcpHelper
	{
		private static TcpListener listener { get; set; }
		private static StreamWriter log { get; set; }
		private static System.Net.Sockets.Socket socket { get; set; }
		public static string data = null;

		public static void StartServer(int port)
		{
			byte[] bytes = new Byte[1024];
			IPAddress address = IPAddress.Any;
			IPEndPoint localEndPoint = new IPEndPoint(address, port);

			socket = new System.Net.Sockets.Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			log = File.CreateText("log.txt");

			try
			{
				socket.Bind(localEndPoint);
				socket.Listen(10);

				Console.WriteLine($"Server started. Listening to TCP clients at port {port}");

				while (true)
				{
					Console.WriteLine("Waiting for a connection...");
					// Program is suspended while waiting for an incoming connection.  
					System.Net.Sockets.Socket handler = socket.Accept();
					data = null;

					Console.WriteLine("Client connected!");

					// An incoming connection needs to be processed.  
					while (true)
					{
						int bytesRec = handler.Receive(bytes);
						data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
						if (String.IsNullOrEmpty(data))
						{
							break;
						}
						else
						{
							var message = new Message(data);
							if(message.Type == Message.Types.Position)
							{
								var position = new Position(message.Content);

								Console.Clear();
								Console.WriteLine("ID: {0}", message.ID);
								Console.WriteLine("Date/Time: {0}", position.DateTime.ToString());
								Console.WriteLine("Latitude: {0}", position.Latitude);
								Console.WriteLine("Longitude: {0}", position.Longitude);
								Console.WriteLine("Speed: {0}", position.Speed);
								Console.WriteLine("Direction: {0}", position.Direction);
								Console.WriteLine("Altitude: {0}", position.Altitude);
								Console.WriteLine("Satellites: {0}", position.Satellites);
								Console.WriteLine("Signal: {0}", position.Signal);
								Console.WriteLine("Power: {0}", position.Power);

								log.WriteLine("ID: {0} | Lat: {1} | Lon: {2} | Power: {3}", message.ID, position.Latitude, position.Longitude, position.Power);
								log.Flush();
							}
						}
					}


					// Echo the data back to the client.  
					byte[] msg = Encoding.ASCII.GetBytes(data);

					handler.Send(msg);
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

		public static void Listen()
		{
			if (listener != null)
			{

				// Continue listening.  
				while (true)
				{
					Console.WriteLine("Waiting for client...");
					var clientTask = listener.AcceptTcpClientAsync(); // Get the client  

					if (clientTask.Result != null)
					{
						Console.WriteLine("Client connected. Waiting for data.");
						var client = clientTask.Result;
						string message = "";

						while (message != null && !message.StartsWith("quit"))
						{
							//byte[] data = Encoding.ASCII.GetBytes("Send next data: [enter 'quit' to terminate] ");
							//client.GetStream().Write(data, 0, data.Length);

							StreamReader reader = new StreamReader(client.GetStream(), true);
							message = reader.ReadToEnd();

							if (!String.IsNullOrEmpty(message))
							{
								Console.WriteLine("Receive: " + message);
								log.WriteLine("Receive: " + message);
								log.Flush();

								var data = message.Split("*");

								var lk = data.Length > 3 ? data[3] : "";
								if (lk.StartsWith("LK"))
								{
									var responseText = "[3G*" + data[1] + "*0002*LK]";
									byte[] response = Encoding.ASCII.GetBytes(responseText);
									client.GetStream().Write(response, 0, response.Length);

									Console.WriteLine("Send: " + responseText);
									log.WriteLine("Send: " + responseText);
									log.Flush();
								}

								break;
							}
						}
						Console.WriteLine("Closing connection.");
						client.GetStream().Dispose();
					}
				}
			}
		}
	}
}
