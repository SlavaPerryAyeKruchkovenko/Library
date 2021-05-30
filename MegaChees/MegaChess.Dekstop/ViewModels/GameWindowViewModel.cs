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
			this.game = new Game(new Drawer(this.Borders , this.Figura));
			//this.SelectFigura = ReactiveCommand.Create(new Action<Figura>((x) =>
			//{
			//	SelectedFigura = x;
			//}));
			this.game.StartGame();

		}
		private Figura figura1;
		public Figura Figura
		{
			get => figura1;
			set => this.RaiseAndSetIfChanged(ref figura1, value);
		}

		private readonly Game game;
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
			this.Figura = (Figura)border.DataContext;
			border.BorderBrush = Brushes.Yellow;
		}

		//public ReactiveCommand<Figura, Unit> SelectFigura { get; }
		
	}
}
