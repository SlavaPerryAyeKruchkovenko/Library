using System;
using System.Collections.Generic;
using System.Text;

namespace MegaCheesLogic
{
	abstract class Figura
	{
		public abstract bool IsMyFigura { get; protected set; }
		public Figura(bool figura)
		{
			IsMyFigura = figura;
		}
		public abstract string ShorName { get; }
		public abstract bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b);

	}
	class Pawn : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Pawn(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public bool isFirstStep = true;
		public override string ShorName => " P ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			bool canDoStep = true;
			Pawn pawn = (Pawn)board[a][b];
			if (pawn.isFirstStep && finishX == startX && finishY - startY >= -2 && finishY - startY < 0)
			{
				for (int j = 1; j <= Math.Abs(finishY - startY); j++)
				{
					if (board[(char)(a - j)][b] != null)
						canDoStep = false;
				}
				if (canDoStep)
				{
					pawn.isFirstStep = false;
					board[a][b] = pawn;
					return canDoStep;
				}
			}
			else if (finishX == startX && finishY - startY == -1 && board[(char)(a - 1)][b] == null)
				return true;
			else if (finishY - startY == -1 && Math.Abs(finishX - startX) == 1)
				return board[(char)(a - 1)][(char)(b + finishX - startX)].IsMyFigura != board[a][b].IsMyFigura;
			return false;
		}
	}
	class Rook : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Rook(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override string ShorName => " R ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			if (startX == finishX && startY != finishY)
			{
				if (CheckObstacles(startY, finishY, board, a, b, true))
					return board[(char)(a + finishY - startY)][b] == null || board[(char)(a + finishY - startY)][b].IsMyFigura != board[a][b].IsMyFigura;
			}
			else if (startX != finishX && startY == finishY)
			{
				if (CheckObstacles(startX, finishX, board, a, b, false))
					return board[a][(char)(b + finishX - startX)] == null || board[a][(char)(b + finishX - startX)].IsMyFigura != board[a][b].IsMyFigura;
			}
			return false;
		}
		private static bool CheckObstacles(int startCoordinate, int finishCoordinate, Dictionary<char, Dictionary<char, Figura>> board, char a, char b, bool moveOnY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(finishCoordinate - startCoordinate); i++)
			{
				if (moveOnY)
				{
					if (finishCoordinate > startCoordinate)
					{
						if (board[(char)(a + i)][b] != null)
							canDoStep = false;
					}
					else
					if (board[(char)(a - i)][b] != null)
						canDoStep = false;
				}
				else
				{
					if (finishCoordinate > startCoordinate)
					{
						if (board[a][(char)(b + i)] != null)
							canDoStep = false;
					}
					else
					if (board[a][(char)(b - i)] != null)
						canDoStep = false;
				}
			}
			return canDoStep;
		}
	}
	class Bishop : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Bishop(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override string ShorName => " B ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			if (Math.Abs(finishX - startX) == Math.Abs(finishY - startY))
				if (CheckObstacles(board, a, b, finishX - startX, finishY - startY))
					return board[(char)(a + finishY - startY)][(char)(b + finishX - startX)] == null || board[(char)(a + finishY - startY)][(char)(b + finishX - startX)].IsMyFigura != board[a][b].IsMyFigura;
			return false;
		}
		private static bool CheckObstacles(Dictionary<char, Dictionary<char, Figura>> board, char a, char b, int lengthX, int lengthY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(lengthX); i++)
			{
				if (lengthX > 0 && lengthY > 0)
				{
					if (board[(char)(a + i)][(char)(b + i)] != null)
						canDoStep = default;
				}

				else if (lengthX > 0 && lengthY < 0)
				{
					if (board[(char)(a - i)][(char)(b + i)] != null)
						canDoStep = default;
				}
				else if (lengthX < 0 && lengthY > 0)
				{
					if (board[(char)(a + i)][(char)(b - i)] != null)
						canDoStep = default;
				}
				else if (lengthX < 0 && lengthY < 0)
				{
					if (board[(char)(a - i)][(char)(b - i)] != null)
						canDoStep = default;
				}
			}

			return canDoStep;
		}

	}
	class King : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public King(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override string ShorName => " K ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			if (Math.Abs(finishY - startY) <= 1 && Math.Abs(finishX - startX) <= 1)
				return board[(char)(a + finishY - startY)][(char)(b + finishX - startX)] == null ||
					board[(char)(a + finishY - startY)][(char)(b + finishX - startX)].IsMyFigura !=
					board[a][b].IsMyFigura;

			return false;
		}

	}
	class Queen : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Queen(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override string ShorName => " Q ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			if (startX == finishX || startY == finishY)
			{
				Rook queen = new Rook(true);
				return queen.IsCorrectMove(board, startX, startY, finishX, finishY, a, b);
			}
			else
			{
				Bishop queen = new Bishop(true);
				return queen.IsCorrectMove(board, startX, startY, finishX, finishY, a, b);
			}

		}

	}
	class Knight : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Knight(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override string ShorName => " H ";
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int startX, int startY, int finishX, int finishY, char a, char b)
		{
			int dx = Math.Abs(finishX - startX);
			int dy = Math.Abs(finishY - startY);
			if (dx + dy == 3 && dx * dy == 2)
				return board[(char)(a + finishY - startY)][(char)(b + finishX - startX)] == null || board[(char)(a + finishY - startY)][(char)(b + finishX - startX)].IsMyFigura != board[a][b].IsMyFigura;
			return false;
		}
	}
}