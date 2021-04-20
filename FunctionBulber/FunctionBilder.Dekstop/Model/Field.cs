using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;


namespace FunctionBilder.Dekstop.Model
{
	public abstract class Field
	{
		public readonly short NormalVizualRangeForLabel = -10;
		public abstract short AxisLineScale { get; }
		public virtual short Scale { get; }
		public virtual short FontSize { get; }
		public virtual bool IsLabelVisible { get; }
		public abstract IBrush AxisColor { get; }
		public virtual Point BeginOfCountdown { get; }
		public abstract Point LayoutSize { get; }	
		public abstract Canvas Canvas { get; }		
	
		public Point CountLayoutSize(Canvas canvas)
		{
			return new Point(canvas.Bounds.Width, canvas.Bounds.Height);
		}
	}

	public class MyField:Field
	{
		public override short AxisLineScale { get; }
		public override short Scale { get; }
		public override short FontSize { get; }
		public override bool IsLabelVisible { get; }
		public override IBrush AxisColor { get; }
		public override Point BeginOfCountdown { get; }
		public override Point LayoutSize { get; }
		public override Canvas Canvas { get; }
		public MyField(Canvas canvas, IBrush axisColor, Point newRange, short[] scales,bool isVisible)
		{
			this.Canvas = canvas;
			this.AxisColor = axisColor;

			this.AxisLineScale = scales[0];
			this.Scale = scales[1];
			this.FontSize = scales[2];

			this.LayoutSize = this.CountLayoutSize(this.Canvas);
			this.BeginOfCountdown = newRange;

			this.IsLabelVisible = isVisible;
		}
		
	}
	public class StandartField:Field
	{
		public override short AxisLineScale { get; } = 5;
		public override short Scale { get; } = 40;
		public override short FontSize { get; } = 6;		
		public override IBrush AxisColor { get; } = Brushes.DeepPink;
		public override Point BeginOfCountdown { get; } = default;
		public override Point LayoutSize { get; }
		public override Canvas Canvas { get; }
		public StandartField(Canvas canvas)
		{
			this.Canvas = canvas;
			this.LayoutSize = this.CountLayoutSize(this.Canvas);
		}
	}
}
