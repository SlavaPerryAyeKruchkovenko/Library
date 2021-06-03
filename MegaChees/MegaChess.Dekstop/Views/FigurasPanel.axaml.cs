using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using MegaChess.Dekstop.Converter;
using MegaChess.Dekstop.Models;
using MegaChess.Logic;
using System;
using System.Linq;

namespace MegaChess.Dekstop.Views
{
	public partial class FigurasPanel : Window
	{
		public enum WhiteFiguras
		{
			White_Elefant = 0,
			White_Knight,
			White_Queen,
			White_Rook
		}
		public enum BlackFiguras
		{
			Black_elefant = 0,
			Black_knight,
			Black_Queen,
			Black_Rook
		}
		public FigurasPanel()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}
		private readonly IObserver<Figura> figura;
		public FigurasPanel(bool IswhiteMove , short[] counts , IObserver<Figura> observer)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.figura = observer;
			this.HasSystemDecorations = false;
			CreatePanel(this.FindControl<DockPanel>("Figuras"), IswhiteMove, counts);
		}
		private void CreatePanel(DockPanel panel , bool isWhite , short[] counts)
		{
			int i = 0;
			string[] figuras;
			if (isWhite)
			{
				figuras = (string[])Enum.GetNames(typeof(WhiteFiguras));
			}				
			else
			{
				figuras = (string[])Enum.GetNames(typeof(BlackFiguras));
			}
			foreach (var item in panel.Children)
			{
				var border = item as Border;
				border.Background = GameField.GetNoReferenceColor(isWhite);
				border.Width = 200;
				border.Height = 200;
				border.Child = new Image()
				{
					Source = (Bitmap)new ImageConverter().Convert(figuras[i]+".png", null, null, null)
				};
				border.DataContext = GetFigura(figuras[i], isWhite, counts);
				border.Tapped += SelectFigura;
				i++;
			}

		}
		private static Figura GetFigura(string name ,bool isWhite , short[] counts)
		{
			if (name.ToLower().Contains("queen"))
				return new Queen(isWhite, counts[0]);
			else if (name.ToLower().Contains("rook"))
				return new Rook(isWhite, counts[1]);
			else if (name.ToLower().Contains("elefant"))
				return new Bishop(isWhite, counts[2]);
			else
				return new Knight(isWhite, counts[3]);
		}
		private void SelectFigura(object sender , RoutedEventArgs e)
		{
			var border = sender as Border;
			this.figura.OnNext((Figura)border.DataContext);
			this.Close();
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
