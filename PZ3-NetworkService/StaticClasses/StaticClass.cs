using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using PZ3_NetworkService.Model;

namespace PZ3_NetworkService.StaticClasses
{
	public class StaticClass
	{
		private const int IpAddressNum = 9;

		public static ObservableCollection<Server> Servers { get; set; }
		public static ObservableCollection<Rectangle> Rectangles { get; set; }
		public static ObservableCollection<string> IpAddresses { get; set; }

		private static NetworkManagment nm = new NetworkManagment();

		static StaticClass()
		{
			Servers = new ObservableCollection<Server>();
			Rectangles = new ObservableCollection<Rectangle>();
			nm.CreateListener();
			LoadIps();
		}

		public static void LoadIps()
		{
			ObservableCollection<string> temp = new ObservableCollection<string>();
			Random r = new Random();
			string ip = "";

			temp.Add("127.0.0.1");

			for (int i = 0 ; i < IpAddressNum ; i++)
			{
				ip += r.Next(127, 192);
				ip += ".";
				ip += r.Next(255).ToString();
				ip += ".";
				ip += r.Next(255).ToString();
				ip += ".";
				ip += r.Next(255).ToString();

				temp.Add(ip);
				ip = "";
			}
			IpAddresses = temp;
		}
	}
}
