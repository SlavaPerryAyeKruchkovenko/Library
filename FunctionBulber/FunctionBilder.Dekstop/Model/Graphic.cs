using Avalonia.Controls;
using Avalonia.Media;

namespace FunctionBilder.Dekstop
{
	public abstract class Graphic
	{
		public readonly double Ratio = 0.4;
		public IBrush PointColor { get; } = Brushes.Red;
		public IBrush LineColor { get; } = Brushes.Yellow;
		public bool IsVisibleElipse { get; }	
		public IBrush[] GraphicColor() => new IBrush[] { this.PointColor, this.LineColor};

		public void GraphicRender()
		{

		}
		public void GraphicRender(Control inputCoordinate)
		{

		}
	}
}
