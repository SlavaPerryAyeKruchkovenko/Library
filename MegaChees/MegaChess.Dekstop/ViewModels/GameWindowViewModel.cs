using MegaChess.Logic;
using System.Collections.ObjectModel;
using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.Converter;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Reactive.Subjects;
using System.Linq;
using Avalonia.Layout;
using System.Collections.Generic;

namespace MegaChess.Dekstop.ViewModels
{
	public class GameWindowViewModel : ViewModelBase
	{
		public GameWindowViewModel()
		{
			var board = new Board();
			this.GameBorders = CreateBorders(board);
			this.WhiteDiedBorders = CreateDiedFigures(board, true);
			this.BlackDiedBorders = CreateDiedFigures(board, false);

			this.figura = new Subject<Figura>();			

			board = null;
			ObservableCollection<Border>[] bordersArray =
			{
				GameBorders,WhiteDiedBorders,BlackDiedBorders
			};
			this.game = new DekstopGameLogic(new Drawer(bordersArray , this.figura));
			//this.SelectFigura = ReactiveCommand.Create(new Action<Figura>((x) =>
			//{
			//	SelectedFigura = x;
			//}));
			this.game.StartGame();

		}
		private Subject<Figura> figura;

		private readonly DekstopGameLogic game;
		private ObservableCollection<Border> GameBorders { get; }
		public ObservableCollection<Border> BlackDiedBorders { get; }
		public ObservableCollection<Border> WhiteDiedBorders { get; }
		private ObservableCollection<Border> CreateBorders(Board board)
		{		
			var borders = new ObservableCollection<Border>();
			foreach (var item in board.GetFiguras())
			{
				var border = new Border();
				border.Tapped += SelectFigura;
				border.BorderThickness = new Thickness(5);
				border.Background = Field.GetColor(item, board);
				borders.Add(border);
			}		
			return borders;
		}
		private ObservableCollection<Border> CreateDiedFigures(Board board, bool isWhite)
		{
			var borders = new ObservableCollection<Border>();
			foreach (var item in board.GetDiedFiguras(isWhite))
			{
				var border = new Border
				{
					BorderBrush = Field.GetNoReferenceColor(!isWhite),
					Background = Field.GetNoReferenceColor(isWhite),
					BorderThickness = new Thickness(5)
			};
				borders.Add(border);
			}
			return borders;
		}
		private void SelectFigura(object sender, RoutedEventArgs e)
		{		
			var border = sender as Border;
			var figura = (Figura)border.DataContext;
			this.figura.OnNext(figura);

			if (figura.IsMyFigura != null)
				border.BorderBrush = Brushes.Yellow;
		}
		public ReadOnlyObservableCollection<Label> CreatePanel(char a , char b)
		{
			var collection = new ObservableCollection<Label>();
			for (char i = a; i <= b; i++)
			{
				var label = new Label()
				{
					Background = Brushes.Transparent,
					Content = i.ToString(),
					VerticalAlignment = VerticalAlignment.Stretch,
					HorizontalAlignment = HorizontalAlignment.Stretch,
					VerticalContentAlignment = VerticalAlignment.Center,
					HorizontalContentAlignment = HorizontalAlignment.Center
				};
				collection.Add(label);
			}
			return new ReadOnlyObservableCollection<Label>(collection);
		}
		//public ReactiveCommand<Figura, Unit> SelectFigura { get; }
		
	}
}
