using System;
using System.Collections.Generic;

namespace MegaChessLogic
{
	public abstract class Figura
	{
		public abstract bool IsMyFigura { get; protected set; }
		public Figura(bool figura)
		{
			IsMyFigura = figura;
		}
		public abstract char ShorName { get; }
		public abstract bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board,int dX,int dY, char a, char b);

	}
	public class Pawn : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Pawn(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public bool isFirstStep = true;
		public override char ShorName => 'P';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX,int dY, char a, char b)
		{
			bool canDoStep = true;
			if (dX==0)
			{
				if (this.isFirstStep && CheckCorrectMoveForDifferent(dY, 2))
				{
					for (int j = 1; j <= Math.Abs(dY); j++)
					{
						if (HaveEnemyOnPosition(a, b, j, 0, board, false))
						{
							canDoStep = false;
						}
					}
					if (canDoStep)
					{
						this.isFirstStep = false;
					}
					return canDoStep;
				}
				else if (CheckCorrectMoveForDifferent(dY, 1))
				{
					return HaveEnemyOnPosition(a, b, 1, 0, board, true);
				}
			}
			else if (CheckCorrectMoveForDifferent(dY, 1) && Math.Abs(dX) == 1)
				return CanDestroy(a, b, dX, board);
					
			return false;
		}
		private bool CheckCorrectMoveForDifferent(int dY,int distance)
		{
			if (Math.Abs(dY) <= distance)
				if (this.IsMyFigura)
					return dY > 0;
				else
					return dY < 0;
			else
				return false;
		}
		private bool HaveEnemyOnPosition(char a, char b, int step1,int step2,
			Dictionary<char, Dictionary<char, Figura>> board, bool equal)
		{
			if (!equal)
				if (this.IsMyFigura)
					return board[(char)(a + step1)][(char)(b +step2)] != null;
				else
					return board[(char)(a - step1)][(char)(b + step2)] != null;
			else
				if (this.IsMyFigura)
				return board[(char)(a + step1)][(char)(b + step2)] == null;
			else
				return board[(char)(a - step1)][(char)(b + step2)] == null;
		}
		private bool CanDestroy(char a, char b,int step,
			Dictionary<char, Dictionary<char, Figura>> board)
		{
			if (HaveEnemyOnPosition(a, b, 1, step, board, false))
				if (this.IsMyFigura)
					return board[(char)(a + 1)][(char)(b + step)].IsMyFigura
						!= board[a][b].IsMyFigura;
				else
					return board[(char)(a - 1)][(char)(b + step)].IsMyFigura
						!= board[a][b].IsMyFigura;
			else
				return false;
		}
	}
	public class Rook : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Rook(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override char ShorName => 'R';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX, int dY, char a, char b)
		{
			if (dX == 0 && dY != 0)
			{
				if (CheckObstacles(dY, board, a, b, true))
					return CanDoStep(board, a, b, dY, 0);
			}
			else if (dX != 0 && dY == 0) 
			{
				if (CheckObstacles(dX, board, a, b, false))
					return CanDoStep(board, a, b, 0, dX);
			}
			return false;
		}
		private static bool CheckObstacles(int dCoordinate, Dictionary<char, Dictionary<char, Figura>> board, char a, char b, bool moveOnY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(dCoordinate); i++)
			{
				if (moveOnY)
				{
					if (dCoordinate>0)
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
					if (dCoordinate>0)
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
		private bool CanDoStep(Dictionary<char, Dictionary<char, Figura>> board,char a, char b, int step1,int step2)
		{
			return board[(char)(a + step1)][(char)(b + step2)] == null 
				|| board[(char)(a + step1)][(char)(b + step2)].IsMyFigura 
				!= board[a][b].IsMyFigura;
		}
	}
	public class Bishop : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Bishop(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override char ShorName => 'B';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX, int dY, char a, char b)
		{
			if (Math.Abs(dX) == Math.Abs(dY))
				if (CheckObstacles(board, a, b, dX, dY))
					return board[(char)(a + dY)][(char)(b + dX)] == null || board[(char)(a + dY)][(char)(b + dX)].IsMyFigura != board[a][b].IsMyFigura;
			return false;
		}
		private static bool CheckObstacles(Dictionary<char, Dictionary<char, Figura>> board,
			char a, char b, int lengthX, int lengthY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(lengthX); i++)
			{
				if (lengthX > 0 && lengthY > 0)			
					if (board[(char)(a + i)][(char)(b + i)] != null)
						canDoStep = default;		
				
				else if (lengthX > 0 && lengthY < 0)
					if (board[(char)(a - i)][(char)(b + i)] != null)
						canDoStep = default;

				else if (lengthX < 0 && lengthY > 0)
					if (board[(char)(a + i)][(char)(b - i)] != null)
						canDoStep = default;

				else if (lengthX < 0 && lengthY < 0)
					if (board[(char)(a - i)][(char)(b - i)] != null)
						canDoStep = default;
			}

			return canDoStep;
		}

	}
	public class King : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public King(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override char ShorName => 'K';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX, int dY, char a, char b)
		{
			if (Math.Abs(dY) <= 1 && Math.Abs(dX) <= 1)
				return board[(char)(a + dY)][(char)(b + dX)] == null ||
					board[(char)(a + dY)][(char)(b + dX)].IsMyFigura !=
					board[a][b].IsMyFigura;

			return false;
		}

	}
	public class Queen : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Queen(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override char ShorName => 'Q';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX, int dY, char a, char b)
		{
			if (dX == 0 || dY == 0) 
			{
				Rook queen = new Rook(true);
				return queen.IsCorrectMove(board, dX, dY, a, b);
			}
			else
			{
				Bishop queen = new Bishop(true);
				return queen.IsCorrectMove(board, dX, dY, a, b);
			}

		}

	}
	public class Knight : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public Knight(bool figura) : base(figura)
		{
			IsMyFigura = figura;
		}
		public override char ShorName => 'H';
		public override bool IsCorrectMove(Dictionary<char, Dictionary<char, Figura>> board, int dX, int dY, char a, char b)
		{
			int dx = Math.Abs(dX);
			int dy = Math.Abs(dY);
			if (dx + dy == 3 && dx * dy == 2)
				return board[(char)(a + dY)]
					[(char)(b + dX)] == null ||
					board[(char)(a + dY)]
					[(char)(b + dX)].IsMyFigura
					!= board[a][b].IsMyFigura;
			return false;
		}
	}
}