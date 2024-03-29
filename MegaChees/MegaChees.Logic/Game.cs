﻿using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace MegaChess.Logic
{
	public class Game
	{
		private int startX;

		private int startY;

		private IDrawer drawer;

		private Board board;

		private CancellationToken token;
		public Game(IDrawer _drawer, Point coordinate)
		{
			this.drawer = _drawer;
			this.board = new Board();
			this.startX = coordinate.X;
			this.startY = coordinate.Y;
		}
		public void SetToken(ref CancellationToken newToken)
		{
			this.token = newToken;
		}
		public CancellationToken GetToken() => this.token;

		public void ChessLogic(bool vsComputer , bool isNewGame)
		{
			try
			{
				this.drawer.CursorVisible(true);
				if (isNewGame)
				{
					NewGamePlay(vsComputer);
				}
				else
				{
					LoadGamePlay(vsComputer);
				}
			}
			catch (Exception ex)
			{
				this.drawer.PrintError(ex.Message);
				this.drawer.Clear();
			}
		}
		private void FinishGame()
		{
			SaveGame(this.board);
			throw new Exception("Игра Окончена");
		}
		private void NewGamePlay(bool vsComputer)
		{
			ChessBot bot = null;
			if(vsComputer)
			{
				bot = new ChessBot(this.board);
			}
			while (IsGameFinish(this.board))
			{
				if (this.token != null && this.token.IsCancellationRequested)
				{
					FinishGame();
				}				
				try
				{
					this.drawer.PrintBoard(this.board);

					Figura[] figuras = MakeStep(vsComputer, bot);
					var firstFigura = figuras[0];
					var secondFigura = figuras[1];

					Point lengh = this.board.CountLengh(firstFigura, secondFigura);
					if (this.board.IsWhiteMove == firstFigura.IsMyFigura &&!firstFigura.HaveUnrealSteep(this.board, lengh))
					{
						ChangeStartLocation(secondFigura);
						this.board.MakeStep(firstFigura, secondFigura, false);
						
						if (firstFigura is Pawn pawn)
						{
							pawn.isFirstStep = false;
							if (PawnFinishGameBoard(firstFigura, this.board))
							{
								if(!vsComputer || this.board.IsWhiteMove)
								{
									this.drawer.ChangePawn(pawn, this.board);
								}
								else
								{
									bot.ChangePawn(pawn, this.board);
								}
							}
						}
						this.board.ChangeSideMode();
						CheckOnCheck(this.board, firstFigura);
					}
					SaveGame(this.board);
				}
				catch (Exception ex)
				{
					this.drawer.PrintError(ex.Message);

					if(ex.Message == "Мат")
					{
						for (int i = 1; i <= 3; i++)
						{
							this.board.TryAddImposibleMove(this.board.IsWhiteMove);
						}
					}	
					if(ex.Message== "Impossible move!")
					{
						this.board.TryAddImposibleMove(this.board.IsWhiteMove);
					}						
				}		
			}
		}
		private Figura[] MakeStep(bool vsComputer, ChessBot bot)
		{
			Figura firstFigura, secondFigura;
			if (!vsComputer || this.board.IsWhiteMove)
			{
				firstFigura = this.drawer.MoveCursor(this.startX, this.startY, board);
				ChangeStartLocation(firstFigura);
				secondFigura = this.drawer.MoveCursor(this.startX, this.startY, board);
				return new Figura[] { firstFigura, secondFigura };
			}
			else
			{
				return bot.MakeStep();
			}			
		}
		private static bool PawnFinishGameBoard(Figura figura , Board board)
		{
			var location = board.FoundFigureCoordinate(figura);
			if (figura.IsMyFigura.Value)
			{
				return location[0] == '8';
			}
			else
			{
				return location[0] == '1';
			}
		}
		private static bool CheckOnCheck(Board board , Figura figure)
		{
			foreach (Figura item in board.GetFiguras().Where(x=> x.IsMyFigura == figure.IsMyFigura))
			{
				var enemyKing = new King(!item.IsMyFigura.Value, 1);
				Point lenght = board.CountLengh(item, enemyKing);
				if (item.IsCorrectMove(board, lenght))
				{
					if (!KingHaveSteep(board, enemyKing) && !HaveMate(board , enemyKing.IsMyFigura))
					{
						throw new Exception("Мат");						
					}
					else
					{
						throw new Exception("Шах");
					}
				}
			}
			return false;
		}
		private static bool HaveMate(Board board , bool? isWhite)
		{
			int count = 0;
			foreach (var figura in board.GetFiguras().Where(x=> x.IsMyFigura == isWhite))
			{
				if (figura is King)
					continue;

				foreach (var enemy in board.GetFiguras().Where(x => x.IsMyFigura != isWhite))
				{
					Point lenght = board.CountLengh(figura, enemy);
					try
					{
						if(!figura.HaveUnrealSteep(board,lenght))
						{
							count++;
						}
					}
					catch(Exception)
					{

					}
					if(count >0)
					{
						return true;
					}					
				}
			}
			return false;
		}
		private static bool KingHaveSteep(Board board , King enemyKing)
		{
			int count = 0;
			foreach (var item in KingArrowFigures(enemyKing, board))
			{
				Point lenght = board.CountLengh(enemyKing, item);
				try
				{
					if (!enemyKing.HaveUnrealSteep(board, lenght))
					{
						count++;
					}
				}
				catch (Exception)
				{

				}
				if (count > 0)
				{
					return true;
				}
			}						
			return !(count == 0);
		}
		private static IEnumerable<Figura> KingArrowFigures(King enemyKing ,Board board)
		{
			var kingCoordinate = board.FoundFigureCoordinate(enemyKing);
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (j == 0 && i == 0)
					{
						continue;
					}						
					char y = (char)(kingCoordinate[0] + i);
					char x = (char)(kingCoordinate[1] + j);
					if (Figura.IsCorrectCoordinate(y, x) && !Figura.SingleColorsFigures(board.GetFigure(y, x),enemyKing))
					{
						yield return board.GetFigure(y, x);
					}						
				}
			}
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
			string figureColor;
			if (board.WhiteImposibleMove == 3)
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
		private void LoadGamePlay(bool vsComputer)
		{
			string path = Environment.CurrentDirectory + "\\save.json";
			try
			{
				string json = File.ReadAllText(path);
				var saves = JsonConvert.DeserializeObject<Board>(json, new JsonSerializerSettings()
				{
					TypeNameHandling = TypeNameHandling.All
				});
				this.board = saves ?? throw new Exception("Empty File");
			}
			catch
			{
				CreateFile(path);
			}			
			NewGamePlay(vsComputer);
		}
		private static void SaveGame(Board board)
		{
			string path = Environment.CurrentDirectory + "\\save.json";			
			SaveBoard(path, board);
		}
		private static void SaveBoard(string path, Board board)
		{
			string content = JsonConvert.SerializeObject(board, Formatting.Indented, new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.All,
			});			
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
