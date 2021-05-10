

namespace MegaChess.Logic
{
	public class ChessGameLogic
	{
		private int startX;

		private int startY;

		private IDrawer drawer;

		private readonly Board board;
		public ChessGameLogic(IDrawer _drawer, int _startX, int _startY)
		{
			this.drawer = _drawer;
			this.board = new Board();
			this.startX = _startX;
			this.startY = _startY;
		}

		public void ChessLogic(bool isNewGame)
		{
			this.drawer.CursorVisible(true);
			if (isNewGame)
			{
				NewGamePlay();
			}
			else
			{
				LoadGamePlay();
			}
		}
		private void NewGamePlay()
		{
			while (true)
			{
				this.drawer.PrintBoard(board.ChessBoard);
				this.drawer.MoveCursor(startX, startY, board.ChessBoard, out int x, out int y);
				SearchFigure(ref x, ref y, out char a, out char b);
				this.drawer.MoveCursor(startX, startY, board.ChessBoard, out int newX, out int newY);
				if (board.ChessBoard[a][b].IsCorrectMove(board.ChessBoard, newX - x, newY - y, a, b)
					/*&& !HaveUnrealStep(a, b)*/)
				{
					MakeStep(a, b, newX - x, newY - y);
				}
			}
		}
		private void SearchFigure(ref int x, ref int y, out char a, out char b)
		{
			b = this.drawer.ConvertToTKeyFormat(x, y, out a);
			if (this.board.ChessBoard[a][b] != null && this.board.ChessBoard[a][b].IsMyFigura)
				return;
			else
				NewGamePlay();
		}
		private void MakeStep(char a, char b, int deltaX, int deltaY)
		{
			Figura figura = board.ChessBoard[a][b];
			board.ChessBoard[(char)(a + deltaY)][(char)(b + deltaX)] = figura;
			board.ChessBoard[a][b] = null;
		}
		//private bool HaveUnrealStep(char a, char b)
		//{
		//	Figura figura = board.board[a][b];
		//	board.board[a][b] = null;
		//	int finishX = King.myKing[0];
		//	int finishY = King.myKing[1];

		//	bool unrealStep = default;
		//	for (char i = '1'; i <= '8'; i++)
		//	{
		//		for (char j = 'A'; j <= 'H'; j++)
		//		{
		//			if (board.board[i][j] != null && !unrealStep && !board.board[i][j].IsMyFigura)
		//			{
		//				int x = drawer.ConvertToLocationFormat(i, j, out int y);
		//				unrealStep = board.board[i][j].IsCorrectMove(
		//					board.board, x / 4, y / 2, finishX / 4, finishY / 2, i, j);
		//			}
		//		}
		//	}
		//	board.board[a][b] = figura;
		//	return unrealStep;
		//}

		private void LoadGamePlay()
		{

		}
	}
}
