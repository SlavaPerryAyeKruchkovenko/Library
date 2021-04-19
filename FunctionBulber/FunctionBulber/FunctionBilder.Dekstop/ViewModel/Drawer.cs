using Avalonia.Controls;
using FunctionBulber.Logic;
using System;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class Drawer : IDrawer
	{
		private object input;
		public Drawer(object _input)
		{
			this.input = _input;
		}
		public void Draw(IDrawer.Instalize func)
		{
			try
			{
				func();
			}
			catch(Exception ex)
			{
				if(this.input is TextBox box)
				{
					box.Text = ex.Message;
				}
				else
				{
					throw new Exception("Тут будет мессендж бокс");
				}
			}
		}
	}
}
