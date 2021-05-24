using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using MegaChess.Logic;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.Converter;

namespace MegaChess.Dekstop.ViewModels
{
	public class GameWindowViewModel : ViewModelBase
	{
		public ObservableCollection<FiguraProperty> GameBoard { get; }

		private Game game;
		public GameWindowViewModel()
		{
			this.game = new Game(new Drawer(this.GameBoard));
			this.game.StartGame();
		}
	}
}
