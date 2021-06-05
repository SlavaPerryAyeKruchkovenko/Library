using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MegaChess.Dekstop.ViewModels;
using System.ComponentModel;
using Avalonia.Interactivity;

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
			this.FindControl<UserControl>("ChessBoard").DataContext = this.gameWindow;
			this.FindControl<UserControl>("WordPanel").FindControl<ItemsControl>("Words").Items = this.gameWindow.CreatePanel('A', 'H');
			this.FindControl<UserControl>("WordPanel2").FindControl<ItemsControl>("Words").Items =this.gameWindow.CreatePanel('A', 'H');
			this.FindControl<UserControl>("NumsPanel").FindControl<ItemsControl>("Nums").Items = this.gameWindow.CreatePanel('1', '8');
			this.FindControl<UserControl>("NumsPanel2").FindControl<ItemsControl>("Nums").Items = this.gameWindow.CreatePanel('1', '8');
			this.FindControl<UserControl>("WhiteFiguras").FindControl<ItemsControl>("DiedFiguras").Items = this.gameWindow.WhiteDiedBorders;
			this.FindControl<UserControl>("BlackFiguras").FindControl<ItemsControl>("DiedFiguras").Items = this.gameWindow.BlackDiedBorders;
		}
		GameWindowViewModel gameWindow = new GameWindowViewModel();
		protected override void OnClosing(CancelEventArgs e)
		{
			this.gameWindow.CloseGame();
			OnClosed(e);
		} 
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void CloseWindow(object sender , RoutedEventArgs e)
		{
			this.gameWindow.CloseGame();
			this.Close();
		}
		private void OpenMenu(object sender, RoutedEventArgs e)
		{
			var window = new MainWindow();
			window.Show();
			this.gameWindow.CloseGame();
			this.Close();
		}
		private void OpenSetting(object sender, RoutedEventArgs e)
		{
			var window = new SettingWindow();
			window.Show();
		}
	}
}
