using System;

namespace PZ3_NetworkService.ViewModel
{
	public class TutorialViewModel : BindableBase
	{
		public MyICommand NetworkDataCommand { get; set; }
		public MyICommand ReportCommand { get; set; }
		public MyICommand GraphCommand { get; set; }
		public MyICommand NetworkViewCommand { get; set; }

		public TutorialViewModel()
		{
			NetworkDataCommand = new MyICommand(OnNetwork);
			ReportCommand = new MyICommand(OnReport);
			GraphCommand = new MyICommand(OnGraph);
			NetworkViewCommand = new MyICommand(OnNetworkView);
		}

		private void OnNetworkView()
		{
			Console.WriteLine("NetworkView");
		}

		private void OnGraph()
		{
			Console.WriteLine("Graph");
		}

		private void OnReport()
		{
			Console.WriteLine("REPORT");
		}

		private void OnNetwork()
		{
			Console.WriteLine("NETWORK");
		}
	}
}
