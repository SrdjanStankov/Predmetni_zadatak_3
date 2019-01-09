using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using PZ3_NetworkService.Model;

namespace PZ3_NetworkService.ViewModel
{
	public class ReportViewModel : BindableBase
	{
		public MyICommand ShowReportCommand { get; set; }
		public string ReportShow { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }

		public ReportViewModel()
		{
			ShowReportCommand = new MyICommand(ShowReport);
			StartDate = "";
			EndDate = "";
		}

		private void ShowReport()
		{
			try
			{
				ReportShow = "";

				string[] startDat = StartDate.Split('.');
				string[] endDat = EndDate.Split('.');

				#region checking combobox
				if (startDat.Length != 3)
				{
					MessageBox.Show("Invalid Starting date. Format is dd.mm.yyyy");
					return;
				}
				if (endDat.Length != 3)
				{
					MessageBox.Show("Invalid End date. Format is dd.mm.yyyy");
					return;
				}

				if (!int.TryParse(startDat[0], out int startD))
				{
					MessageBox.Show("Invalid Starting date. Format is dd.mm.yyyy");
					return;
				}
				if (!int.TryParse(startDat[1], out int startM))
				{
					MessageBox.Show("Invalid Starting date. Format is dd.mm.yyyy");
					return;
				}
				if (!int.TryParse(startDat[2], out int startY))
				{
					MessageBox.Show("Invalid Starting date. Format is dd.mm.yyyy");
					return;
				}

				if (!int.TryParse(endDat[0], out int endD))
				{
					MessageBox.Show("Invalid End date. Format is dd.mm.yyyy");
					return;
				}
				if (!int.TryParse(endDat[1], out int endM))
				{
					MessageBox.Show("Invalid End date. Format is dd.mm.yyyy");
					return;
				}
				if (!int.TryParse(endDat[2], out int endY))
				{
					MessageBox.Show("Invalid End date. Format is dd.mm.yyyy");
					return;
				}
				#endregion

				DateTime startDate = new DateTime(startY,startM,startD);
				DateTime endDate = new DateTime(endY, endM, endD);

				if (startDate > endDate)
				{
					MessageBox.Show("Starting date is larger than ending date.");
				}

				SortedDictionary<string, string> reports = new SortedDictionary<string, string>();

				using (StreamReader sr = new StreamReader("Log.txt"))
				{
					while (!sr.EndOfStream)
					{
						string temp = sr.ReadLine();
						string[] vals = temp.Split(',', ':');
						DateTime date = Convert.ToDateTime(vals[0]);
						if (date >= startDate && date <= endDate)
						{
							string message = "\t" + vals[0] + " " + vals[1] + ", CHANGED STATE: " + vals[3] + Environment.NewLine;
							if (!reports.ContainsKey(vals[2]))
							{
								reports.Add(vals[2], message);
							}
							else
							{
								reports[vals[2]] += message;
							}
						}
					}
				}

				ReportShow = "REPORT:" + Environment.NewLine;
				foreach (KeyValuePair<string, string> item in reports)
				{
					ReportShow += item.Key + ":" + Environment.NewLine + item.Value;
					OnPropertyChanged("ReportShow");
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
	}
}
