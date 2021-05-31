using MegaChess.Logic;
using System.Collections.ObjectModel;
using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.Converter;
using ReactiveUI;
using System.Reactive;
using System;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Reactive.Subjects;

namespace MegaChess.Dekstop.ViewModels
{
	public class GameWindowViewModel : ViewModelBase
	{
		public GameWindowViewModel()
		{
			this.Borders = CreateBorders();
			this.figura = new Subject<Figura>();
			
			this.figura.OnNext(new Empty(null,34));
			this.game = new DekstopGameLogic(new Drawer(this.Borders , this.figura));
			//this.SelectFigura = ReactiveCommand.Create(new Action<Figura>((x) =>
			//{
			//	SelectedFigura = x;
			//}));
			this.game.StartGame();

		}
		private Subject<Figura> figura;

		private readonly DekstopGameLogic game;
		public ObservableCollection<Border> Borders { get; }
		private ObservableCollection<Border> CreateBorders()
		{
			var borders = new ObservableCollection<Border>();
			for (int i = 0; i < 64; i++)
			{
				var border = new Border();
				border.Tapped += SelectFigura;
				border.BorderThickness = new Thickness(5);
				borders.Add(border);
			}
			return borders;
		}
		private void SelectFigura(object sender, RoutedEventArgs e)
		{
			var border = sender as Border;
			this.figura.OnNext((Figura)border.DataContext);
			border.BorderBrush = Brushes.Yellow;
		}

		//public ReactiveCommand<Figura, Unit> SelectFigura { get; }
		
	}
}
