using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using PZ3_NetworkService.Model;
using PZ3_NetworkService.StaticClasses;

namespace PZ3_NetworkService.ViewModel
{
	public class NetworViewViewModel : BindableBase, IDropTarget
	{
		public ObservableCollection<Server> ServersToShow { get; set; }
		public ObservableCollection<Server> Destination { get; set; }

		public NetworViewViewModel()
		{
			ServersToShow = new ObservableCollection<Server>(StaticClass.Servers);
			Destination = new ObservableCollection<Server>();
		}

		public void DragOver(IDropInfo dropInfo)
		{
			if (dropInfo.Data != null && dropInfo.TargetItem != null)
			{
				dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
				dropInfo.Effects = DragDropEffects.Copy;
			}
		}

		public void Drop(IDropInfo dropInfo)
		{
			//ServerItemViewModel sourceItem = dropInfo.Data as ServerItemViewModel;
			//ServerItemViewModel targetItem = dropInfo.TargetItem as ServerItemViewModel;
			//targetItem.Add(sourceItem);
		}
	}
}
