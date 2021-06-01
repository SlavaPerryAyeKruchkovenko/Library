using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MegaChess.Dekstop.ViewModels;

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
			var gameWindow = new GameWindowViewModel();
			this.FindControl<UserControl>("ChessBoard").DataContext = gameWindow;
			this.FindControl<UserControl>("WordPanel").DataContext = gameWindow;
			this.FindControl<UserControl>("WordPanel2").DataContext = gameWindow;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
