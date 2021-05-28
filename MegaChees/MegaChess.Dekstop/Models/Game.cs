using MegaChess.Logic;

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
