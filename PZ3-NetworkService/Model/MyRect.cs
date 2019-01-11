namespace PZ3_NetworkService.Model
{
	public class MyRect : BindableBase
	{
		private int height;

		public int Height
		{
			get
			{
				return height;
			}

			set
			{
				height = value;
				OnPropertyChanged("Height");
			}
		}
	}
}
