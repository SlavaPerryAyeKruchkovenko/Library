using MegaChess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaChess.Dekstop.Models
{
	public class Game:BaseModel
	{
		IDrawer drawer;
		public Game(IDrawer _drawer)
		{
			this.drawer = _drawer;
		}
		public void StartGame()
		{
			ChessGameLogic game = new ChessGameLogic(this.drawer,0,0);
			game.ChessLogic(true);
		}
	}
}
