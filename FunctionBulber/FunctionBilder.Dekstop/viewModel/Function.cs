using Avalonia;
using Avalonia.Media;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionBilder.Dekstop.viewModel
{
	class Function
	{
		IFunctionDrawer functionDrawer { get; }
		public Function(IFunctionDrawer _functionDrawer)
		{
			this.functionDrawer = _functionDrawer;
		}
		public void Render(Point canvasSize,Point cycleSize,Stack<Element> elements,double range)
		{
			this.functionDrawer.DrawLine(new Point(0, canvasSize.Y / 2),
				new Point(canvasSize.X, canvasSize.Y / 2), Brushes.DeepPink);

			this.functionDrawer.DrawLine(new Point(canvasSize.X / 2, 0),
				new Point(canvasSize.X / 2, canvasSize.Y), Brushes.DeepPink);

			this.functionDrawer.DrawArrows(new Point(canvasSize.X, canvasSize.Y / 2),
				new Point(10, 10));

			this.functionDrawer.DrawArrows(new Point(canvasSize.X / 2, 0),
				new Point(10, -10));

			this.functionDrawer.DrawFunction(cycleSize.X, cycleSize.Y,
				range, elements);
		}
	}
}
