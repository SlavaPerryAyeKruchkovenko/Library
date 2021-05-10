 using MegaChess.Logic;

namespace MegaChess.Dekstop.ViewModels
{
	class GameWindowViewModel:ViewModelBase
	{
		public  GameWindowViewModel()
		{
			this.GameBoard = new Board();
		}
		public Board GameBoard { get; }
	}
}
