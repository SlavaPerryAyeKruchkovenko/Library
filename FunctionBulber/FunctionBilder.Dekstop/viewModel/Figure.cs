using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace FunctionBilder.Dekstop.viewModel
{
	abstract class Figure
	{
		public abstract Control Create(Point[] points, IBrush brushes);
	}
	class MyEllipse : Figure
	{
		public override Control Create(Point[] points, IBrush brushes)
		{
			Ellipse ellipse = new Ellipse
			{
				Width = 3,
				Height = 3,
				Fill = brushes
			};
			Canvas.SetLeft(ellipse, points[0].X);
			Canvas.SetTop(ellipse, points[0].Y);
			return ellipse;
		}
	}
	class MyLine : Figure
	{
		public override Control Create(Point[] points, IBrush brushes)
		{
			Line line = new Line
			{
				StartPoint = points[0],
				EndPoint = points[1],
				Stroke = brushes,
				StrokeThickness = 1
			};
			return line;
		}
	}
	class Mypolygon : Figure
	{
		public override Control Create(Point[] points, IBrush brushes)
		{
			Polygon arrow = new Polygon
			{
				Points = new Point[] {
					new Point(points[0].X - points[1].X, points[0].Y - points[1].Y),
					new Point(points[0].X, points[0].Y),
					new Point(points[0].X - points[1].Y, points[0].Y + points[1].X) },
				Stroke = brushes,
				StrokeThickness = 1,
				Fill = brushes
			};
			return arrow;
		}
	}
	abstract class MyControler
	{
		public abstract Control[] Create(Point[] points, ref string[] content);
	}
	class MyLabel:MyControler
	{
		public override Control[] Create(Point[] points, ref string[] content)
		{
			Label[] labels = new Label[]
			{
				CreateLabel(new Point(points[0].X+points[1].X,points[0].Y),new Point(0,-10),ref content[0]),
				CreateLabel(new Point(points[0].X,points[0].Y-points[1].Y),new Point(0,-10),ref content[1])
			};
			return labels;
		}
		private static Label CreateLabel(Point point,Point range ,ref string content)
		{
			Label myLabel = new Label
			{
				Content = content,
				FontSize = 5,
				VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
				HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
			};
			Canvas.SetLeft(myLabel, point.X + range.X);
			Canvas.SetTop(myLabel, point.Y + range.Y);
			return myLabel;
		}
	}
}
