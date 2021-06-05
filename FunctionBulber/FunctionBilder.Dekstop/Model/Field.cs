using Avalonia.Media;


namespace FunctionBilder.Dekstop.Model
{
	static class Field
	{
		public readonly static short FontSize = 6;
		public readonly static short NormalVizualRangeForLabel = -10;
		public readonly static short StandartScale = 1;
		public readonly static short BeautifulScale = 40;
		public readonly static IBrush StandartPointColor = Brushes.Red;
		public readonly static IBrush StandartLineColor = Brushes.Yellow;
		public readonly static short LineScale = 5;
		public readonly static double Ratio = 0.4;
		public static IBrush[] StandartGraphicColor()
		{
			return new IBrush[] { StandartPointColor, StandartLineColor };
		}
	}
}
