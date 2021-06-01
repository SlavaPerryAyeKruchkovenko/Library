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
			this.FindControl<UserControl>("WordPanel").FindControl<ItemsControl>("Words").Items = gameWindow.CreatePanel('A', 'H');
			this.FindControl<UserControl>("WordPanel2").FindControl<ItemsControl>("Words").Items = gameWindow.CreatePanel('A', 'H');
			this.FindControl<UserControl>("NumsPanel").FindControl<ItemsControl>("Nums").Items = gameWindow.CreatePanel('1', '8');
			this.FindControl<UserControl>("NumsPanel2").FindControl<ItemsControl>("Nums").Items = gameWindow.CreatePanel('1', '8');
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
