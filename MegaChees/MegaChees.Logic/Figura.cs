using System;

namespace MegaChess.Logic
{
	public abstract class Figura
	{
		public abstract bool IsMyFigura { get; protected set; }
		public abstract short Number { get; protected set; }
		public Figura(bool figura, short num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public abstract char ShorName { get; }
		public abstract bool IsCorrectMove(Board board, int dX, int dY);
		public bool HaveUnrealSteep(Figura figura, Board board, int dX, int dY)
		{
			var kingCoordinate = FoundFigureCoordinate(board, new King(!figura.IsMyFigura, 1));
			if(!this.IsCorrectMove(board,dX,dY))
			{
				return false;
			}
			bool unrealStep = false;
			
			return unrealStep;
		}
		public static char[] FoundFigureCoordinate(Board board , Figura figura)
		{
			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = 'A'; j <= 'H'; j++)
				{
					var newFigura = board.GetFigure(i, j);
					if (newFigura.Equals(figura))
					{
						return new char[] { i, j };
					}					
				}
			}
			return default;
		}
		public override string ToString()
		{
			return this.ShorName.ToString();
		}
	}
	public class Pawn : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Pawn(bool figura, short num):base(figura,num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public bool isFirstStep = true;
		public override char ShorName => 'P';		

		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var pawnCoordinate = FoundFigureCoordinate(board, this);
			bool canDoStep = true;
			if (dX == 0)
			{
				if (this.isFirstStep && CheckCorrectMoveForDifferent(dY, 2))
				{
					for (int j = 1; j <= Math.Abs(dY); j++)
					{
						if (HaveEnemyOnPosition(pawnCoordinate[0], pawnCoordinate[1], j, 0, board, false))
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
					return HaveEnemyOnPosition(pawnCoordinate[0], pawnCoordinate[1], 1, 0, board, true);
				}
			}
			else if (CheckCorrectMoveForDifferent(dY, 1) && Math.Abs(dX) == 1)
				return CanDestroy(pawnCoordinate[0], pawnCoordinate[1], dX, board);

			return false;
		}
		private bool CheckCorrectMoveForDifferent(int dY, int distance)
		{
			if (Math.Abs(dY) <= distance)
				if (this.IsMyFigura)
					return dY > 0;
				else
					return dY < 0;
			else
				return false;
		}
		private bool HaveEnemyOnPosition(char a, char b, int step1, int step2, Board board, bool equal)
		{
			if (!equal)
				if (this.IsMyFigura)
					return !(board.GetFigure((char)(a + step1),(char)(b + step2)) is Empty);
				else
					return !(board.GetFigure((char)(a - step1),(char)(b + step2)) is Empty);
			else
				if (this.IsMyFigura)
				return board.GetFigure((char)(a + step1),(char)(b + step2)) is Empty;
			else
				return board.GetFigure((char)(a - step1),(char)(b + step2)) is Empty;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Pawn))
			{
				return false;
			}
			var figure = (Pawn)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
		private bool CanDestroy(char a, char b, int step, Board board)
		{
			if (HaveEnemyOnPosition(a, b, 1, step, board, false))
				if (this.IsMyFigura)
					return board.GetFigure((char)(a + 1), (char)(b + step)).IsMyFigura != board.GetFigure(a, b).IsMyFigura;
				else
					return board.GetFigure((char)(a - 1), (char)(b + step)).IsMyFigura != board.GetFigure(a, b).IsMyFigura;
			else
				return false;
		}
	}
	public class Rook : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Rook(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'R';
		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var rookCoordinate = FoundFigureCoordinate(board, this);
			return IsCorrectMove2(board, dX, dY, rookCoordinate);
		}
		protected internal static bool IsCorrectMove2(Board board, int dX, int dY, char[] point)
		{			
			if (dX == 0 && dY != 0)
			{
				if (CheckObstacles(dY, board, point[0], point[1], true))
					return CanDoStep(board, point[0], point[1], dY, 0);
			}
			else if (dX != 0 && dY == 0)
			{
				if (CheckObstacles(dX, board, point[0], point[1], false))
					return CanDoStep(board, point[0], point[1], 0, dX);
			}
			return false;
		}
		private static bool CheckObstacles(int dCoordinate, Board board, char a, char b, bool moveOnY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(dCoordinate); i++)
			{
				if (moveOnY)
				{
					if (dCoordinate > 0)
					{
						if (!(board.GetFigure((char)(a + i), b) is Empty))
							canDoStep = false;
					}
					else if (!(board.GetFigure((char)(a - i), b) is Empty)) 
						canDoStep = false;
				}
				else
				{
					if (dCoordinate > 0)
					{
						if (!(board.GetFigure(a, (char)(b + i)) is Empty))
							canDoStep = false;
					}
					else if (!(board.GetFigure(a, (char)(b - i)) is Empty)) 
						canDoStep = false;
				}
			}
			return canDoStep;
		}
		private static bool CanDoStep(Board board, char a, char b, int step1, int step2)
		{
			return board.GetFigure((char)(a + step1), (char)(b + step2)) is Empty
				|| board.GetFigure((char)(a + step1), (char)(b + step2)).IsMyFigura
				!= board.GetFigure(a, b).IsMyFigura;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Rook))
			{
				return false;
			}
			var figure = (Rook)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class Bishop : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Bishop(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'B';
		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var bishopCoordinate = FoundFigureCoordinate(board, this);
			return IsCorrectMove2(board, dX, dY, bishopCoordinate);
		}
		protected internal static bool IsCorrectMove2(Board board, int dX, int dY , char[] point)
		{
			if (Math.Abs(dX) == Math.Abs(dY))
				if (CheckObstacles(board, point[0], point[1], dX, dY))
					return board.GetFigure((char)(point[0] + dY), (char)(point[1] + dX)) is Empty ||
						board.GetFigure((char)(point[0] + dY), (char)(point[1] + dX)).IsMyFigura != board.GetFigure(point[0], point[1]).IsMyFigura;
			return false;
		}
		private static bool CheckObstacles(Board board, char a, char b, int lengthX, int lengthY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(lengthX); i++)
			{
				if (lengthX > 0 && lengthY > 0)
					if (!(board.GetFigure((char)(a + i), (char)(b + i)) is Empty))
						canDoStep = default;

				else if (lengthX > 0 && lengthY < 0)
					if (!(board.GetFigure((char)(a - i), (char)(b + i)) is Empty))
						canDoStep = default;

				else if (lengthX < 0 && lengthY > 0)
					if (!(board.GetFigure((char)(a + i), (char)(b - i)) is Empty))
						canDoStep = default;

				else if (lengthX < 0 && lengthY < 0)
					if (!(board.GetFigure((char)(a - i), (char)(b - i)) is Empty)) 
						canDoStep = default;
			}

			return canDoStep;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Bishop))
			{
				return false;
			}
			var figure = (Bishop)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class King : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public King(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'K';
		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var kingCoordinate = FoundFigureCoordinate(board, this);
			if (Math.Abs(dY) <= 1 && Math.Abs(dX) <= 1)
				return board.GetFigure((char)(kingCoordinate[0] + dY), (char)(kingCoordinate[1] + dX)) is Empty ||
					board.GetFigure((char)(kingCoordinate[0] + dY),(char)(kingCoordinate[1] + dX)).IsMyFigura !=
					board.GetFigure(kingCoordinate[0], kingCoordinate[1]).IsMyFigura;

			return false;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is King))
			{
				return false;
			}
			var figure = (King)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class Queen : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Queen(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'Q';
		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var queenCoordinate = FoundFigureCoordinate(board, this);
			if (dX == 0 || dY == 0)
			{
				return Rook.IsCorrectMove2(board, dX, dY, queenCoordinate);
			}
			else
			{
				return Bishop.IsCorrectMove2(board, dX, dY, queenCoordinate);
			}

		}
		public override bool Equals(object obj)
		{
			if (!(obj is Queen))
			{
				return false;
			}
			var figure = (Queen)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
	public class Knight : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Knight(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'H';
		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			var khigthCoordinate = FoundFigureCoordinate(board, this);
			int dx = Math.Abs(dX);
			int dy = Math.Abs(dY);
			if (dx + dy == 3 && dx * dy == 2)
				return board.GetFigure((char)(khigthCoordinate[0] + dY),(char)(khigthCoordinate[1] + dX)) == null ||
					board.GetFigure((char)(khigthCoordinate[0] + dY),(char)(khigthCoordinate[1] + dX)).IsMyFigura
					!= board.GetFigure(khigthCoordinate[0], khigthCoordinate[1]).IsMyFigura;
			return false;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Knight))
			{
				return false;
			}
			var figure = (Knight)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
	public class Empty : Figura
	{
		public override bool IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Empty(bool figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => ' ';

		public override bool IsCorrectMove(Board board, int dX, int dY)
		{
			return false;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Empty))
			{
				return false;
			}
			var figure = (Empty)obj;
			return figure.IsMyFigura == this.IsMyFigura && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
}