using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FunctionBilder.Dekstop.ViewModel;
using System;

namespace FunctionBilder.Dekstop.Model
{
	public class Field
	{
		public readonly short NormalVizualRangeForLabel = -10;
		public readonly double Ratio = 0.4;
		public short AxisLineScale { get; } = 5;
		public short Scale { get; } = 40;
		public bool IsLabelVisible { get; }
		public IBrush AxisColor { get; } = Brushes.DeepPink;
		public Point BeginOfCountdown { get; } = default;
		public Point LayoutSize { get; }
		public Canvas Canvas { get; }
		public DataGrid Input { get; }

		private IFunctionDrawer functionDrawer;
		public Field(Canvas canvas, DataGrid input)
		{
			this.Canvas = canvas;
			this.LayoutSize = this.CountLayoutSize(this.Canvas);
			this.Input = input;
		}
		public Field(Canvas canvas, Point newRange, short[] scales, bool isVisible, DataGrid input)
		{
			this.Canvas = canvas;

			this.AxisLineScale = scales[0];
			this.Scale = scales[1];

			this.BeginOfCountdown = newRange;
			this.LayoutSize = this.CountLayoutSize(this.Canvas);

			this.IsLabelVisible = isVisible;

			this.Input = input;
		}
		public void ClearCanvas()
		{
			this.Canvas.Children.Clear();
		}
		public void AddChildren(Control control)
		{
			this.Canvas.Children.Insert(0, control);
		}
		public Point CountLayoutSize(Canvas canvas)
		{
			return new Point(canvas.Bounds.Width, canvas.Bounds.Height);
		}
		public void RenderField()
		{
			this.functionDrawer = new FunctionDrawer(this);
			Point rangeLocation = this.BeginOfCountdown;
			Point layoutSize = this.LayoutSize;
			Point[] points;

			if (this.IsLabelVisible)
			{
				points = new Point[]
				{
					new Point(-layoutSize.X/2-rangeLocation.X,layoutSize.X/2-rangeLocation.X),
					new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
				};
				if (Math.Abs(rangeLocation.Y) < layoutSize.Y / 2)
				{
					this.functionDrawer.DrawLabels(points[0], points[1], true);
				}
				points = new Point[]
				{
					new Point(-layoutSize.Y/2+rangeLocation.Y,layoutSize.Y/2+rangeLocation.Y),
					new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
				};
				if (Math.Abs(rangeLocation.X) < layoutSize.X / 2)
				{
					this.functionDrawer.DrawLabels(points[0], points[1], false);
				}
			}
			points = new Point[]
			{
				new Point(0, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], this.AxisColor, this.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X / 2+rangeLocation.X, 0),
				new Point(layoutSize.X / 2+rangeLocation.X, layoutSize.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], this.AxisColor, this.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X / 2+rangeLocation.X, 0)
			};

			this.functionDrawer.DrawArrows(points[0], new Point(10, 10), this.AxisColor);

			this.functionDrawer.DrawArrows(points[1], new Point(10, -10), this.AxisColor);
		}
	}
}
