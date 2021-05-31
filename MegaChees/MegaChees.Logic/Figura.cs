using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;

namespace MegaChess.Logic
{
	public abstract class Figura: INotifyPropertyChanged
	{
		[JsonProperty]
		public abstract bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public abstract short Number { get; protected set; }
		public Figura(bool? figura, short num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public abstract char ShorName { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		public abstract bool IsCorrectMove(Board board, Point lenght);
		public bool HaveUnrealSteep(Board board, Point coordinate)
		{
			if (board.IsWhiteMove != this.IsMyFigura) 
			{
				throw new Exception("Не ваш ход");
			}
			else if(!this.IsCorrectMove(board, coordinate))
			{
				throw ThrowUnrealStep();
			}
			else
			{
				char[] coordinates = board.FoundFigureCoordinate(this);
				Figura figura = board.GetFigure((char)(coordinates[0] + coordinate.Y), (char)(coordinates[1] + coordinate.X));
				board.MakeStep(this, figura, false);
				foreach (var item in board.GetFiguras())
				{
					if (!SingleColorsFigures(this, item) && !(item is Empty)) 
					{
						Point lenght = board.CountLengh(item, new King(this.IsMyFigura.Value, 1));
						if(item.IsCorrectMove(board, lenght))
						{
							board.TryAddImposibleMove(this.IsMyFigura.Value);
							board.MakeStep(this, figura, true);
							throw new AccessViolationException("Impossible move!");
						}
					}
				}
				board.MakeStep(this, figura, true);
				if(this is Pawn pawn)
				{
					pawn.isFirstStep = false;
				}			
			}		
			return false;
		}
		protected static bool SingleColorsFigures(Figura figura1, Figura figura2)
		{
			return figura1.IsMyFigura == figura2.IsMyFigura;
		}
		protected static bool HaventEnemyOnPosition(char[] point, Point location, Board board)
		{
			if (board.GetFigure(point[0], point[1]).IsMyFigura.Value)
				return Empty.IsEmpty(board, point, location);
			else
				return Empty.IsEmpty(board, point, new Point(location.X, -location.Y));
		}
		protected static bool IsCorrectCoordinate(char a, char b) => a <= '8' && a >= '1' && b <= 'H' && b >= 'A';
		public override string ToString() => this.ShorName.ToString();
		protected Exception ThrowUnrealStep() => new Exception("Невозможный ход");
	}
	public class Pawn : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
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
			var pawnCoordinate = board.FoundFigureCoordinate(this);
			if (lenght.X == 0)
			{
				if (this.isFirstStep && CheckCorrectMoveForDifferent(lenght.Y, 2))
				{
					for (int j = 1; j <= Math.Abs(lenght.Y); j++)
					{
						bool canDoStep = HaventEnemyOnPosition(pawnCoordinate, new Point(0, j), board);
						if (!canDoStep)
						{
							return false;
						}							
					}
					
					return true;
				}
				else if (CheckCorrectMoveForDifferent(lenght.Y, 1))
				{
					return HaventEnemyOnPosition(pawnCoordinate, new Point(0, 1), board);
				}
			}
			else if (CheckCorrectMoveForDifferent(lenght.Y, 1) && Math.Abs(lenght.X) == 1)
			{
				//проверка что в конечной точке не стоит враг
				return !SingleColorsFigures(this, board.GetFigure((char)(pawnCoordinate[0] + lenght.Y), (char)(pawnCoordinate[1] + lenght.X)));
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
		public override bool Equals(object obj)
		{
			if (!(obj is Pawn))
			{
				return false;
			}
			var figure = (Pawn)obj;
			return SingleColorsFigures(this,figure) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}	
	}
	public class Rook : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public override short Number { get; protected set; }
		public Rook(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'R';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var rookCoordinate = board.FoundFigureCoordinate(this);
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
					canDoStep = HaventEnemyOnPosition(point, new Point(0, i), board);
				}
				else
				{
					canDoStep = HaventEnemyOnPosition(point, new Point(i, 0), board);
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
			Figura figura1 = board.GetFigure((char)(a + step1), (char)(b + step2));
			Figura figura2 = board.GetFigure(a, b);
			return figura1 is Empty || !SingleColorsFigures(figura1, figura2);
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Rook))
			{
				return false;
			}
			var figure = (Rook)obj;
			return SingleColorsFigures(figure, this) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class Bishop : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public override short Number { get; protected set; }
		public Bishop(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'B';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var bishopCoordinate = board.FoundFigureCoordinate(this);
			return IsCorrectMove2(board, lenght, bishopCoordinate);
		}
		internal static bool IsCorrectMove2(Board board, Point lenght , char[] point)
		{
			if (Math.Abs(lenght.X) == Math.Abs(lenght.Y) && IsCorrectCoordinate(point[0], point[1]))
				if (CheckObstacles(board, point, lenght.X, lenght.Y))
					return !SingleColorsFigures(board.GetFigure((char)(point[0] + lenght.Y), (char)(point[1] + lenght.X)), board.GetFigure(point[0], point[1]));
			return false;
		}
		private static bool CheckObstacles(Board board, char[] point, int lengthX, int lengthY)
		{
			for (int i = 1; i < Math.Abs(lengthX); i++)
			{
				if (lengthX > 0 && lengthY > 0)
				{
					if (!Empty.IsEmpty(board, point, new Point(i, i)))
					{
						return false;
					}
						
				}					
				else if (lengthX > 0 && lengthY < 0)
				{
					if (!Empty.IsEmpty(board, point, new Point(i, -i)))
					{
						return false;
					}						
				}
				else if (lengthX < 0 && lengthY > 0)
				{
					if (!Empty.IsEmpty(board, point, new Point(-i, i)))
					{
						return false;
					}
				}	
				else if (lengthX < 0 && lengthY < 0)
				{
					if (!Empty.IsEmpty(board, point, new Point(-i, -i)))
					{
						return false;
					}
				}				
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
			return SingleColorsFigures(figure , this) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class King : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public override short Number { get; protected set; }
		public King(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'K';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var kingCoordinate = board.FoundFigureCoordinate(this);
			char a = (char)(kingCoordinate[0] + lenght.Y);
			char b = (char)(kingCoordinate[1] + lenght.X);
			if (Math.Abs(lenght.Y) <= 1 && Math.Abs(lenght.X) <= 1 && IsCorrectCoordinate(a, b))
				return !SingleColorsFigures(board.GetFigure(a, b), board.GetFigure(kingCoordinate[0], kingCoordinate[1]));

			return false;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is King))
			{
				return false;
			}
			var figure = (King)obj;
			return SingleColorsFigures(figure, this) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, ShorName);
		}
	}
	public class Queen : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public override short Number { get; protected set; }
		public Queen(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'Q';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var queenCoordinate = board.FoundFigureCoordinate(this);
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
			return SingleColorsFigures(figure, this) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
	public class Knight : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
		public override short Number { get; protected set; }
		public Knight(bool? figura, short num) : base(figura, num)
		{
			this.IsMyFigura = figura;
			this.Number = num;
		}
		public override char ShorName => 'H';
		public override bool IsCorrectMove(Board board, Point lenght)
		{
			var khigthCoordinate = board.FoundFigureCoordinate(this);
			int dx = Math.Abs(lenght.X);
			int dy = Math.Abs(lenght.Y);
			char a = (char)(khigthCoordinate[0] + lenght.Y);
			char b = (char)(khigthCoordinate[1] + lenght.X);
			if (dx + dy == 3 && dx * dy == 2 && IsCorrectCoordinate(a, b))
				return !SingleColorsFigures(board.GetFigure(a, b), board.GetFigure(khigthCoordinate[0], khigthCoordinate[1]));
			return false;
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Knight))
			{
				return false;
			}
			var figure = (Knight)obj;
			return SingleColorsFigures(figure, this) && figure.Number == this.Number;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
	public class Empty : Figura
	{
		[JsonProperty]
		public override bool? IsMyFigura { get; protected set; }
		[JsonProperty]
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
			return SingleColorsFigures(figure, this) && figure.Number == this.Number;
		}
		public static bool IsEmpty(Board board , char[] point , Point gap)
		{
			return board.GetFigure((char)(point[0] + gap.Y), (char)(point[1] + gap.X)) is Empty;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(IsMyFigura, Number);
		}
	}
}