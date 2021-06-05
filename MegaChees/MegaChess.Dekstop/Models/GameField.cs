using Avalonia.Media;
using MegaChess.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MegaChess.Dekstop.Models
{
	static class GameField
	{
		private static IBrush firstColor = Brushes.White;
		public static IBrush secondColor = Brushes.Black;
		public static IBrush thirthColor = Brushes.Red;
		private static bool[] setting = Array.Empty<bool>();
		private static Dictionary<string, Dictionary<short, string>> images = new Dictionary<string, Dictionary<short, string>>()
		{
				{ "White", new Dictionary<short, string> { { 1, "White_Pawn.png" }, { 2, "White_Rook.png" }, { 3, "White_Knight.png" }, { 4, "White_Elefant.png" }, { 5, "White_Queen.png" }, {6 , "White_King.png" } } },
				{ "Red", new Dictionary<short, string> { { 1, "Red_Pawn.png" }, { 2, "Red_Rook.png" }, { 3, "Red_Knight.png" }, { 4, "Red_Elefant.png" }, { 5, "Red_Queen.png" }, { 6, "Red_King.png" } } }
		};
		private static string imagesKey = "White";
		public static string ImageKey
		{
			get { return imagesKey; }
			set
			{
				foreach (var item in images)
				{
					if (item.Key == value)
					{
						imagesKey = value;
					}					
				}
			}
		}
		public static void SetColor(IBrush color , int numColor)
		{
			if (color is IBrush && color != null)
			{
				switch(numColor)
				{
					case (1): firstColor = color;break;
					case (2): secondColor = color; break;
					case (3): thirthColor = color; break;
					default: throw new Exception("Такого поля нету");
				}
			}
				
		}
		public static List<string> GetColors()
		{
			var colors = new List<string>();
			foreach (var item in images)		
				colors.Add(item.Key);

			return colors;
		}		
		public static bool[] GetSettings()
		{
			if (setting.Length != 2)
				return new bool[] { true, true };
			else
				return setting;
		}
		public static void SetSettings(bool vsComputer, bool isNewGame)
		{
			setting = new bool[] { vsComputer, isNewGame };
		}
		public static IBrush GetColor(Figura figura, Board board)
		{			
			var lenght = board.FoundFigureCoordinate(figura);				

			return (lenght[0] + lenght[1]) % 2 == 0 ? secondColor : firstColor;
		}
		public static IBrush GetNoReferenceColor(bool isWhite) => isWhite ? firstColor : secondColor;
		public static IBrush GetBorderBrushesColor() => thirthColor;
		public static string GetEnemyFiguraImage(Figura figura)
		{
			return figura.ToString().ToUpper() switch
			{
				"P" => "Black_Pawn.png",
				"R" => "Black_Rook.png",
				"H" => "Black_Knight.png",
				"B" => "Black_Elefant.png",
				"Q" => "Black_Queen.png",
				"K" => "Black_King.png",
				_ => "",
			};
		}
		public static string GetMyFiguraImage(Figura figura)
		{
			return figura.ToString().ToUpper() switch
			{
				"P" => images[imagesKey][1],
				"R" => images[imagesKey][2],
				"H" => images[imagesKey][3],
				"B" => images[imagesKey][4],
				"Q" => images[imagesKey][5],
				"K" => images[imagesKey][6],
				_ => "",
			};
		}
	}
}
