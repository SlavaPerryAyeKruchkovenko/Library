using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.Model
{
	public class Field
	{
		public readonly short NormalVizualRangeForLabel = -10;
		public readonly double Ratio = 0.4;
		public short AxisLineScale { get; } = 5;
		public short Scale { get; } = 40;
		public short FontSize { get; } = 6;
		public bool IsLabelVisible { get; }
		public IBrush AxisColor { get; } = Brushes.DeepPink;
		public Point BeginOfCountdown { get; } = default;
		public Point LayoutSize { get; }
		public Canvas Canvas { get; }
		public DataGrid Input { get; }
		public Field(Canvas canvas,DataGrid input)
		{
			this.Canvas = canvas;
			this.LayoutSize = this.CountLayoutSize(this.Canvas);
			this.Input = input;
		}
		public Field(Canvas canvas, Point newRange, short[] scales, bool isVisible,DataGrid input)
		{
			this.Canvas = canvas;

			this.AxisLineScale = scales[0];
			this.Scale = scales[1];
			this.FontSize = scales[2];

			this.LayoutSize = this.CountLayoutSize(this.Canvas);
			this.BeginOfCountdown = newRange;

			this.IsLabelVisible = isVisible;

			this.Input = input;
		}
		public Point CountLayoutSize(Canvas canvas)
		{
			return new Point(canvas.Bounds.Width, canvas.Bounds.Height);
		}
	}
}
