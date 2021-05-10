using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class GameWindow : Window
	{
		public GameWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
