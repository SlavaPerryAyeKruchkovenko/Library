using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FunctionBilder.Dekstop.View
{
	public class GraphicWindow : Window
	{
		public GraphicWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			
		}
		public GraphicWindow(Thickness cursor)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.Margin = cursor;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
