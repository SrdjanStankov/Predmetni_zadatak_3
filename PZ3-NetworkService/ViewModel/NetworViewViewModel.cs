using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PZ3_NetworkService.Model;
using PZ3_NetworkService.StaticClasses;

namespace PZ3_NetworkService.ViewModel
{
	public class NetworViewViewModel : BindableBase
	{
		public static Dictionary<int, Server> ServersD { get; set; } = new Dictionary<int, Server>();
		public static Dictionary<int, Server> Dropped { get; set; } = new Dictionary<int, Server>();

		public ObservableCollection<Server> Servers { get; set; } = new ObservableCollection<Server>();

		public static Server draggedItem = null;
		private bool dragging = false;
		private bool fromList = false;

		public MyICommand LeftMBUp { get; set; }
		public MyICommand<ListView> SELCHANGE { get; set; }
		public MyICommand<Canvas> DCommand { get; set; }

		private Server selectedObject;
		public Server SelectedObject { get => selectedObject; set => selectedObject = value; }

		public MyICommand<Canvas> FreeCanvas { get; set; }
		public MyICommand<Canvas> CanSel { get; set; }

		private void RefreshList()
		{
			Servers.Clear();
			foreach (var d in ServersD.Values)
			{
				Servers.Add(d);
			}
		}

		public NetworViewViewModel()
		{
			RefreshList();
			LeftMBUp = new MyICommand(OnMouseLeftButtonUp);
			SELCHANGE = new MyICommand<ListView>(SelectionChange);
			DCommand = new MyICommand<Canvas>(Drop);

			FreeCanvas = new MyICommand<Canvas>(OnRunFreeCanvas);
			CanSel = new MyICommand<Canvas>(CanvasSelect);

			Canvas c = new Canvas();

			foreach (var item in StaticClass.Servers)
			{
				ServersD.Add(item.Id, item);
			}
			RefreshList();
		}

		private void OnRunFreeCanvas(Canvas canvas)
		{
			if (canvas.Resources["taken"] != null)
			{
				char[] delimiters = new char[] { '.' };
				string[] words = ((TextBlock) canvas.Children[0]).Text.Split(delimiters);
				int id = int.Parse(words[0]);

				ServersD[id] = Dropped[id];
				Dropped.Remove(id);
				OnPropertyChanged("Valves");

				RefreshList();

				canvas.Background = Brushes.White;
				((TextBlock) canvas.Children[0]).Text = "Dostupno";
				((TextBlock) canvas.Children[0]).Foreground = Brushes.Black;
				((TextBlock) canvas.Children[0]).Background = Brushes.White;
				canvas.Resources.Remove("taken");
			}
		}

		public void Drop(Canvas c)
		{
			if (draggedItem != null)
			{
				if (c.Resources["taken"] == null)
				{
					BitmapImage logo = new BitmapImage();
					logo.BeginInit();
					logo.UriSource = new Uri(draggedItem.ImgSrc, UriKind.Absolute);
					logo.EndInit();
					c.Background = new ImageBrush(logo);
					ViewImagesStorage.Canvases[c.Name] = draggedItem.ImgSrc;
					((TextBlock) c.Children[0]).Text = draggedItem.Id + "." + draggedItem.Name;
					((TextBlock) c.Children[0]).Foreground = Brushes.Black;
					((TextBlock) c.Children[0]).Background = (SolidColorBrush) new BrushConverter().ConvertFrom("#ffffff");//("#ffff40"));

					c.Resources.Add("taken", true);

					if (fromList)
					{
						Dropped[draggedItem.Id] = ServersD[draggedItem.Id];
						ServersD.Remove(draggedItem.Id);
						OnPropertyChanged("Valves");
						fromList = false;
					}
					else
					{
						EmptyCanvas();
					}
				}
				RefreshList();
				dragging = false;
			}
		}

		private void EmptyCanvas()
		{
			CanvasToEmpty.Background = Brushes.White;
			((TextBlock) CanvasToEmpty.Children[0]).Text = "Dostupno";
			((TextBlock) CanvasToEmpty.Children[0]).Foreground = Brushes.Black;
			((TextBlock) CanvasToEmpty.Children[0]).Background = Brushes.White;
			CanvasToEmpty.Resources.Remove("taken");
		}

		private void OnMouseLeftButtonUp()
		{
			draggedItem = null;
			selectedObject = null;
			dragging = false;
		}

		public void SelectionChange(ListView o)
		{
			if (!dragging)
			{
				fromList = true;
				dragging = true;

				draggedItem = new Server(SelectedObject);

				DragDrop.DoDragDrop(o, draggedItem, DragDropEffects.Copy | DragDropEffects.Move);
			}
		}

		private Canvas CanvasToEmpty;

		private void CanvasSelect(Canvas canvas)
		{
			if (canvas.Resources["taken"] != null)
			{
				char[] delimiters = new char[] { '.' };
				string[] words = ((TextBlock) canvas.Children[0]).Text.Split(delimiters);
				int id = int.Parse(words[0]);

				draggedItem = Dropped[id];

				if (!dragging)
				{
					fromList = false;
					dragging = true;
					CanvasToEmpty = canvas;
					DragDrop.DoDragDrop(canvas, draggedItem, DragDropEffects.Copy | DragDropEffects.Move);
				}
			}
		}
	}
}