using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PZ3_NetworkService.StaticClasses;

namespace PZ3_NetworkService.ViewModel
{
	public class DataChartViewModel : BindableBase
	{
		private ObservableCollection<Rectangle> s_rectangles;

		public MyICommand ShowButtonCommand { get; set; }

		public ObservableCollection<Rectangle> Rectangles
		{
			get
			{
				return s_rectangles;
			}
			set
			{
				s_rectangles = value;
				OnPropertyChanged("Rectangles");
			}
		}
		public DataChartViewModel()
		{
			ShowButtonCommand = new MyICommand(OnShow);
			Rectangles = StaticClass.Rectangles;
		}

		public void OnShow()
		{
			Rectangles.Clear();
			for (int i = 0 ; i < StaticClass.Servers.Count ; i++)
			{
				Rectangles.Add(new Rectangle() { Fill = Brushes.Aqua, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(3), Width = 20, Height = StaticClass.Servers[i].Value });
			}
		}
	}
}