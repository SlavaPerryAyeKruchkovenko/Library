using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using MegaChess.Logic;
using System.Collections.ObjectModel;
using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.Converter;
using ReactiveUI;
using System.Reactive;
using System;

namespace MegaChess.Dekstop.ViewModels
{
	public class GameWindowViewModel : ViewModelBase
	{
		public GameWindowViewModel()
		{
			this.game = new Game(new Drawer(this.GameBoard));
			this.game.StartGame();
			this.SelectFigura = ReactiveCommand.Create(new Action<Figura>((x) =>
			{
				SelectedFigura = x;
			}));
		}
		public ObservableCollection<СellProperty > GameBoard { get; }
		public ReactiveCommand<Figura, Unit> SelectFigura { get; }
		public static Figura SelectedFigura { get; private set; }

		private readonly Game game;

		
		
		
    }
}
