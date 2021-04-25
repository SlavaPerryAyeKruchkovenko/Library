using Avalonia;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class Function
	{
		private IFunctionDrawer functionDrawer { get; set; }
		public string FunctionText { get; }
		public Graphic Graphic { get; }
		
		public Function(string _function,Graphic _graphic)
		{
			this.FunctionText = _function;
			this.Graphic = _graphic;
		}
		public void Render(Field field)
		{
			this.functionDrawer = new FunctionDrawer(field);

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

			this.functionDrawer.DrawFunction(this.Graphic,this.FunctionText);
		}
	}
}
