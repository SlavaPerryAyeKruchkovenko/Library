﻿using Avalonia;
using Avalonia.Media;
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
		void DrawArrows(Point location, Point size,IBrush brush);
		void DrawLabels(Point gap, Point coordinate, bool isXLine);
	}

	class FunctionDrawer : IFunctionDrawer
	{
		private Field field { get; }
		public FunctionDrawer(Field _field)
		{
			this.field = _field;
		}
		public void DrawLine(Point startLocation, Point finishLocation, IBrush brush, short lineScale)
		{
			Figure figure = new MyLine();
			this.field.Canvas.Children.Insert(0, figure.Create(new Point[] { startLocation, finishLocation }, brush, lineScale));
		}
		public void DrawFunction(Graphic graphic, ReversePolandLogic RPN)
		{
			var coordinates = new List<Point>();
			Point startLinePoint = default;

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

					if(graphic.IsVisibleElipse)
					{
						DrawPoint(pointNow, graphic.PointColor, this.field.Ratio * this.field.Scale);
					}					
					if (Math.Abs(Math.Abs(startLinePoint.X - point.X) - range * this.field.Scale) < range)
					{
						double x = canvasSize.X + startLinePoint.X;
						double y = canvasSize.Y - startLinePoint.Y;
						DrawLine(new Point(x, y), pointNow, graphic.LineColor, this.field.AxisLineScale);
					}
					startLinePoint = point;
				}
				else
				{
					startLinePoint = default;
				}
			}
			DrawAnswer(coordinates);
		}
		public void DrawArrows(Point location, Point size,IBrush brush)
		{
			Figure figure = new Mypolygon();
			this.field.Canvas.Children.Insert(0, figure.Create(new Point[] { location, size }, brush, 1));
		}
		public void DrawLabels(Point gap, Point coordinate, bool isXLine)
		{
			for (int i = (int)gap.X; i < gap.Y; i++)
			{
				if (i % this.field.Scale == 0)
				{
					MyControler controler = new MyLabel();
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
					this.field.Canvas.Children.Insert(0, controler.Create(newCoordinate, content, this.field.Ratio * this.field.Scale));
				}

			}
		}
		private void DrawPoint(Point pointNow, IBrush brush, double scale)
		{
			Figure figure = new MyEllipse();
			this.field.Canvas.Children.Insert(0, figure.Create(new Point[] { pointNow }, brush, scale));
		}
		private void DrawAnswer(List<Point> points)
		{
			if (this.field.Input != null) 
			this.field.Input.Items = points;
		}
	}
}
