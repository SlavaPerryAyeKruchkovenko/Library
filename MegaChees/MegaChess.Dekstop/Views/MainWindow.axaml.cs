using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MegaChess.Dekstop.ViewModels;

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
			ShowGameWindow(false, false);			
		}
		private void ShowGameWindow(bool vsComputer , bool isLoadGame)
		{			
			this.DataContext = new MainWindowViewModel(new bool[] { vsComputer , isLoadGame});
			this.Close();
		}
	}
}
