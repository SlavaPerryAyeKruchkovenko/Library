using System;
using System.Threading;
using System.Threading.Tasks;



namespace MegaCheess.Menu
{
	using MegaChess.Logic;
	using System.Drawing;

	class Printer : IDrawer
	{
		private static int coordinateX = Console.WindowWidth / 2 - 39;
		private static readonly int coordininateY = -1;
		private static readonly int delay = 100;
		private static readonly int SecondDelay = 10;

		public void CursorVisible(bool visible)
		{
			Console.CursorVisible = visible;
		}
		public void Clear()
		{
			EraseNameSpace(MenuDataBase.start_Y_ForNameSpace, 21, 5, 0);
			Thread.Sleep(delay);
			EraseNameSpace(MenuDataBase.start_Y_ForNewGameSpace, 33, 3, 2);
			Thread.Sleep(delay);
			EraseNameSpace(MenuDataBase.start_Y_ForContinueSpace, 33, 3, 4);
			Thread.Sleep(delay);
			EraseNameSpace(MenuDataBase.start_Y_ForScoreSpace, 22, 3, 6);
			Thread.Sleep(delay);
			EraseNameSpace(MenuDataBase.start_Y_ForExitSpace, 18, 3, 8);
			Thread.Sleep(delay);
		}
		public void PrintBoard(Board board)
		{
			Console.Clear();
			int step = 2;
			PrintOpenBar();
			for (char i = '8'; i >= '1'; i--)
			{
				Console.SetCursorPosition(MenuDataBase.CordinateX, MenuDataBase.CordinateY + step);
				Console.Write(i);
				for (char j = 'A'; j <= 'H'; j++)
				{
					ReserveColor();
					Console.Write(MenuDataBase.stickBar);
					Console.BackgroundColor = SwitchBoardColor(i, j);

					if (!(board.GetFigure(i,j) is Empty))
					{
						Console.ForegroundColor = ConvertToFigura(board.GetFigure(i,j));
						Console.Write($" {board.GetFigure(i, j)} ");
					}
					else
						Console.Write("   ");
				}
				ReserveColor();
				Console.Write(MenuDataBase.stickBar);
				PrintMiddleBar(step + 1);
				step += 2;

			}
			ReserveColor();
			PrintCloseBar();
		}
		public char[] ConvertToTKeyFormat(int x,int y)
		{
			var key1 = y switch
			{
				1 => '1',
				2 => '2',
				3 => '3',
				4 => '4',
				5 => '5',
				6 => '6',
				7 => '7',
				8 => '8',
				_ => ' ',
			};
			var key2 = x switch
			{
				1 => 'A',
				2 => 'B',
				3 => 'C',
				4 => 'D',
				5 => 'E',
				6 => 'F',
				7 => 'G',
				8 => 'H',
				_ => ' ',
			};
			return new char[] { key1, key2 };
		}
		public Point ConvertToLocationFormat(char i, char j)
		{
			int y = 30 - ((Convert.ToInt32(i.ToString()) - 1) * 2 + 8);
			int x = (j - 'A') * 4 + 10;
			return new Point(x, y);
		}
		public Figura MoveCursor(int x, int y, Board board)
		{
			int newX = x;
			int newY = y;
			ConsoleKeyInfo key = new ConsoleKeyInfo();
			while (key.Key != ConsoleKey.Enter)
			{
				Console.SetCursorPosition(newX, newY);
				key = Console.ReadKey();
				PrintBoard(board);

				if (key.Key == ConsoleKey.UpArrow && newY - 2 > 6)
					newY -= 2;
				else if (key.Key == ConsoleKey.DownArrow && newY + 2 < 24)
					newY += 2;
				else if (key.Key == ConsoleKey.LeftArrow && newX - 4 > 6)
					newX -= 4;
				else if (key.Key == ConsoleKey.RightArrow && newX + 4 < 40)
					newX += 4;
				else if (key.Key == ConsoleKey.Escape)
					Environment.Exit(0);
			}
			newX = (newX-2)/4 - 1;
			newY = 8-((newY-8)/2);
			var location = ConvertToTKeyFormat(newX, newY);
			return board.GetFigure(location[0], location[1]);
		}
		public static void PrintMenu()
		{
			Console.Clear();
			coordinateX = Console.WindowWidth / 2 - 20;

			if (Console.WindowWidth <= 80)
				ErrorMesange();

			PrintNameSpace('E', '5', MenuDataBase.start_Y_ForNameSpace, 7, true, "name space", 7, MenuDataBase.NameSpaceColor);
			PrintNameSpace('C', '9', MenuDataBase.start_Y_ForNewGameSpace, 7, false, "new game space", 20, MenuDataBase.NewGameSpaceColor);
			PrintNameSpace('C', ':', MenuDataBase.start_Y_ForContinueSpace, 7, false, "continue space", 20, MenuDataBase.ContinueSpaceColor);
			PrintNameSpace('C', '7', MenuDataBase.start_Y_ForScoreSpace, 6, false, "score space", 8, MenuDataBase.ScoreSpaceColor);
			PrintNameSpace('C', '5', MenuDataBase.start_Y_ForExitSpace, 6, false, "exit space", 4, MenuDataBase.ExitSpaceColor);

			Thread.Sleep(delay);
		}
		private static void PrintNameSpace(char firstRestriction, char secondRestriction, int displaceY, int displaceX, bool haveShortName, string nameSpace, int indent, ConsoleColor foregrount)
		{
			Console.ForegroundColor = foregrount;

			int x = coordinateX - indent, y = coordininateY + displaceY;
			for (char letter = 'A'; letter <= firstRestriction; letter++)
			{
				y++;

				if (haveShortName)
					PrintShortNameSpace(displaceY);

				for (char num = '1'; num <= secondRestriction; num++)
				{
					x += displaceX;
					Console.SetCursorPosition(x, y);

					if (nameSpace == "new game space" && num == '5')
						x += 7;
					PrintElementOfDictionary(nameSpace, letter, num);
				}
				x = coordinateX - indent;
			}
			if (haveShortName)
				Console.Write("V 2.0");
		}
		private static void PrintShortNameSpace(int displaceY)
		{
			for (int i = 0; i < 5; i++)
			{
				Console.SetCursorPosition(coordinateX - 1, coordininateY + displaceY + 1 + i);
				Console.WriteLine(MenuDataBase.shortName[i]);
			}

		}
		private static void PrintElementOfDictionary(string nameSpace, char letter, char num)
		{
			switch (nameSpace)
			{
				case "name space": Console.Write(MenuDataBase.nameSpace[letter][num.ToString()]); break;
				case "new game space": Console.Write(MenuDataBase.newGameSpace[letter][num.ToString()]); break;
				case "continue space": Console.Write(MenuDataBase.continueSpace[letter][num.ToString()]); break;
				case "score space": Console.Write(MenuDataBase.scoreSpace[letter][num.ToString()]); break;
				case "exit space": Console.Write(MenuDataBase.exitSpace[letter][num.ToString()]); break;
			}
		}
		async private static void ErrorMesange()
		{
			while (Console.WindowWidth <= 80)
			{
				Console.Clear();
				Console.WriteLine(MenuDataBase.ErorrString);
				await Task.Delay(delay);
			}
		}
		private static void EraseNameSpace(int startY, int startX, int height, int speed)
		{
			for (int i = 0; i <= startX; i++)
			{
				for (int j = startY; j < startY + height; j++)
				{
					Console.SetCursorPosition(Console.WindowWidth / 2 - i, j);
					Console.WriteLine(" ");
					Console.SetCursorPosition(Console.WindowWidth / 2 + i, j);
					Console.WriteLine(" ");
					Thread.Sleep(SecondDelay - speed);
				}
			}
		}
		private static void ReserveColor()
		{
			Console.BackgroundColor = MenuDataBase.standartColorBack;
			Console.ForegroundColor = MenuDataBase.standartColorForeGround;
		}
		private static void PrintOpenBar()
		{
			Console.SetCursorPosition(MenuDataBase.CordinateX + 1, MenuDataBase.CordinateY);
			Console.Write(MenuDataBase.letterBar);
			Console.SetCursorPosition(MenuDataBase.CordinateX + 1, MenuDataBase.CordinateY + 1);
			Console.Write(MenuDataBase.openBoardBar);
		}
		private static void PrintMiddleBar(int step)
		{
			Console.SetCursorPosition(MenuDataBase.CordinateX + 1, MenuDataBase.CordinateY + step);
			Console.Write(MenuDataBase.middleBoardBar);
		}
		private static void PrintCloseBar()
		{
			Console.SetCursorPosition(MenuDataBase.CordinateX + 1, MenuDataBase.CordinateY + 17);
			Console.Write(MenuDataBase.closeBoardBar);
			Console.SetCursorPosition(MenuDataBase.CordinateX + 1, MenuDataBase.CordinateY + 18);
			Console.Write(MenuDataBase.letterBar);
		}
		private static ConsoleColor SwitchBoardColor(int a, int b)
		{
			if ((a + b) % 2 == 0)
				return CheeseGameSeetingDataBase.GameSpaceColor1;
			else
				return CheeseGameSeetingDataBase.GameSpaceColor2;
		}
		private static ConsoleColor ConvertToFigura(Figura figura)
		{
			if (figura.IsMyFigura == true)
				return ConsoleColor.Red;
			else
				return ConsoleColor.DarkGray;
		}

		public void PrintError(string ex)
		{
			Console.SetCursorPosition(0, 0);
			Console.WriteLine(ex);
			Thread.Sleep(300);
		}

		public void ChangePawn(Pawn pawn, Board board)
		{
			
		}
	}
}
