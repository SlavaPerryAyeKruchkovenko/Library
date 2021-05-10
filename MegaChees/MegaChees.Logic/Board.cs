﻿using System.Collections.Generic;

namespace MegaChess.Logic
{
	public class Board
	{
		public Dictionary<char, Dictionary<char, Figura>> ChessBoard { get; }
		public Dictionary<short, Dictionary<short, Figura>> DeadBlackFigures { get; }
		public Dictionary<short, Dictionary<short, Figura>> DeadWhitekFigures { get; }
		public Board()
		{
			this.ChessBoard = new Dictionary<char, Dictionary<char, Figura>>()
			{
				{ '8', new Dictionary<char, Figura> { { 'A', new Rook(false)}, { 'B', new Knight(false) }, { 'C', new Bishop(false) }, { 'D', new Queen(false) }, { 'E', new King(false) }, { 'F', new Bishop(false) }, { 'G', new Knight(false) }, { 'H', new Rook(false) } } },
				{ '7', new Dictionary<char, Figura> { { 'A', new Pawn(false) }, { 'B',new Pawn(false) }, { 'C', new Pawn(false) }, { 'D', new Pawn(false) }, { 'E', new Pawn(false) }, { 'F', new Pawn(false) }, { 'G', new Pawn(false) }, { 'H', new Pawn(false)} } },
				{ '6', new Dictionary<char, Figura> { { 'A', null }, { 'B', null }, { 'C', null }, { 'D', null }, { 'E', null }, { 'F', null }, { 'G', null }, { 'H', null } } },
				{ '5', new Dictionary<char, Figura> { { 'A', null }, { 'B', null }, { 'C', null }, { 'D', null }, { 'E', null }, { 'F', null }, { 'G', null }, { 'H', null } } },
				{ '4', new Dictionary<char, Figura> { { 'A', null }, { 'B', null }, { 'C', null }, { 'D', null }, { 'E', null }, { 'F', null }, { 'G', null }, { 'H', null } } },
				{ '3', new Dictionary<char, Figura> { { 'A', null }, { 'B', null }, { 'C', null }, { 'D', null }, { 'E', null }, { 'F', null }, { 'G', null }, { 'H', null } } },
				{ '2', new Dictionary<char, Figura> { { 'A', new Pawn(true) }, { 'B', new Pawn(true) }, { 'C', new Pawn(true) }, { 'D',new Pawn(true) }, { 'E', new Pawn(true) }, { 'F', new Pawn(true) }, { 'G', new Pawn(true) }, { 'H',new Pawn(true) } } },
				{ '1', new Dictionary<char, Figura> { { 'A', new Rook(true)  }, { 'B', new Knight(true) }, { 'C', new Bishop(true)}, { 'D', new Queen(true) }, { 'E', new King(true) }, { 'F', new Bishop(true) }, { 'G', new Knight(true) }, { 'H', new Rook(true)} } }
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
		
	}
}
