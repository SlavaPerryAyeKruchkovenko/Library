using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	static class CanvasExtension
	{
		public static List<Point> GraphicRender(this Canvas canvas, string _function,
			double[] location, Point rangeLocation, double scale, IBrush[] brushes)
		{
			Point canvasSize = new Point(canvas.Bounds.Width, canvas.Bounds.Height);

			IFunctionDrawer FDrawer = new FunctionDrawer(canvas);
			var function = new Function(FDrawer, rangeLocation, canvasSize, brushes);
			return function.Render(new Point(location[0], location[1]), _function, location[2], scale);
		}
	}
}
