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

namespace MegaChess.Dekstop.ViewModels
{
	public class GameWindowViewModel : ViewModelBase
	{
		public GameWindowViewModel()
		{
			this.Borders = CreateBorders();
			this.Labels = CreatePanel('A', 'H');
			this.Nums = CreatePanel('1', '8');

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
		private ObservableCollection<Border> Borders { get; }
		private ReadOnlyObservableCollection<Label> Labels { get; }
		private ReadOnlyObservableCollection<Label> Nums { get; }
		private ObservableCollection<Border> CreateBorders()
		{
			var board = new Board();
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
		private void SelectFigura(object sender, RoutedEventArgs e)
		{
			
			var border = sender as Border;
			var figura = (Figura)border.DataContext;
			this.figura.OnNext(figura);

			if (figura.IsMyFigura != null)
				border.BorderBrush = Brushes.Yellow;
		}
		private ReadOnlyObservableCollection<Label> CreatePanel(char a , char b)
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
