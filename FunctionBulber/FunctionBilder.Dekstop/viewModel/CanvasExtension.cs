using Avalonia;
using Avalonia.Controls;
using FunctionBilder.Dekstop.Model;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.ViewModel
{
	static class CanvasExtension
	{
		public static List<Point> GraphicRender(this Canvas canvas, string _function,double[] gap,
			Field field,double scale)
		{

			IFunctionDrawer FDrawer = new FunctionDrawer(canvas);
			var function = new Function(FDrawer,field);

			return function.Render(_function, scale,gap);
		}
	}
}
