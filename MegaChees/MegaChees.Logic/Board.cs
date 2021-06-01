using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MegaChess.Logic
{
	public class Board
	{
		[JsonProperty]
		private Dictionary<char, Dictionary<char, Figura>> ChessBoard { get; }
		[JsonProperty]
		private Dictionary<char, Dictionary<char, Figura>> DeadBlackFigures { get; }
		[JsonProperty]
		private Dictionary<char, Dictionary<char, Figura>> DeadWhitekFigures { get; }
		[JsonProperty]
		public bool IsWhiteMove { get; private set; } = true;
		[JsonProperty]
		public short WhiteImposibleMove { get; private set; } = 0;
		[JsonProperty]
		public short BlackImposibleMove { get; private set; } = 0;

		[JsonProperty]
		private short CountEmptyFigure = 32;
		public Board()
		{
			this.ChessBoard = new Dictionary<char, Dictionary<char, Figura>>()
			{
				{ '8', new Dictionary<char, Figura> { { 'A', new Rook(false,1)}, { 'B', new Knight(false,1) }, { 'C', new Bishop(false,1) }, { 'D', new Queen(false,1) }, { 'E', new King(false,1) }, { 'F', new Bishop(false,2) }, { 'G', new Knight(false,2) }, { 'H', new Rook(false,2) } } },
				{ '7', new Dictionary<char, Figura> { { 'A', new Pawn(false,1) }, { 'B',new Pawn(false,2) }, { 'C', new Pawn(false,3) }, { 'D', new Pawn(false,4) }, { 'E', new Pawn(false,5) }, { 'F', new Pawn(false,6) }, { 'G', new Pawn(false,7) }, { 'H', new Pawn(false,8)} } },
				{ '6', new Dictionary<char, Figura> { { 'A', new Empty(null,1) }, { 'B', new Empty(null, 2) }, { 'C', new Empty(null, 3) }, { 'D', new Empty(null, 4) }, { 'E', new Empty(null, 5) }, { 'F', new Empty(null, 6) }, { 'G', new Empty(null, 7) }, { 'H', new Empty(null, 8) } } },
				{ '5', new Dictionary<char, Figura> { { 'A', new Empty(null, 9) }, { 'B', new Empty(null, 10) }, { 'C', new Empty(null, 11) }, { 'D', new Empty(null, 12) }, { 'E', new Empty(null, 13) }, { 'F', new Empty(null, 14) }, { 'G', new Empty(null, 15) }, { 'H', new Empty(null, 16) } } },
				{ '4', new Dictionary<char, Figura> { { 'A', new Empty(null, 17) }, { 'B', new Empty(null, 18) }, { 'C', new Empty(null, 19) }, { 'D', new Empty(null, 20) }, { 'E', new Empty(null, 21) }, { 'F', new Empty(null, 22) }, { 'G', new Empty(null, 23) }, { 'H', new Empty(null, 24) } } },
				{ '3', new Dictionary<char, Figura> { { 'A', new Empty(null, 25) }, { 'B', new Empty(null, 26) }, { 'C', new Empty(null, 27) }, { 'D', new Empty(null, 28) }, { 'E', new Empty(null, 29) }, { 'F', new Empty(null, 30) }, { 'G', new Empty(null, 31) }, { 'H', new Empty(null, 32) } } },
				{ '2', new Dictionary<char, Figura> { { 'A', new Pawn(true,1) }, { 'B', new Pawn(true,2) }, { 'C', new Pawn(true,3) }, { 'D',new Pawn(true,4) }, { 'E', new Pawn(true,5) }, { 'F', new Pawn(true,6) }, { 'G', new Pawn(true,7) }, { 'H',new Pawn(true,8) } } },
				{ '1', new Dictionary<char, Figura> { { 'A', new Rook(true,1)  }, { 'B', new Knight(true,1) }, { 'C', new Bishop(true,1)}, { 'D', new Queen(true,1) }, { 'E', new King(true,1) }, { 'F', new Bishop(true,2) }, { 'G', new Knight(true,2) }, { 'H', new Rook(true,2)} } }
			};
			var deadFigures = new Dictionary<char, Dictionary<char, Figura>>()
			{
				{ '1',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '2',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '3',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '4',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '5',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '6',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '7',new Dictionary<char, Figura> { { '1', null }, { '2', null } } },
				{ '8',new Dictionary<char, Figura> { { '1', null }, { '2', null } } }
			};
			this.DeadBlackFigures = deadFigures;
			this.DeadWhitekFigures = deadFigures;
		}
		public void ChangeSideMode() => this.IsWhiteMove = !this.IsWhiteMove;
		public Figura GetFigure(char a, char b) => this.ChessBoard[a][b];
		protected internal void TryAddImposibleMove(bool isWhite)
		{
			_ = isWhite ? this.WhiteImposibleMove++ : this.BlackImposibleMove++;
		}
		public void MakeStep(Figura firstFigura, Figura secondFigura, bool isReverseStep)//Сделать ход
		{
			if (secondFigura is Empty || secondFigura.IsMyFigura == firstFigura.IsMyFigura)
				TrySwitchFigure(firstFigura, secondFigura);
			else if (!isReverseStep)
				TryKillFigure(firstFigura, secondFigura);
			else
				TryReplaceFigure(firstFigura, secondFigura);
		}		
		private void TrySwitchFigure(Figura selectFigure, Figura replaceFigura)
		{
			try
			{
				var coordinate1 = FoundFigureCoordinate(selectFigure);
				var coordinate2 = FoundFigureCoordinate(replaceFigura);
				this.ChessBoard[coordinate1[0]][coordinate1[1]] = replaceFigura;
				this.ChessBoard[coordinate2[0]][coordinate2[1]] = selectFigure;
			}
			catch (Exception)
			{
				throw new EntryPointNotFoundException("Не пытайтесь сломать игру");
			}
		}
		private void TryKillFigure(Figura killer, Figura died)// рубим фигуру
		{
			try
			{
				this.CountEmptyFigure++;
				var coordinate1 = FoundFigureCoordinate(killer);
				var coordinate2 = FoundFigureCoordinate(died);				
				this.ChessBoard[coordinate2[0]][coordinate2[1]] = killer;
				this.ChessBoard[coordinate1[0]][coordinate1[1]] = new Empty(null , this.CountEmptyFigure);
				AddDiedFigure(died);
			}
			catch (Exception)
			{
				this.CountEmptyFigure--;
				throw new EntryPointNotFoundException("Нельзя рубить своего");
			}
		}
		private void TryReplaceFigure(Figura killer,Figura died)// возращаем мертвую фигуру
		{
			try
			{
				var coordinate1 = FoundFigureCoordinate(killer);
				var coordinate2 = FoundFigureCoordinate(new Empty(null, this.CountEmptyFigure));

				this.ChessBoard[coordinate1[0]][coordinate1[1]] = died;
				this.ChessBoard[coordinate2[0]][coordinate2[1]] = killer;			
				this.CountEmptyFigure--;
				DeleteDiedFigure(died);
			}
			catch (Exception)
			{
				throw new EntryPointNotFoundException("Фигуру нельзя востановить");
			}
		}
		private void AddDiedFigure(Figura died)// добовялем срубленную фигуру в правлеьный словарь
		{
			if(died.IsMyFigura.Value)
			{
				AddFigure(this.DeadWhitekFigures, died);
			}
			else
			{
				AddFigure(this.DeadWhitekFigures, died);
			}
		}
		private void DeleteDiedFigure(Figura died)// удаляем срубленную фигуру
		{
			Dictionary<char, Dictionary<char, Figura>> board;
			if (died.IsMyFigura.Value)
				board = this.DeadWhitekFigures;
			else
				board = this.DeadBlackFigures;

			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = '1'; j <= '2'; j++)
				{
					if (board[i][j].Equals(died))
					{
						board[i][j] = null;
						return;
					}
				}
			}
			throw new Exception("Нельзя востановить данную фигуру");
		}
		private static void AddFigure(Dictionary<char, Dictionary<char, Figura>> board , Figura figura)// добовляет фигуру в словарь
		{
			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = '1'; j <= '2'; j++)
				{
					if (board[i][j] == null)
					{
						board[i][j] = figura;
						return;
					}
				}
			}
			throw new Exception("Вы не можите срубить больше");
		}
		public Point CountLengh(Figura figura1, Figura figura2)// считает растояние между фигурами
		{
			var firstCoordinate = FoundFigureCoordinate(figura1);
			var secondCoordinate = FoundFigureCoordinate(figura2);
			int y = secondCoordinate[0] - firstCoordinate[0];
			int x = secondCoordinate[1] - firstCoordinate[1];
			return new Point(x, y);
		}
		public IEnumerable<Figura> GetFiguras()//возращает доску
		{
			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = 'A'; j <= 'H'; j++)
				{
					yield return this.ChessBoard[i][j];
				}
			}
		}
		public IEnumerable<Figura> GetDiedFiguras(bool isWhite)//возращает доску
		{
			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = '1'; j <= '2'; j++)
				{
					if(isWhite)
					{
						yield return this.DeadWhitekFigures[i][j];
					}						
					else
					{
						yield return this.DeadBlackFigures[i][j];
					}					
				}
			}
		}
		public char[] FoundFigureCoordinate(Figura figura)
		{
			for (char i = '1'; i <= '8'; i++)
			{
				for (char j = 'A'; j <= 'H'; j++)
				{
					var newFigura = this.GetFigure(i, j);
					if (newFigura.Equals(figura))
					{
						return new char[] { i, j };
					}
				}
			}
			throw new ArgumentException("Такой фигуры нет");
		}
	}
}
