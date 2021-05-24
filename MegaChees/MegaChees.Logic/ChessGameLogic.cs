using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace MegaChess.Logic
{
	public class ChessGameLogic
	{
		private int startX;

		private int startY;

		private IDrawer drawer;

		private Board board;
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
			while (IsGameFinish(this.board))
			{				
				try
				{
					this.drawer.PrintBoard(this.board);
					var firstFigura = this.drawer.MoveCursor(startX, startY, this.board);
					ChangeStartLocation(firstFigura);

					var secondFigura = this.drawer.MoveCursor(startX, startY, this.board);

					Point lengh = this.board.CountLengh(firstFigura, secondFigura);
					if (!firstFigura.HaveUnrealSteep(this.board, lengh) && firstFigura.IsMyFigura == board.IsWhiteMove)
					{
						ChangeStartLocation(secondFigura);
						this.board.MakeStep(firstFigura, secondFigura , false);
						this.board.ChangeSideMode();
						CheckOnCheck(this.board, firstFigura);
					}
				}
				catch(Exception ex)
				{
					this.drawer.PrintError(ex.Message);
				}
				SaveGame(this.board);
			}
		}
		private static bool CheckOnCheck(Board board , Figura figure)
		{
			foreach (var item in board.GetFiguras())
			{
				if(item.IsMyFigura == figure.IsMyFigura)
				{
					Point lenght = board.CountLengh(item, new King(!item.IsMyFigura.Value, 1));
					if(item.IsCorrectMove(board,lenght))
					{
						throw new Exception("Шах");
					}
				}
			}
			return false;
		}
		private void ChangeStartLocation(Figura figura)
		{
			char[] coordinate = this.board.FoundFigureCoordinate(figura);
			Point point = this.drawer.ConvertToLocationFormat(coordinate[0], coordinate[1]);
			this.startX = point.X;
			this.startY = point.Y;
		}
		private bool IsGameFinish(Board board)
		{
			string figureColor = null;
			if(board.WhiteImposibleMove == 3)
			{
				figureColor = "Black";
			}
			else if(board.BlackImposibleMove == 3)
			{
				figureColor = "White";
			}				
			else
			{
				return true;
			}
			throw new Exception($"{figureColor} win!!!");
		}
		private void LoadGamePlay()
		{
			string path = Environment.CurrentDirectory + "\\save.json";
			try
			{
				string json = File.ReadAllText(path);
				var saves = JsonConvert.DeserializeObject<Board>(json);
				this.board = saves ?? throw new Exception("Empty File");
			}
			catch
			{
				CreateFile(path);
			}
			NewGamePlay();
		}
		private static void SaveGame(Board board)
		{
			string path = Environment.CurrentDirectory + "\\save.json";			
			SaveBoard(path, board);
		}
		private static void SaveBoard(string path, Board board)
		{
			string content = JsonConvert.SerializeObject(board);			
			File.Delete(path);
			CreateFile(path);
			File.WriteAllText(path, content);
		}
		private static void CreateFile(string path)
		{
			File.Create(path).DisposeAsync();
		}
	}
}
