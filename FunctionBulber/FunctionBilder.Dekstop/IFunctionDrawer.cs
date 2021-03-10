using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop
{
	interface IFunctionDrawer
	{
		void DrawLine();
		void DrawFunction(double startX, double finishX, double rangeX,Stack<Element> elements);
		void DrawAswer(string asnwer);
		void DrawArrows();
	}
	abstract class Figure
	{
		public abstract Control Create(Point start, Point finish);
	}
	class MyEllipse:Figure
	{
		public override Control Create(Point start, Point finish)
		{
			Ellipse ellipse = new Ellipse
			{
				Width = 3,
				Height = 3,
				Fill = Brushes.Yellow
			};
			Canvas.SetLeft(ellipse, start.X);
			Canvas.SetTop(ellipse, start.Y);
			return ellipse;
		}
	}
	class MyLine:Figure
	{
		public override Control Create(Point start,Point finish)
		{
			Line line = new Line
			{
				StartPoint = start,
				EndPoint = finish,
				Stroke = Brushes.Blue,			
			};
			return line;
		}
	}
	class FunctionDrawer:IFunctionDrawer
	{
		TextBox outputBox;
		Canvas drawCanvas;
		IDrawer drawer;
		public FunctionDrawer(TextBox _outputBox,Canvas canvas,IDrawer _drawer)
		{
			this.outputBox = _outputBox;
			this.drawCanvas = canvas;
			this.drawer = _drawer;
		}
		public void DrawLine()
		{
			Figure figure = new MyLine();
			this.drawCanvas.Children.Insert(0, figure.Create(
				new Point(0,this.drawCanvas.Bounds.Height/2), 
				new Point(this.drawCanvas.Bounds.Width, this.drawCanvas.Bounds.Height / 2)));
			this.drawCanvas.Children.Insert(0, figure.Create(
				new Point(this.drawCanvas.Bounds.Width / 2, 0),
				new Point(this.drawCanvas.Bounds.Width / 2, this.drawCanvas.Bounds.Height)));

		}
		public void DrawFunction(double startX, double finishX, double rangeX, 
			Stack<Element> elements)
		{
			for (double i = startX; i <= finishX; i += rangeX) 
			{
				Stack<Element> rpn = elements.Clone<Element>();
				Calculate calculate = new Calculate(this.drawer, rpn);
				Point point = new Point(i, calculate.CountRPN(new double[] { i, i }));

				DrawAswer($"X={point.X}: Y={point.Y}");

				Figure figure = new MyEllipse();
				this.drawCanvas.Children.Insert(0, figure.Create
					(new Point(this.drawCanvas.Bounds.Width / 2 + point.X,
					this.drawCanvas.Bounds.Height / 2 - point.Y),new Point()));
			}
		}
		public void DrawArrows()
		{

		}
		public void DrawAswer(string asnwer)
		{
			this.outputBox.Text += asnwer + $"{Environment.NewLine}";
		}
	}
}
