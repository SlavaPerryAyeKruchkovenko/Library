using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FunctionBilder.Dekstop.viewModel;
using FunctionBulber.Logic;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop
{
	interface IFunctionDrawer
	{
		void DrawLine(Point start,Point finish,IBrush brush);
		void DrawFunction(double startX, double finishX, double rangeX,Stack<Element> elements);
		void DrawAswer(List<Point> points);
		void DrawArrows(Point start,Point range);
		void DrawLabel(Point[] points, string[] content);
	}
	
	class FunctionDrawer:IFunctionDrawer
	{
		DataGrid outputBox;
		Canvas drawCanvas;
		IDrawer drawer;
		List<Point> points { get; }
		public FunctionDrawer(DataGrid _outputBox,Canvas canvas,IDrawer _drawer)
		{
			this.outputBox = _outputBox;
			this.drawCanvas = canvas;
			this.drawer = _drawer;
			this.points = new List<Point>();
		}
		public void DrawLine(Point start, Point finish,IBrush brush)
		{
			Figure figure = new MyLine();
			this.drawCanvas.Children.Insert(0, figure.Create(new Point[] { start, finish },brush));
		}
		public void DrawFunction(double startX, double finishX, double rangeX, 
			Stack<Element> elements)
		{
			Point startLinePoint = default;
			for (double i = startX; i <= finishX; i += rangeX) 
			{
				Point canvasSize = new Point(this.drawCanvas.Bounds.Width / 2,
					this.drawCanvas.Bounds.Height / 2);
				Stack<Element> rpn = elements.Clone<Element>();
				Calculate calculate = new Calculate(this.drawer, rpn);

				Point point = new Point(i, calculate.CountRPN(new double[] { i, i }));

				points.Add(point);

				if (i % 10 == 0)
				{
					DrawLabel(new Point[] { canvasSize,point },new string[] 
					{point.X.ToString(),point.Y.ToString() });
				}
				if(double.IsNormal(point.Y))
				{
					Figure figure = new MyEllipse();
					Point pointNow = new Point(canvasSize.X + point.X, canvasSize.Y - point.Y);

					this.drawCanvas.Children.Insert(0, figure.Create(new Point[] { pointNow },Brushes.Red));

					if(startLinePoint!=default)
					{
						DrawLine(new Point(canvasSize.X+startLinePoint.X,canvasSize.Y-startLinePoint.Y),
							pointNow,Brushes.Yellow);
					}						
					startLinePoint = point;		
				}
				else
				{
					startLinePoint = default;
				}
			}
			DrawAswer(this.points);
		}
		public void DrawArrows(Point start,Point range)
		{
			Figure figure = new Mypolygon();
			this.drawCanvas.Children.Insert(0, figure.Create(new Point[] { start, range }, Brushes.DeepPink));
		}
		public void DrawAswer(List<Point> points)
		{
			this.outputBox.Items=points;
		}
		public void DrawLabel(Point[] points,string[] content)
		{
			MyControler controler = new MyLabel();
			Control[] labels= controler.Create(points,ref content);
			this.drawCanvas.Children.Insert(this.drawCanvas.Children.Count - 1, labels[0]);
			this.drawCanvas.Children.Insert(this.drawCanvas.Children.Count - 1, labels[1]);
		}
	}
}
