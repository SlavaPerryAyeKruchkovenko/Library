
using MegaChess.Dekstop.Views;

namespace MegaChess.Dekstop.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(bool[] parametrs)
		{
			OpenGameWindow(parametrs[0], parametrs[1]);
		}
		public MainWindowViewModel()
		{
			
		}
		private static void OpenGameWindow(bool vsCompute , bool isLoadGame)
		{
			var game = new GameWindow();
			game.Show();
		}
	}
}
