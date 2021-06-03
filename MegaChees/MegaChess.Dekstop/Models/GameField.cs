using Avalonia.Media;
using MegaChess.Logic;
using System;

namespace MegaChess.Dekstop.Models
{
	static class GameField
	{
		private static IBrush firstColor = Brushes.White;
		private static IBrush secondColor = Brushes.Black;
		private static IBrush thirthColor = Brushes.Red;
		private static bool[] setting = Array.Empty<bool>();
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
	}
}
