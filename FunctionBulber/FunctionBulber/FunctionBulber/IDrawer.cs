using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBulber.Logic
{
	public interface IDrawer
	{
		public delegate void Instalize();
		abstract void Draw(Instalize func);
	}
}
