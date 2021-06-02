using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class DeadFiguresBoard : UserControl
	{
		public DeadFiguresBoard()
		{
			InitializeComponent();
		}
		private Rect size;
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
						border.Width = canvas.Bounds.Width / 2;
						border.Height = canvas.Bounds.Height / 8;
						border.Margin = new Thickness(horisontal, vertical);
						horisontal += canvas.Bounds.Width / 2;
						if (count % 2 == 0)
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
