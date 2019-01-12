using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PZ3_NetworkService.StaticClasses;
using PZ3_NetworkService.ViewModel;

namespace PZ3_NetworkService
{
	public class NetworkManagment
	{
		//private int count = 15; // Inicijalna vrednost broja objekata u sistemu
								// ######### ZAMENITI stvarnim brojem elemenata

		public void CreateListener()
		{
			var tcp = new TcpListener(IPAddress.Any, 25565);
			tcp.Start();

			var listeningThread = new Thread(() =>
			{
				while (true)
				{
					var tcpClient = tcp.AcceptTcpClient();
					ThreadPool.QueueUserWorkItem(param =>
					{
						//Prijem poruke
						NetworkStream stream = tcpClient.GetStream();
						string incomming;
						byte[] bytes = new byte[1024];
						int i = stream.Read(bytes, 0, bytes.Length);
						//Primljena poruka je sacuvana u incomming stringu
						incomming = Encoding.ASCII.GetString(bytes, 0, i);

						//Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
						if (incomming.Equals("Need object count"))
						{
							//Response
							/* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
							Byte[] data = System.Text.Encoding.ASCII.GetBytes(StaticClass.Servers.Count.ToString());
							stream.Write(data, 0, data.Length);
						}
						else
						{
							try
							{
								//U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
								//Console.WriteLine(incomming); //Na primer: "Objekat_1:272"

								//################ IMPLEMENTACIJA ####################
								// Obraditi poruku kako bi se dobile informacije o izmeni
								string[] vals = incomming.Split('_', ':');
								if (vals.Length > 0)
								{
									int id = Convert.ToInt32(vals[1]);
									int value = Convert.ToInt32(vals[2]);

									// Azuriranje potrebnih stvari u aplikaciji
									StaticClass.Servers[id].Value = value;
									StaticClass.Rectangles[id].Height = value * 2;

									Console.WriteLine("Received value " + value + " For object " + id);

									using (StreamWriter sw = new StreamWriter("Log.txt",true))
									{
										sw.WriteLine(DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + ": " + StaticClass.Servers[id].Name + ", " + value);
									}
								}
							}
							catch (Exception)
							{
								//MessageBox.Show(e.ToString());
							}

						}
					}, null);
				}
			});

			listeningThread.IsBackground = true;
			listeningThread.Start();
		}
	}
}
