﻿using System.Drawing;

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
			bool whiteSteep = true;
			while (true)
			{
				this.drawer.PrintBoard(this.board);
				var firstFigura = this.drawer.MoveCursor(startX, startY, board);
				var secondFigura = this.drawer.MoveCursor(startX, startY, board);
				
				Point lengh = CountLengh(firstFigura, secondFigura);
				if (firstFigura.IsCorrectMove(this.board, lengh.X, lengh.Y) && firstFigura.IsMyFigura == whiteSteep)
				{
					MakeStep(firstFigura, secondFigura);
				}
				whiteSteep = !whiteSteep;
			}
		}
		private Point CountLengh(Figura figura1 , Figura figura2)
		{
			var firstCoordinate = Figura.FoundFigureCoordinate(this.board, figura1);
			var secondCoordinate = Figura.FoundFigureCoordinate(this.board, figura2);
			int y = secondCoordinate[0] - firstCoordinate[0];
			int x = secondCoordinate[1] - firstCoordinate[1];
			return new Point(x, y);
		}
		private void MakeStep(Figura firstFigura, Figura secondFigura)
		{
			this.board.TryReplaceFigure(firstFigura, secondFigura);
		}

		private void LoadGamePlay()
		{

		}
	}
}
