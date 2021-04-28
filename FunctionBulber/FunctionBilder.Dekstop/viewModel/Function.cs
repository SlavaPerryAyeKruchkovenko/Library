using Avalonia;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class Function
	{
		private IFunctionDrawer functionDrawer { get; set; }
		public string FunctionText { get; private set; }
		private ReversePolandLogic RPN {get;}
		public Graphic Graphic { get; private set; }
		
		public Function(string _function,Graphic _graphic)
		{
			this.FunctionText = _function;
			this.Graphic = _graphic;
			this.RPN = new ReversePolandLogic(this.FunctionText);
			this.RPN.StackInitialization();
		}		
		public void Render(Field field)
		{
			this.functionDrawer = new FunctionDrawer(field);

			ChangeGraphic();
			Point rangeLocation = field.BeginOfCountdown;
			Point layoutSize = field.LayoutSize;
			Point[] points;

			if (field.IsLabelVisible)
			{	
				points = new Point[]		
				{
					new Point(-layoutSize.X/2-rangeLocation.X,layoutSize.X/2-rangeLocation.X),
					new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
				};
				this.functionDrawer.DrawLabels(points[0], points[1], field.Scale, true, field.Ratio);
			
				points = new Point[]
				{
					new Point(-layoutSize.Y/2+rangeLocation.Y,layoutSize.Y/2+rangeLocation.Y),
					new Point(layoutSize.X/2,layoutSize.Y/2)+rangeLocation
				};
				this.functionDrawer.DrawLabels(points[0], points[1], field.Scale, false, field.Ratio);
			}			
			points = new Point[]
			{
				new Point(0, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], field.AxisColor, field.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X / 2+rangeLocation.X, 0),
				new Point(layoutSize.X / 2+rangeLocation.X, layoutSize.Y)
			};

			this.functionDrawer.DrawLine(points[0], points[1], field.AxisColor,field.AxisLineScale);

			points = new Point[]
			{
				new Point(layoutSize.X, layoutSize.Y / 2+rangeLocation.Y),
				new Point(layoutSize.X / 2+rangeLocation.X, 0)
			};

			this.functionDrawer.DrawArrows(points[0], new Point(10, 10), field.AxisColor);

			this.functionDrawer.DrawArrows(points[1], new Point(10, -10), field.AxisColor);

			this.functionDrawer.DrawFunction(this.Graphic,this.RPN);
		}
		private static List<string> dicks = new List<string> { "dick", "член", "пиписька", "хуй", "cock", "pennis" };
		private void ChangeGraphic()
		{
			if (dicks.Contains(this.FunctionText.Trim()))
			{
				this.Graphic = new Graphic(false, new double[] { -3, 3, 0.01 });
				this.FunctionText = "Sqrt(sin(x) ^ 2) + 5 * e ^ (-x ^ 100) * cos(x)";
			}
		}
		public string GetCoordinateInPoint(Point pointNow)
		{
			string content = "";
			Point point = ModelNumerable.YCoordinate(this.RPN, new double[] { pointNow.X });
			content += this.FunctionText;

			if (Math.Abs(-1 * pointNow.Y - point.Y) < 0.5 && point.X >= this.Graphic.gap[0] && point.X <= this.Graphic.gap[1]) 
			{
				content += " в точке " + Math.Round(point.X, 2).ToString();
				content += " ~ " + Math.Round(point.Y, 2).ToString() + "\n";
			}
			else
			{
				content += " Не имеет значения в данной точке" + "\n";
			}
			return content;
		}
	}
}
