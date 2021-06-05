using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class NumsPanel : UserControl
	{
		public NumsPanel()
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
				double vertical = 0;
				foreach (var item in canvas.Children)
				{
					((Label)item).FontSize = canvas.Bounds.Width / 2;
					((Label)item).Width = canvas.Bounds.Width;
					((Label)item).Height = canvas.Bounds.Height / 8;
					((Label)item).Margin = new Thickness(0, vertical);
					vertical += canvas.Bounds.Height / 8;
				}
				this.size = canvas.Bounds;
			}
		}
	}
}
