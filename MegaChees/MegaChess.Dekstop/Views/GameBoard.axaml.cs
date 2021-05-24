using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MegaChess.Dekstop.ViewModels;
using System.Linq;

namespace MegaChess.Dekstop.Views
{
	public partial class GameBoard : UserControl
	{
		private Rect size;
		private Canvas canvas;
		public GameBoard()
		{
			InitializeComponent();
			this.canvas = this.FindControl<Canvas>("Holst");
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		
		private void ChangeSize(object sender , AvaloniaPropertyChangedEventArgs e)
		{
			if(this.size != this.canvas.Bounds)
			{
				double horisontal = 0;
				double vertical = 0;
				short count = 0;
				foreach (var item in this.canvas.Children)
				{
					count++;
					((Border)item).Width = this.canvas.Bounds.Width / 8;
					((Border)item).Height = this.canvas.Bounds.Height / 8;
					((Border)item).Margin = new Thickness(horisontal, vertical);
					horisontal += this.canvas.Bounds.Width / 8;
					if(count % 8 == 0)
					{
						vertical+= this.canvas.Bounds.Height / 8;
						horisontal = 0;
					}
				}
			}
			this.size = this.canvas.Bounds;
		}
	}
}
