using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
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
		private void OpenGame(object sender , RoutedEventArgs e)
		{
			ShowGameWindow();			
		}
		private void ShowGameWindow()
		{
			var game = new GameWindow();
			//game.Topmost = true;
			game.Show();
		}
	}
}
