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
		private bool vsComputer;
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}		
		private void ShowGameWindow(bool vsComputer , bool isLoadGame)
		{			
			this.DataContext = new MainWindowViewModel(new bool[] { vsComputer , isLoadGame});
			this.Close();
		}
		private void NewGame(object sender, RoutedEventArgs e)
		{
			ShowGameWindow(this.vsComputer, true);
		}
		private void LoadGame(object sender, RoutedEventArgs e)
		{
			ShowGameWindow(this.vsComputer, false);
		}
		private void PlayVsComputer(object sender, RoutedEventArgs e)
		{
			this.vsComputer = true;
		}
		private void PlayVsPeople(object sender, RoutedEventArgs e)
		{
			this.vsComputer = false;
		}
	}
}
