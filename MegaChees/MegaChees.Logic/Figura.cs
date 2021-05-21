using System;
using System.Drawing;

namespace MegaChess.Logic
{
	public abstract class Figura
	{
		public abstract bool? IsMyFigura { get; protected set; }
		public abstract short Number { get; protected set; }
		public Figura(bool? figura, short num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public abstract char ShorName { get; }
		public abstract bool IsCorrectMove(Board board, Point lenght);
		public bool HaveUnrealSteep(Board board, Point coordinate)
		{
			if(!this.IsCorrectMove(board,coordinate))
			{
				return true;
			}
			else
			{
				foreach (var item in board.GetFiguras())
				{
					if(item.IsMyFigura != this.IsMyFigura && !(item is Empty))
					{
						Point lenght = board.CountLengh(new King(this.IsMyFigura.Value, 1), item);
						if(item.IsCorrectMove(board, lenght))
						{
							throw new AccessViolationException("Impossible move!");
						}
					}
				}
			}		
			return false;
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
			throw new ArgumentException("Такой фигуры нет");
		}
		protected static bool HaventEnemyOnPosition(char[] point, Point location, Board board)
		{
			if (board.GetFigure(point[0], point[1]).IsMyFigura.Value)
				return Empty.IsEmpty(board, point, location);
			else
				return Empty.IsEmpty(board, point, new Point(-location.X, location.Y));
		}
		protected static bool IsCorrectCoordinate(char a, char b) => a <= '8' && a >= '1' && b <= 'H' && b >= 'A';
		public override string ToString()
		{
			return this.ShorName.ToString();
		}
	}
	public class Pawn : Figura
	{
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Pawn(bool? figura, short num):base(figura,num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public bool isFirstStep = true;
		public override char ShorName => 'P';		

		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var pawnCoordinate = FoundFigureCoordinate(board, this);
			if (lenght.X == 0)
			{
				if (this.isFirstStep && CheckCorrectMoveForDifferent(lenght.Y, 2))
				{
					for (int j = 1; j <= Math.Abs(lenght.Y); j++)
					{
						bool canDoStep = HaventEnemyOnPosition(pawnCoordinate, new Point(j, 0), board);
						if (!canDoStep)
						{
							return false;
						}							
					}
					this.isFirstStep = false;
					return true;
				}
				else if (CheckCorrectMoveForDifferent(lenght.Y, 1))
				{
					return HaventEnemyOnPosition(pawnCoordinate, new Point(1, 0), board);
				}
			}
			else if (CheckCorrectMoveForDifferent(lenght.Y, 1) && Math.Abs(lenght.X) == 1)
			{
				return CanDestroy(pawnCoordinate, lenght.X, board);
			}				
			return false;
		}
		private bool CheckCorrectMoveForDifferent(int dY, int distance)
		{
			if (Math.Abs(dY) <= distance)
				if (this.IsMyFigura.Value)
					return dY > 0;
				else
					return dY < 0;
			else
				return false;
		}
		private bool CanDestroy(char[] point, int step, Board board)
		{
			if (!HaventEnemyOnPosition(point, new Point(1, step), board))
				if (this.IsMyFigura.Value)
					return board.GetFigure((char)(point[0] + 1), (char)(point[1] + step)).IsMyFigura != board.GetFigure(point[0], point[1]).IsMyFigura;
				else
					return board.GetFigure((char)(point[0] - 1), (char)(point[1] + step)).IsMyFigura != board.GetFigure(point[0], point[1]).IsMyFigura;
			else
				return false;
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
	}
	public class Rook : Figura
	{
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Rook(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'R';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var rookCoordinate = FoundFigureCoordinate(board, this);
			return IsCorrectMove2(board, lenght, rookCoordinate);
		}
		internal static bool IsCorrectMove2(Board board, Point lenght, char[] point)
		{			
			if(IsCorrectCoordinate(point[0],point[1]))
			{
				if (lenght.X == 0 && lenght.Y != 0)
				{
					if (CheckObstacles(lenght.Y, board, point, true))
						return CanDoStep(board, point[0], point[1], lenght.Y, 0);
				}
				else if (lenght.X != 0 && lenght.Y == 0)
				{
					if (CheckObstacles(lenght.X, board, point, false))
						return CanDoStep(board, point[0], point[1], 0, lenght.X);
				}
			}		
			return false;
		}
		private static bool CheckObstacles(int dCoordinate, Board board, char[] point, bool moveOnY)
		{
			bool canDoStep = true;
			for (int i = 1; i < Math.Abs(dCoordinate); i++)
			{
				if (moveOnY)
				{
					canDoStep = HaventEnemyOnPosition(point, new Point(i, 0), board);
				}
				else
				{
					canDoStep = HaventEnemyOnPosition(point, new Point(0, i), board);
				}
				if (!canDoStep)
				{
					return false;
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
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Bishop(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'B';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var bishopCoordinate = FoundFigureCoordinate(board, this);
			return IsCorrectMove2(board, lenght, bishopCoordinate);
		}
		internal static bool IsCorrectMove2(Board board, Point lenght , char[] point)
		{
			if (Math.Abs(lenght.X) == Math.Abs(lenght.Y) && IsCorrectCoordinate(point[0],point[1]))
				if (CheckObstacles(board, point, lenght.X, lenght.Y))
					return board.GetFigure((char)(point[0] + lenght.Y), (char)(point[1] + lenght.X)).IsMyFigura != board.GetFigure(point[0], point[1]).IsMyFigura;
			return false;
		}
		private static bool CheckObstacles(Board board, char[] point, int lengthX, int lengthY)
		{
			for (int i = 1; i < Math.Abs(lengthX); i++)
			{
				if (lengthX > 0 && lengthY > 0) 
					if (!Empty.IsEmpty(board, point, new Point(i, i)))
						return false;

				else if (lengthX > 0 && lengthY < 0)
					if (!Empty.IsEmpty(board, point, new Point(-i, i)))
						return false;

				else if (lengthX < 0 && lengthY > 0)
					if (!Empty.IsEmpty(board, point, new Point(i, -i)))
						return false;

				else if (lengthX < 0 && lengthY < 0)
					if (!Empty.IsEmpty(board, point, new Point(-i, -i))) 
						return false;
			}

			return true;
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
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public King(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'K';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var kingCoordinate = FoundFigureCoordinate(board, this);
			char a = (char)(kingCoordinate[0] + lenght.Y);
			char b = (char)(kingCoordinate[1] + lenght.X);
			if (Math.Abs(lenght.Y) <= 1 && Math.Abs(lenght.X) <= 1 && IsCorrectCoordinate(a, b)) 
				return board.GetFigure(a, b).IsMyFigura != board.GetFigure(kingCoordinate[0], kingCoordinate[1]).IsMyFigura;

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
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Queen(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'Q';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var queenCoordinate = FoundFigureCoordinate(board, this);
			if (lenght.X == 0 ||lenght.X == 0)
			{
				return Rook.IsCorrectMove2(board, lenght, queenCoordinate);
			}
			else
			{
				return Bishop.IsCorrectMove2(board, lenght, queenCoordinate);
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
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Knight(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'H';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var khigthCoordinate = FoundFigureCoordinate(board, this);
			int dx = Math.Abs(lenght.X);
			int dy = Math.Abs(lenght.Y);
			char a = (char)(khigthCoordinate[0] + lenght.Y);
			char b = (char)(khigthCoordinate[1] + lenght.X);
			if (dx + dy == 3 && dx * dy == 2 && IsCorrectCoordinate(a, b)) 
				return board.GetFigure(a, b).IsMyFigura != board.GetFigure(khigthCoordinate[0], khigthCoordinate[1]).IsMyFigura;
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
		public override bool? IsMyFigura { get; protected set; }
		public override short Number { get; protected set; }
		public Empty(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => ' ';

		public override bool IsCorrectMove(Board board, Point lenght)
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
		protected internal static bool IsEmpty(Board board , char[] point , Point gap)
		{
			return board.GetFigure((char)(point[0] + gap.X), (char)(point[1] + gap.Y)) is Empty;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
}