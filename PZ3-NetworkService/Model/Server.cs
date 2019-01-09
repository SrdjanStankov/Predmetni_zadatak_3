namespace PZ3_NetworkService.Model
{
	public class Server : ValidationBase
	{
		private int id;
		private string name;
		private string ipAddress;
		private string imgSrc;
		private double value;

		public Server()
		{
		}

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
			get
			{
				return id;
			}

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
			get
			{
				return name;
			}

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
			get
			{
				return ipAddress;
			}

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
			get
			{
				return imgSrc;
			}

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
			get
			{
				return value;
			}

			set
			{
				if (this.value != value)
				{
					this.value = value;
					OnPropertyChanged("Value");
				}
			}
		}

		protected override void ValidateSelf()
		{
			if (string.IsNullOrWhiteSpace(imgSrc))
			{
				ValidationErrors["ImgSrc"] = "Image source is required.";
			}

			if (string.IsNullOrWhiteSpace(ipAddress))
			{
				ValidationErrors["IpAddress"] = "Ip address is required";
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				ValidationErrors["Name"] = "Name is required";
			}

			if (id <= 0)
			{
				ValidationErrors["Id"] = "Id must be greater than '0'";
			}
		}
	}
}
