using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace MegaChess.Logic
{
	class ChessBot
	{
		private Figura figura1;
		private Figura figura2;
		private Board board;
		public ChessBot(Board board)
		{
			this.board = board;
		}
		public Figura[] MakeStep()
		{
			SelectBestMove(this.board);
			var figura1 = this.figura1;
			var figura2 = this.figura2;
			this.figura1 = null;
			this.figura2 = null;
			return new Figura[] { figura1, figura2 };
		}
		private void SelectBestMove(Board board)
		{
			while(true)
			{
				var figura = RandomFigura(board.GetFiguras().Where(x => x.IsMyFigura == false));

				foreach (var secondItem in board.GetFiguras().Where(x => x.IsMyFigura != false))
				{
					Point lenght = board.CountLengh(figura, secondItem);
					try
					{
						if (!figura.HaveUnrealSteep(board, lenght))
						{
							this.figura1 = figura;
							this.figura2 = secondItem;
							return;
						}
					}
					catch (Exception ex)
					{

					}
				}
			}		
		}
		private Figura RandomFigura(IEnumerable<Figura> figuras)
		{
			var rnd = new Random();
			int number = rnd.Next(0, figuras.Count()-1);
			var figurasList = new List<Figura>(figuras);
			return figurasList[number];
		}
	}
}
