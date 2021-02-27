using System;
using System.Collections.Generic;
using System.Text;

namespace MegaCheess.Menu
{
	class MenuDataBase
	{
		public static readonly int start_Y_ForNameSpace = 3;
		public static readonly int start_Y_ForNewGameSpace = 9;
		public static readonly int start_Y_ForContinueSpace = 13;
		public static readonly int start_Y_ForScoreSpace = 17;
		public static readonly int start_Y_ForExitSpace = 21;
		public static ConsoleColor StandartColor = ConsoleColor.Yellow;
		public static ConsoleColor HighlightColor = ConsoleColor.Red;
		public static ConsoleColor NameSpaceColor = ConsoleColor.Blue;
		public static ConsoleColor NewGameSpaceColor = StandartColor;
		public static ConsoleColor ContinueSpaceColor = StandartColor;
		public static ConsoleColor ScoreSpaceColor = StandartColor;
		public static ConsoleColor ExitSpaceColor = StandartColor;
		public static readonly string TheWayOfFile = "loadgame.txt";
		public static int CordinateX = 7;
		public static int CordinateY = 6;
		public static ConsoleColor standartColorForeGround = ConsoleColor.Yellow;
		public static ConsoleColor standartColorBack = ConsoleColor.Black;
		public static readonly string letterBar = "  A   B   C   D   E   F   G   H  ";
		public static readonly string openBoardBar = "╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗";
		public static readonly string middleBoardBar = "╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣";
		public static readonly string closeBoardBar = "╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝";
		public static readonly string stickBar = "║";

		public static string ErorrString = "Ошибка, измените увеличьте размер консоли";

		public static string[] shortName = new string[5] { "C", "H", "E", "S", "S" };

		public static Dictionary<char, Dictionary<string, string>> nameSpace = new Dictionary<char, Dictionary<string, string>>()
		{
				{ 'A', new Dictionary<string, string> { { "1", "╔════╗" }, { "2", "╦    ╦" }, { "3", "╔════╗" }, { "4", "╔════╗ " }, { "5", "╔════╗ " } } },
				{ 'B', new Dictionary<string, string> { { "1", "║     " }, { "2", "║    ║" }, { "3", "║     " }, { "4", "║      " }, { "5", "║      " } } },
				{ 'C', new Dictionary<string, string> { { "1", "║     " }, { "2", "╠════╣" }, { "3", "║════╣" }, { "4", "╚════╗ " }, { "5", "╚════╗ " } } },
				{ 'D', new Dictionary<string, string> { { "1", "║     " }, { "2", "║    ║" }, { "3", "║     " }, { "4", "     ║ " }, { "5", "     ║ " } } },
				{ 'E', new Dictionary<string, string> { { "1", "╚════╝" }, { "2", "╩    ╩" }, { "3", "╚════╝" }, { "4", "╚════╝ " }, { "5", "╚════╝ " } } }

		};
		public static Dictionary<char, Dictionary<string, string>> newGameSpace = new Dictionary<char, Dictionary<string, string>>()
		{
				{ 'A', new Dictionary<string, string> { { "1", "╦  ╦" }, { "2", "╔══╗" }, { "3", "╔══╗" }, { "4", "╔══╗" }, { "5", "╔══╗" }, { "6", "╦  ╔╗" }, { "7", "╔══╗" }, { "8", "╔══╗" }, { "9", "╔══╗" } } },
				{ 'B', new Dictionary<string, string> { { "1", "║══║" }, { "2", "║  ║" }, { "3", "║══╣" }, { "4", "║══║" }, { "5", "╚╦═╣" }, { "6", "║ / ║" }, { "7", "║   " }, { "8", "╠══╝" }, { "9", "║══║" } } },
				{ 'C', new Dictionary<string, string> { { "1", "╩  ╩" }, { "2", "╚══╝" }, { "3", "╚══╝" }, { "4", "╩  ╩" }, { "5", "╚╝ ╩" }, { "6", "╚╝  ╩" }, { "7", "╩   " }, { "8", "╩   " }, { "9", "╩  ╩" } } }
		};
		public static Dictionary<char, Dictionary<string, string>> continueSpace = new Dictionary<char, Dictionary<string, string>>()
		{
				{ 'A', new Dictionary<string, string> { { "1", "╔══╗" }, { "2", "╔══╗" }, { "3", "╔══╗" }, { "4", " ╔══╗ " }, { "5", "╔══╗" }, { "6", " ╔══╗  " }, { "7", "╗ ╦ ╔" }, { "8", "╦  ╔╗" }, { "9", "╔═╦═╗" }, { ":", "╦   " } } },
				{ 'B', new Dictionary<string, string> { { "1", "║  ║" }, { "2", "╠══╝" }, { "3", "║  ║" }, { "4", "╓╩══╩╗" }, { "5", "║  ║" }, { "6", " ║  ║  " }, { "7", "╠═╬═╣" }, { "8", "║ / ║" }, { "9", "  ║  " }, { ":", "║══╗" } } },
				{ 'C', new Dictionary<string, string> { { "1", "╩  ╩" }, { "2", "╩   " }, { "3", "╚══╝" }, { "4", "╚    ╝" }, { "5", "╚══╝" }, { "6", "╚╝  ╚╝ " }, { "7", "╝ ╩ ╚" }, { "8", "╚╝  ╝" }, { "9", "  ╩  " }, { ":", "╚══╝" } } }
		};
		public static Dictionary<char, Dictionary<string, string>> scoreSpace = new Dictionary<char, Dictionary<string, string>>()
		{
				{ 'A', new Dictionary<string, string> { { "1", "╔══╗" }, { "2", "╔═══" }, { "3", "╦ -╔╗" }, { "4", "╔═╦═╗" }, { "5", "╦  ╔╗" }, { "6", "╦  ╦" }, { "7", "╔══╗" } } },
				{ 'B', new Dictionary<string, string> { { "1", "╠══╝" }, { "2", "╠═══" }, { "3", "║ / ║" }, { "4", "  ║  " }, { "5", "║ / ║" }, { "6", "║══║" }, { "7", "║   " } } },
				{ 'C', new Dictionary<string, string> { { "1", "╩   " }, { "2", "╚═══" }, { "3", "╚╝  ╩" }, { "4", "  ╩  " }, { "5", "╚╝  ╩" }, { "6", "╩  ╩" }, { "7", "╩   " } } }
		};
		public static Dictionary<char, Dictionary<string, string>> exitSpace = new Dictionary<char, Dictionary<string, string>>()
		{
				{ 'A', new Dictionary<string, string> { { "1", "╔══╗" }, { "2", "╦    ╦" }, { "3", " ╔╗╔╗ " }, { "4", "╔══╗" }, { "5", " ╔══╗ " } } },
				{ 'B', new Dictionary<string, string> { { "1", "║══╣" }, { "2", "║══╗ ║" }, { "3", "  ╠╣  " }, { "4", "║  ║" }, { "5", "╓╩══╩╗" } } },
				{ 'C', new Dictionary<string, string> { { "1", "╚══╝" }, { "2", "╚══╝ ╩" }, { "3", " ╚╝╚╝ " }, { "4", "╚══╝" }, { "5", "╩    ╩" } } }
		};
	}
}
