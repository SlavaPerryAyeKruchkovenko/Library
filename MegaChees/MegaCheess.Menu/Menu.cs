using System;

namespace MegaCheess.Menu
{
	using MegaChessLogic;
	class Menu
	{
		public static void MoveArrow()
		{
			bool haveChoice;
			do
			{
				Printer.PrintMenu();
				ConsoleKeyInfo key = Console.ReadKey();

				haveChoice = CountConsoleKey(key);

			} while (!haveChoice);
		}
		private static bool CountConsoleKey(ConsoleKeyInfo key)
		{
			bool haveChoice = false;
			if (key.Key == ConsoleKey.UpArrow)
				ChangeColorUp();

			else if (key.Key == ConsoleKey.DownArrow)
				ChangeColorDown();

			else if (key.Key == ConsoleKey.Enter)
				haveChoice = ChangeGameSpace();
			return haveChoice;
		}
		private static void ChangeColorUp()
		{
			Console.Beep(130, 250);

			if (MenuDataBase.ContinueSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.NewGameSpaceColor = MenuDataBase.HighlightColor;
				MenuDataBase.ContinueSpaceColor = MenuDataBase.StandartColor;
			}
			else if (MenuDataBase.ScoreSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.ContinueSpaceColor = MenuDataBase.HighlightColor;
				MenuDataBase.ScoreSpaceColor = MenuDataBase.StandartColor;
			}
			else if (MenuDataBase.ExitSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.ScoreSpaceColor = MenuDataBase.HighlightColor;
				MenuDataBase.ExitSpaceColor = MenuDataBase.StandartColor;
			}
		}
		private static void ChangeColorDown()
		{
			Console.Beep(100, 250);

			if (MenuDataBase.NewGameSpaceColor == MenuDataBase.ContinueSpaceColor && MenuDataBase.ScoreSpaceColor == MenuDataBase.ExitSpaceColor)

				MenuDataBase.NewGameSpaceColor = MenuDataBase.HighlightColor;

			else if (MenuDataBase.NewGameSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.NewGameSpaceColor = MenuDataBase.StandartColor;
				MenuDataBase.ContinueSpaceColor = MenuDataBase.HighlightColor;
			}
			else if (MenuDataBase.ContinueSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.ContinueSpaceColor = MenuDataBase.StandartColor;
				MenuDataBase.ScoreSpaceColor = MenuDataBase.HighlightColor;
			}
			else if (MenuDataBase.ScoreSpaceColor == MenuDataBase.HighlightColor)
			{
				MenuDataBase.ScoreSpaceColor = MenuDataBase.StandartColor;
				MenuDataBase.ExitSpaceColor = MenuDataBase.HighlightColor;
			}
		}
		private static bool ChangeGameSpace()
		{
			Console.Beep(200, 250);
			Printer printer = new Printer();
			ChessGameLogic chess = new ChessGameLogic(new Printer(), 18, 20);

			if (MenuDataBase.NewGameSpaceColor == MenuDataBase.HighlightColor)
			{
				printer.Clear();
				chess.ChessLogic(true);
				return true;
			}
			if (MenuDataBase.ContinueSpaceColor == MenuDataBase.HighlightColor)
			{
				printer.Clear();
				chess.ChessLogic(false);
				return true;
			}
			if (MenuDataBase.ScoreSpaceColor == MenuDataBase.HighlightColor)
			{
				printer.Clear();
				GameSetting.WorkWithSetting();
				return true;
			}
			if (MenuDataBase.ExitSpaceColor == MenuDataBase.HighlightColor)
			{
				printer.Clear();
				Environment.Exit(0);
			}
			return false;
		}
	}
}
