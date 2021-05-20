using System;
using System.Collections.Generic;

namespace MegaChess.Logic
{
	public class Board
	{
		private Dictionary<char, Dictionary<char, Figura>> ChessBoard { get; }
		public Dictionary<short, Dictionary<short, Figura>> DeadBlackFigures { get; }
		public Dictionary<short, Dictionary<short, Figura>> DeadWhitekFigures { get; }

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
			var deadFigures = new Dictionary<short, Dictionary<short, Figura>>()
			{
				{ 1,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 2,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 3,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 4,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 5,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 6,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 7,new Dictionary<short, Figura> { { 1, null }, { 2, null } } },
				{ 8,new Dictionary<short, Figura> { { 1, null }, { 2, null } } }
			};
			this.DeadBlackFigures = deadFigures;
			this.DeadWhitekFigures = deadFigures;
		}
		public Figura GetFigure(char a, char b) => this.ChessBoard[a][b];
		public void TryReplaceFigure(Figura selectFigure, Figura replaceFigura)
		{
			try
			{
				var coordinate1 = Figura.FoundFigureCoordinate(this, selectFigure);
				var coordinate2 = Figura.FoundFigureCoordinate(this, replaceFigura);
				this.ChessBoard[coordinate1[0]][coordinate1[1]] = replaceFigura;
				this.ChessBoard[coordinate2[0]][coordinate2[1]] = selectFigure;
			}
			catch (Exception)
			{
				throw new EntryPointNotFoundException("Не пытайтесь сломать игру");
			}
		}
		public void KillFigure(Figura killer, Figura died)
		{
			try
			{
				this.CountEmptyFigure++;
				var coordinate1 = Figura.FoundFigureCoordinate(this, killer);
				var coordinate2 = Figura.FoundFigureCoordinate(this, died);				
				this.ChessBoard[coordinate2[0]][coordinate2[1]] = killer;
				this.ChessBoard[coordinate1[0]][coordinate1[1]] = new Empty(null , this.CountEmptyFigure);
			}
			catch (Exception)
			{
				this.CountEmptyFigure--;
				throw new EntryPointNotFoundException("Нельзя рубить своего");
			}
		}
	}
}
