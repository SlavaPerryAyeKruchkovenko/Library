using Avalonia.Media;
using MegaChess.Logic;

namespace MegaChess.Dekstop.Models
{
	static class Field
	{
		private static IBrush firstColor = Brushes.White;
		private static IBrush secondColor = Brushes.Black;
		private static IBrush thirthColor = Brushes.Red;
		public static IBrush GetColor(Figura figura, Board board)
		{
			var lenght = board.FoundFigureCoordinate(figura);
			return (lenght[0] + lenght[1]) % 2 == 0 ? firstColor : secondColor;
		}
		public static IBrush GetBorderBrushesColor() => thirthColor;
	}
}
