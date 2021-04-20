using Avalonia.Controls;
using Avalonia.Media;

namespace FunctionBilder.Dekstop
{
	public abstract class Graphic
	{
		public readonly double Ratio = 0.4;
		public abstract IBrush PointColor { get; }
		public abstract IBrush LineColor { get; }
		public virtual bool IsVisibleElipse { get; }	
		public IBrush[] GraphicColor() => new IBrush[] { this.PointColor, this.LineColor};

		public void GraphicRender()
		{

		}
		public void GraphicRender(Control inputCoordinate)
		{

		}
	}
	public class MyGraphic : Graphic
	{
		public override IBrush PointColor { get; }

		public override IBrush LineColor { get; }
		public override bool IsVisibleElipse { get; }
	}
	public class StandartGraphic : Graphic
	{
		public override IBrush PointColor { get; } = Brushes.Red;
		public override IBrush LineColor { get; } = Brushes.Yellow;
		public override bool IsVisibleElipse { get; }
	}
}
