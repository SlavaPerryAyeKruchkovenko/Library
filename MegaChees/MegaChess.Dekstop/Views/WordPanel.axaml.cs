using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Layout;

namespace MegaChess.Dekstop.Views
{
	public partial class WordPanel : UserControl
	{
		public WordPanel()
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
			var canvas = sender as Canvas;
			if (this.size != canvas.Bounds)
			{
				double horisontal = 0;
				foreach (var item in canvas.Children)
				{
					((Label)item).Width = canvas.Bounds.Width / 8;
					((Label)item).Height = canvas.Bounds.Height;					
					((Label)item).Margin = new Thickness(horisontal, 0);
					horisontal += canvas.Bounds.Width / 8;
				}
				this.size = canvas.Bounds;
			}
		}
	}
}
