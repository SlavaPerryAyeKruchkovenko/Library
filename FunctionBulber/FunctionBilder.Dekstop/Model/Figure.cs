using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace FunctionBilder.Dekstop.Model
{
	abstract class Figure
	{
		public abstract Control Create(Point[] coordinates, IBrush brushes, double scale);
	}
	class MyEllipse : Figure
	{
		public override Control Create(Point[] coordinates, IBrush brushes, double scale)
		{
			var ellipse = new Ellipse
			{
				Width = scale,
				Height = scale,
				Fill = brushes,
				Margin = new Thickness(coordinates[0].X - scale / 2, coordinates[0].Y - scale / 2)
			};
			return ellipse;
		}
	}
	class MyLine : Figure
	{
		public override Control Create(Point[] coordinates, IBrush brushes, double scale)
		{
			var line = new Line
			{
				StartPoint = coordinates[0],
				EndPoint = coordinates[1],
				Stroke = brushes,
				StrokeThickness = scale
			};

			return line;
		}
	}
	class Mypolygon : Figure
	{
		public override Control Create(Point[] coordinates, IBrush brushes, double scale)
		{
			var arrow = new Polygon
			{
				Points = new Point[]
				{
					new Point(coordinates[0].X - coordinates[1].X, coordinates[0].Y - coordinates[1].Y),
					new Point(coordinates[0].X, coordinates[0].Y),
					new Point(coordinates[0].X - coordinates[1].Y, coordinates[0].Y + coordinates[1].X)
				},
				Stroke = brushes,
				StrokeThickness = scale,
				Fill = brushes
			};
			return arrow;
		}
	}
	abstract class MyControler
	{
		public abstract Control Create(Point coordinate, string content, double fontSize);
	}
	class MyLabel : MyControler
	{
		public override Control Create(Point coordinate, string content, double fontSize)
		{
			var myLabel = new Label
			{
				Content = content,
				FontSize = fontSize,
				VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
				HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
				Margin = new Thickness(coordinate.X, coordinate.Y)
			};
			
			return myLabel;
		}
	}
	class MyToolTip : MyControler
	{
		public override Control Create(Point coordinate, string content, double fontSize)
		{
			var myToolTip = new ToolTip
			{
				Content = content,
				FontSize = fontSize,
				IsVisible = true,
				Margin = new Thickness(coordinate.X,coordinate.Y)
			};
			return myToolTip;
		}
	}
}
