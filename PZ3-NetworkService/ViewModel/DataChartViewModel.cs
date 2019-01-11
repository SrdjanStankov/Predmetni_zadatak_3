using System.Collections.ObjectModel;
using PZ3_NetworkService.Model;
using PZ3_NetworkService.StaticClasses;

namespace PZ3_NetworkService.ViewModel
{
	public class DataChartViewModel : BindableBase
	{
		public MyICommand ShowButtonCommand { get; set; }
		public ObservableCollection<MyRect> Rectangles { get; set; }

		public DataChartViewModel()
		{
			ShowButtonCommand = new MyICommand(OnShow);
			Rectangles = StaticClass.Rectangles;
		}

		public void OnShow()
		{
			//Rectangles.Clear();
			//for (int i = 0 ; i < StaticClass.Servers.Count ; i++)
			//{
			//	Rectangles.Add(new Rectangle() { Fill = Brushes.Aqua, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(3), Width = 20, Height = StaticClass.Servers[i].Value });
			//}
		}
	}
}