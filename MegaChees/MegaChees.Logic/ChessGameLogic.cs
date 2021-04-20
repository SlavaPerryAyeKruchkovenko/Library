

namespace MegaChess.Logic
{
	public class ChessGameLogic
	{
		private int startX { get; }
		private int startY { get; }
		private IDrawer drawer { get; }
		private Board board { get; }
		public ChessGameLogic(IDrawer _drawer, int _startX, int _startY)
		{
			drawer = _drawer;
			board = new Board();
			startX = _startX;
			startY = _startY;
		}

		public void ChessLogic(bool isNewGame)
		{
			drawer.CursorVisible(true);
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
				drawer.PrintBoard(board.board);
				drawer.MoveCursor(startX, startY, board.board, out int x, out int y);
				SearchFigure(ref x, ref y, out char a, out char b);
				drawer.MoveCursor(startX, startY, board.board, out int newX, out int newY);
				if (board.board[a][b].IsCorrectMove(board.board, newX - x, newY - y, a, b)
					/*&& !HaveUnrealStep(a, b)*/)
				{
					MakeStep(a, b, newX - x, newY - y);
				}
			}
		}
		private void SearchFigure(ref int x, ref int y, out char a, out char b)
		{
			b = drawer.ConvertToTKeyFormat(x, y, out a);
			if (board.board[a][b] != null && board.board[a][b].IsMyFigura)
				return;
			else
				NewGamePlay();
		}
		private void MakeStep(char a, char b, int deltaX, int deltaY)
		{
			Figura figura = board.board[a][b];
			board.board[(char)(a + deltaY)][(char)(b + deltaX)] = figura;
			board.board[a][b] = null;
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
