using System;
using System.Drawing;

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
					var firstFigura = this.drawer.MoveCursor(startX, startY, this.board);
					ChangeStartLocation(firstFigura);

					var secondFigura = this.drawer.MoveCursor(startX, startY, this.board);

					Point lengh = this.board.CountLengh(firstFigura, secondFigura);
					if (!firstFigura.HaveUnrealSteep(this.board, lengh) && firstFigura.IsMyFigura == whiteSteep)
					{
						ChangeStartLocation(secondFigura);
						MakeStep(firstFigura, secondFigura);
						whiteSteep = !whiteSteep;
					}
			}
		}
		private void ChangeStartLocation(Figura figura)
		{
			char[] coordinate = Figura.FoundFigureCoordinate(this.board, figura);
			Point point = this.drawer.ConvertToLocationFormat(coordinate[0], coordinate[1]);
			this.startX = point.X;
			this.startY = point.Y;
		}
		
		private void MakeStep(Figura firstFigura, Figura secondFigura)
		{
			if (secondFigura.IsMyFigura == null || secondFigura.IsMyFigura == firstFigura.IsMyFigura)
				this.board.TryReplaceFigure(firstFigura, secondFigura);
			else
				this.board.KillFigure(firstFigura, secondFigura);
		}

		private void LoadGamePlay()
		{
			
		}
	}
}
