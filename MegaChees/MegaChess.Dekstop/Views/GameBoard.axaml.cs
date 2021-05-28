using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using MegaChess.Dekstop.Converter;
using MegaChess.Dekstop.ViewModels;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using System.Threading;

namespace MegaChess.Dekstop.Views
{
	public partial class GameBoard : UserControl
	{
		private Rect size;
		public GameBoard()
		{
			InitializeComponent();
		}		
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void ChangeSize(object sender , AvaloniaPropertyChangedEventArgs e)
		{
			var canvas = sender as Canvas;
			
			if (this.size != canvas.Bounds && canvas.Children.Count != 0)
			{
				double horisontal = 0;
				double vertical = 0;
				short count = 0;
				foreach (var item in canvas.Children)
				{
					if (item is Border border)
					{
						
						count++;
						border.Width = canvas.Bounds.Width / 8;
						border.Height = canvas.Bounds.Height / 8;
						border.Margin = new Thickness(horisontal, vertical);
						horisontal += canvas.Bounds.Width / 8;
						if (count % 8 == 0)
						{
							vertical += canvas.Bounds.Height / 8;
							horisontal = 0;
						}
					}
				}
			}
			this.size = canvas.Bounds;
		}	
	}
}
