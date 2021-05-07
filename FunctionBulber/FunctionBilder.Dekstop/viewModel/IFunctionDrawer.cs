using Avalonia;
using Avalonia.Media;
using Avalonia.Threading;
using FunctionBilder.Dekstop.Model;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	public interface IFunctionDrawer
	{
		void DrawLine(Point startLocation, Point finishLocation, IBrush brush, short lineScale);
		void DrawFunction(Graphic graphic, ReversePolandLogic function);
		void DrawArrows(Point location, Point size, IBrush brush);
		void DrawLabels(Point gap, Point coordinate, bool isXLine);
	}

	class FunctionDrawer : IFunctionDrawer
	{
		private readonly Field field;
		public FunctionDrawer(Field _field)
		{
			this.field = _field;
		}
		public void DrawLine(Point startLocation, Point finishLocation, IBrush brush, short lineScale)
		{
			Dispatcher.UIThread.InvokeAsync(() => 
			{ 
				var figure = new MyLine(); 
				this.field.AddChildren(figure.Create(new Point[] { startLocation, finishLocation }, brush, lineScale)); 
			});
		}
		public void DrawFunction(Graphic graphic, ReversePolandLogic RPN)
		{
			var coordinates = new List<Point>();
			Point? startLinePoint = default;

			Point canvasSize = this.field.LayoutSize / 2 + this.field.BeginOfCountdown;

			double range = graphic.gap[2];

			for (double i = graphic.gap[0]; i <= graphic.gap[1]; i += range)
			{
				i = Math.Round(i, range.Length());
				Point point;

				if (Math.Abs(i * this.field.Scale + this.field.BeginOfCountdown.X) < this.field.Canvas.Bounds.Width / 2)
				{
					point = ModelNumerable.YCoordinate(RPN, new double[] { i, i }) * this.field.Scale;
					coordinates.Add(point);
				}
				else
				{
					continue;
				}

				if ((double.IsNormal(point.Y) || point.Y == 0) && Math.Abs(point.Y - this.field.BeginOfCountdown.Y) < this.field.Canvas.Bounds.Height / 2)
				{

					Point pointNow = new Point(canvasSize.X + point.X, canvasSize.Y - point.Y);

					if (graphic.IsVisibleElipse)
					{
						DrawPoint(pointNow, graphic.PointColor, this.field.Ratio * this.field.Scale);
					}
					if (startLinePoint.HasValue)
					{
						double x = canvasSize.X + startLinePoint.Value.X;
						double y = canvasSize.Y - startLinePoint.Value.Y;
						DrawLine(new Point(x, y), pointNow, graphic.LineColor, this.field.AxisLineScale);
					}
					startLinePoint = point;
				}
				else
				{
					startLinePoint = null;
				}
			}
			DrawAnswer(coordinates);
		}
		public void DrawArrows(Point location, Point size, IBrush brush)
		{
			Dispatcher.UIThread.InvokeAsync(() =>
			{
				var figure = new Mypolygon();
				this.field.AddChildren(figure.Create(new Point[] { location, size }, brush, 1));
			});
		}
		public void DrawLabels(Point gap, Point coordinate, bool isXLine)
		{
			for (int i = (int)gap.X; i < gap.Y; i++)
			{
				if (i % this.field.Scale == 0)
				{
					var controler = new MyLabel();
					Point newCoordinate;

					if (isXLine)
					{
						newCoordinate = new Point(coordinate.X + i, coordinate.Y);
					}
					else
					{
						newCoordinate = new Point(coordinate.X, coordinate.Y - i);
					}
					string content = (i / this.field.Scale).ToString();
					DrawPoint(newCoordinate, this.field.AxisColor, this.field.Ratio * this.field.Scale);
					this.field.AddChildren(controler.Create(newCoordinate, content, this.field.Ratio * this.field.Scale));
				}

			}
		}
		private void DrawPoint(Point pointNow, IBrush brush, double scale)
		{
			Action action = () =>
			{
				var figure = new MyEllipse();
				this.field.AddChildren(figure.Create(new Point[] { pointNow }, brush, scale));
			};
			Dispatcher.UIThread.InvokeAsync(action);		
		}
		private void DrawAnswer(List<Point> points)
		{
			Dispatcher.UIThread.InvokeAsync(() =>
			{
				if (this.field.Input != null)
					this.field.Input.Items = points;
			});			
		}
	}
}