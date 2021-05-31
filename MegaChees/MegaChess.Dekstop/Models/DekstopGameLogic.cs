using MegaChess.Logic;
using System.Threading;
using System.Threading.Tasks;

namespace MegaChess.Dekstop.Models
{
	public class DekstopGameLogic:BaseModel
	{
		IDrawer drawer;
		public DekstopGameLogic(IDrawer _drawer)
		{
			this.drawer = _drawer;
		}
		public void StartGame()
		{
			Game game = new Game(this.drawer,0,0);
			Task t = new Task(()=>game.ChessLogic(true));
			t.Start();
		}
	}
}
