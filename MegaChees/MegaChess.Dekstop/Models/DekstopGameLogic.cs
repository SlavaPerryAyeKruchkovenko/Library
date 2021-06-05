using MegaChess.Logic;
using System.Threading;
using System.Threading.Tasks;

namespace MegaChess.Dekstop.Models
{
	public class DekstopGameLogic : BaseModel
	{
		IDrawer drawer;
		Task gameThread;
		CancellationTokenSource cancelTokenSource;
		CancellationToken token;
		public DekstopGameLogic(IDrawer _drawer)
		{
			this.cancelTokenSource = new CancellationTokenSource();
			this.token  = this.cancelTokenSource.Token;
			this.drawer = _drawer;
		}
		public void StartGame()
		{
			Game game = new Game(this.drawer, 0, 0);
			game.SetToken(ref this.token);
			this.gameThread = new Task(() => 
				game.ChessLogic(GameField.GetSettings()[0], GameField.GetSettings()[1]));
			this.gameThread.Start();
		}
		public void FinishGame()
		{
			if (this.gameThread != null)
			{
				cancelTokenSource.Cancel();
			}			
		}
	}
}
