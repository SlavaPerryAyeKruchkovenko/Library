using Avalonia;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBilder.Dekstop.Model
{
	public class Function
	{
		private IFunctionDrawer functionDrawer;
		public string FunctionText { get; private set; }

		private readonly ReversePolandLogic RPN;
		public Graphic Graphic { get; private set; }

		public Function(string _function, Graphic _graphic)
		{
			this.FunctionText = _function;
			this.Graphic = _graphic;
			ChangeGraphic();
			this.RPN = new ReversePolandLogic(this.FunctionText);
			this.RPN.StackInitialization();
		}
		public void Render(Field field)
		{
			this.functionDrawer = new FunctionDrawer(field);
			this.functionDrawer.DrawFunction(this.Graphic, this.RPN);
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
			var content = new StringBuilder();
			Point point = ModelNumerable.YCoordinate(this.RPN, new double[] { pointNow.X });
			content.Append(this.FunctionText);

			if (Math.Abs(-1 * pointNow.Y - point.Y) < 0.5 && point.X >= this.Graphic.gap[0] && point.X <= this.Graphic.gap[1])
			{
				content.Append(" в точке " + Math.Round(point.X, 2).ToString());
				content.Append(" ~ " + Math.Round(point.Y, 2).ToString() + "\n");
			}
			else
			{
				content.Append(" Не имеет значения в данной точке" + "\n");
			}
			return content.ToString();
		}
		public override string ToString()
		{
			return this.FunctionText;
		}
	}
}
