﻿namespace PZ3_NetworkService.Model
{
	public class Server : ValidationBase
	{
		private int id;
		private string name;
		private string ipAddress;
		private string imgSrc;
		private double value;

		public Server() { }

		public Server(Server obj)
		{
			id = obj.id;
			name = obj.name;
			ipAddress = obj.ipAddress;
			imgSrc = obj.imgSrc;
			value = obj.value;
		}

		public int Id
		{
			get => id;

			set
			{
				if (id != value)
				{
					id = value;
					OnPropertyChanged("Id");
				}
			}
		}

		public string Name
		{
			get => name;

			set
			{
				if (name != value)
				{
					name = value;
					OnPropertyChanged("Name");
				}
			}
		}

		public string IpAddress
		{
			get => ipAddress;

			set
			{
				if (ipAddress != value)
				{
					ipAddress = value;
					OnPropertyChanged("IpAddress");
				}
			}
		}

		public string ImgSrc
		{
			get => imgSrc;

			set
			{
				if (imgSrc != value)
				{
					imgSrc = value;
					OnPropertyChanged("ImgSrc");
				}
			}
		}

		public double Value
		{
			get => value;

			set
			{
				if (this.value != value)
				{
					this.value = value;
					OnPropertyChanged("Value");
				}
			}
		}

		public override string ToString()
		{
			return Name;
		}

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(imgSrc))
			{
				ValidationErrors["ImgSrc"] = "Slika nedostaje.";
			}

			if (string.IsNullOrWhiteSpace(ipAddress))
			{
				ValidationErrors["IpAddress"] = "IP addresa nedostaje";
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				ValidationErrors["Name"] = "Ime nedostaje";
			}

			if (id <= 0)
			{
				ValidationErrors["Id"] = "ID mora biti veci od 0";
			}
		}
	}
}
