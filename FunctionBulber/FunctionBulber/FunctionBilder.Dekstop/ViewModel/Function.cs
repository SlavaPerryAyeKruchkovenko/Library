using Avalonia;
using Avalonia.Media;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	class Function
	{
		private IFunctionDrawer functionDrawer { get; }
		private Point rangeLocation { get; }
		private Point layoutSize { get; }
		private IBrush[] brushes { get; }
		public Function(IFunctionDrawer _functionDrawer, Point _rangeLocation, Point _canvasSize, IBrush[] _brushes)
		{
			functionDrawer = _functionDrawer;
			rangeLocation = _rangeLocation;
			layoutSize = _canvasSize;
			brushes = _brushes;
		}
		public List<Point> Render(Point cycleSize, string function, double range, double scale)
		{
			Point[] points = new Point[]
			{
				new Point(-layoutSize.X/2-rangeLocation.X,layoutSize.X/2-rangeLocation.X),
				new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
			};
			functionDrawer.DrawLabels(points[0], points[1], scale, true);

			points = new Point[]
			{
				new Point(-layoutSize.Y/2+rangeLocation.Y,layoutSize.Y/2+rangeLocation.Y),
				new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
			};
			functionDrawer.DrawLabels(points[0], points[1], scale, false);

			points = new Point[]
			{
				new Point(0, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y)
			};

			functionDrawer.DrawLine(points[0], points[1], Brushes.DeepPink);

			points = new Point[]
			{
				new Point(layoutSize.X / 2+rangeLocation.X, 0),
				new Point(layoutSize.X / 2+rangeLocation.X, layoutSize.Y)
			};

			functionDrawer.DrawLine(points[0], points[1], Brushes.DeepPink);

			points = new Point[]
			{
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X / 2+rangeLocation.X, 0)
			};

			functionDrawer.DrawArrows(points[0], new Point(10, 10));

			functionDrawer.DrawArrows(points[1], new Point(10, -10));

			return functionDrawer.DrawFunction(cycleSize, range, function, rangeLocation, scale, brushes);
		}
	}
}
