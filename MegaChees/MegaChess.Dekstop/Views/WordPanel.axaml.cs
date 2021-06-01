using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
				foreach (var item in canvas.Children)
				{
					((Label)item).Width = this.Width / 8;
					((Label)item).Height = this.Height;
				}
				this.size = canvas.Bounds;
			}
		}
	}
}
