using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	interface IFunctionDrawer
	{
		void DrawLine(Point startLocation, Point finishLocation, IBrush brush);
		List<Point> DrawFunction(Point gap, double range, string function, Point location, double scale, IBrush[] brushes);
		void DrawArrows(Point location, Point size);
		void DrawLabels(Point gap, Point coordinate, double fontSize, bool isXLine);
	}

	class FunctionDrawer : IFunctionDrawer
	{
		private Canvas drawCanvas { get; }
		public FunctionDrawer(Canvas _canvas)
		{
			drawCanvas = _canvas;
		}
		public void DrawLine(Point startLocation, Point finishLocation, IBrush brush)
		{
			Figure figure = new MyLine();
			drawCanvas.Children.Insert(0, figure.Create(new Point[] { startLocation, finishLocation }, brush, Field.LineScale));
		}
		public List<Point> DrawFunction(Point gap, double range, string function, Point location, double scale, IBrush[] brushes)
		{
			var coordinates = new List<Point>();
			Point startLinePoint = default;

			Point canvasSize = new Point(drawCanvas.Bounds.Width / 2,
					drawCanvas.Bounds.Height / 2) + location;

			var RPN = new ReversePolandLogic(function);
			RPN.StackInitialization();

			for (double i = gap.X; i <= gap.Y; i += range)
			{
				i = Math.Round(i, range.Length());
				Point point;

				if (Math.Abs(i * scale + location.X) < drawCanvas.Bounds.Width / 2)
				{

					point = ModelNumerable.YCoordinate(RPN, new double[] { i, i }) * scale;

					coordinates.Add(point);
				}
				else
				{
					continue;
				}

				if ((double.IsNormal(point.Y) || point.Y == 0) && Math.Abs(point.Y - location.Y) < drawCanvas.Bounds.Height / 2)
				{

					Point pointNow = new Point(canvasSize.X + point.X, canvasSize.Y - point.Y);

					DrawPoint(pointNow, brushes[0], Field.Ratio * scale);

					if (Math.Abs(Math.Abs(startLinePoint.X - point.X) - range * scale) < range)
					{
						double x = canvasSize.X + startLinePoint.X;
						double y = canvasSize.Y - startLinePoint.Y;
						DrawLine(new Point(x, y), pointNow, brushes[1]);
					}
					startLinePoint = point;
				}
				else
				{
					startLinePoint = default;
				}
			}
			return coordinates;
		}
		public void DrawArrows(Point location, Point size)
		{
			Figure figure = new Mypolygon();
			drawCanvas.Children.Insert(0, figure.Create(new Point[] { location, size }, Brushes.DeepPink, 1));
		}
		public void DrawLabels(Point gap, Point coordinate, double fontSize, bool isXLine)
		{

			for (int i = (int)gap.X; i < gap.Y; i++)
			{
				if (i % fontSize == 0)
				{
					MyControler controler = new MyLabel();
					Point coordinate1;

					if (isXLine)
					{
						coordinate1 = new Point(coordinate.X + i, coordinate.Y);
					}
					else
					{
						coordinate1 = new Point(coordinate.X, coordinate.Y - i);
					}
					if (Math.Abs(coordinate1.X) > drawCanvas.Bounds.Width || Math.Abs(coordinate1.Y) > drawCanvas.Bounds.Height)
					{
						continue;
					}
					string content = (i / fontSize).ToString();
					drawCanvas.Children.Insert(0, controler.Create(coordinate1, content, Field.Ratio * fontSize));
				}

			}
		}
		private void DrawPoint(Point pointNow, IBrush brush, double scale)
		{
			Figure figure = new MyEllipse();
			drawCanvas.Children.Insert(0, figure.Create(new Point[] { pointNow }, brush, scale));
		}
	}
}
