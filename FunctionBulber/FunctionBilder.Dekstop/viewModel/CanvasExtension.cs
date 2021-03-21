using Avalonia;
using Avalonia.Controls;
using FunctionBulber.Logic;
using System;

namespace FunctionBilder.Dekstop.viewModel
{
	static class CanvasExtension
	{
		public static void GraphicRender(this Canvas canvas,string _function,IDrawer drawer,
			DataGrid outputBox,double[] location)
		{
			Point canvasSize = new Point(canvas.Bounds.Width, canvas.Bounds.Height);

			ReversePolandLogic reversePoland = new ReversePolandLogic(_function, drawer);
			reversePoland.StacKInstalization();

			Function function = new Function(new FunctionDrawer(outputBox, canvas, drawer));
			function.Render(canvasSize, new Point(location[0], location[1]),
				reversePoland.GetStack(), location[2]);
		}
	}
	static class TextboxArrayExtension
	{
		public static double[] ToDouble(this TextBox[] boxes)
		{
			return new double[]
			{ 
				Convert.ToDouble(boxes[0].Text),
				Convert.ToDouble(boxes[1].Text),
				Convert.ToDouble(boxes[2].Text)
			};
		}
	}
	public class Drawer : IDrawer
	{
		private TextBox tbx { get; }
		public Drawer(TextBox _tbx)
		{
			tbx = _tbx;
		}
		public void Draw(string input)
		{
			this.tbx.Clear();
			this.tbx.Text += input;
		}
	}
}
