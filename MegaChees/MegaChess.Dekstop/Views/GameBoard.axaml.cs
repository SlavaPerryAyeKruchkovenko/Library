using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MegaChess.Dekstop.ViewModels;

namespace MegaChess.Dekstop.Views
{
	public partial class GameBoard : UserControl
	{
		private Rect size;
		private readonly GameWindowViewModel GameWindow;
		public GameBoard()
		{
			InitializeComponent();
			this.GameWindow = new GameWindowViewModel();
			this.FindControl<ItemsControl>("Cells").Items = this.GameWindow.Borders;
		}		
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void ChangeSize(object sender, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.size != (sender as Canvas).Bounds)
			{
				var canvas = sender as Canvas;
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
				this.size = canvas.Bounds;
			}
		}
	}
}
