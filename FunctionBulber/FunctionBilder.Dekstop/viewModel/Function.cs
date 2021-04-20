using Avalonia;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	class Function
	{
		private IFunctionDrawer functionDrawer { get; }
		
		private Field field { get; }
		public Function(IFunctionDrawer _functionDrawer,Field _field)
		{
			this.functionDrawer = _functionDrawer;
			this.field = _field;
		}
		public List<Point> Render(string function, double scale,double[] gap)
		{
			Point cycleSize = new Point(gap[0], gap[1]);
			double range = gap[2];

			Point rangeLocation = this.field.BeginOfCountdown;
			Point layoutSize = this.field.LayoutSize;

			Point[] points = new Point[]
			{
				new Point(-layoutSize.X/2-rangeLocation.X,layoutSize.X/2-rangeLocation.X),
				new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
			};
			this.functionDrawer.DrawLabels(points[0], points[1], scale, true, this.field.Ratio);

			points = new Point[]
			{
				new Point(-layoutSize.Y/2+rangeLocation.Y,layoutSize.Y/2+rangeLocation.Y),
				new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
			};
			this.functionDrawer.DrawLabels(points[0], points[1], scale, false, this.field.Ratio);

			points = new Point[]
			{
				new Point(0, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], this.field.AxisColor, this.field.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X / 2+rangeLocation.X, 0),
				new Point(layoutSize.X / 2+rangeLocation.X, layoutSize.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], this.field.AxisColor,this.field.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X / 2+rangeLocation.X, 0)
			};

			this.functionDrawer.DrawArrows(points[0], new Point(10, 10), this.field.AxisColor);

			this.functionDrawer.DrawArrows(points[1], new Point(10, -10), this.field.AxisColor);

			return this.functionDrawer.DrawFunction(cycleSize, range, function, scale, this.field);
		}
	}
}
