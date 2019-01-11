using System;
using System.Collections.ObjectModel;
using PZ3_NetworkService.Model;
using PZ3_NetworkService.StaticClasses;

namespace PZ3_NetworkService.ViewModel
{
	public class NetworkDataViewModel : BindableBase
	{
		public ObservableCollection<Server> ServersToShow { get; set; }
		public MyICommand AddServerCommand { get; set; }
		public MyICommand DeleteServerCommand { get; set; }
		public MyICommand FindServerCommand { get; set; }
		public string ImgSrc { get; set; }

		private Server currentServer;
		private string selectedIp;
		private Server selectedServer;
		private Server filterServer;

		public bool Lt { get; set; }
		public bool Gt { get; set; }

		public NetworkManagment nm;

		public NetworkDataViewModel()
		{
			ServersToShow = StaticClass.Servers;
			ImgSrc = Environment.CurrentDirectory + "\\server_PNG20.png";
			currentServer = new Server
			{
				ImgSrc = ImgSrc
			};
			AddServerCommand = new MyICommand(OnAdd);
			DeleteServerCommand = new MyICommand(OnDelete);

			Lt = true;
			FilterServer = new Server()
			{
				ImgSrc = ImgSrc
			};
			FindServerCommand = new MyICommand(OnFilter);
		}

		private void OnFilter()
		{
			ObservableCollection<Server> finded = new ObservableCollection<Server>();

			foreach (Server item in StaticClass.Servers)
			{
				if (item.IpAddress == FilterServer.IpAddress)
				{
					if (Lt)
					{
						if (item.Id < FilterServer.Id)
						{
							finded.Add(item);
						}
					}
					else
					{
						if (item.Id > FilterServer.Id)
						{
							finded.Add(item);
						}
					}
				}
			}

			ServersToShow = finded;
			OnPropertyChanged("ServersToShow");
		}

		public ObservableCollection<string> IpAddresses
		{
			get
			{
				return StaticClass.IpAddresses;
			}
		}

		public Server FilterServer
		{
			get
			{
				return filterServer;
			}
			set
			{
				filterServer = value;
				OnPropertyChanged("FilterServer");
			}
		}

		private void OnDelete()
		{
			selectedServer.Validate();
			if (CheckIfExist(selectedServer))
			{
				int idx = StaticClass.Servers.IndexOf(selectedServer);
				StaticClass.Servers.RemoveAt(idx);
				StaticClass.Rectangles.RemoveAt(idx);
			}
		}

		private void OnAdd()
		{
			CurrentServer.Validate();
			if (CurrentServer.IsValid)
			{
				if (!CheckIfExist(currentServer))
				{
					StaticClass.Servers.Add(new Server(currentServer));
					StaticClass.Rectangles.Add(new MyRect());
				}
			}
		}

		private bool CheckIfExist(Server currentServer)
		{
			foreach (Server item in StaticClass.Servers)
			{
				if (item.Id == currentServer.Id)
				{
					return true;
				}
			}
			return false;
		}

		public Server CurrentServer
		{
			get
			{
				return currentServer;
			}
			set
			{
				currentServer = value;
				OnPropertyChanged("CurrentServer");
			}
		}

		public string SelectedIp
		{
			get
			{
				return selectedIp;
			}

			set
			{
				selectedIp = value;
				CurrentServer.IpAddress = value;
				OnPropertyChanged("CurrentServer");
				OnPropertyChanged("SelectedIp");
			}
		}

		public Server SelectedServer
		{
			get
			{
				return selectedServer;
			}

			set
			{
				selectedServer = value;
				OnPropertyChanged("selectedServer");
			}
		}


	}
}
