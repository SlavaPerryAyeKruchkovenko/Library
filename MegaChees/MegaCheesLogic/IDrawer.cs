using System.Collections.Generic;

namespace MegaChess.Logic
{
	public interface IDrawer
	{
		void PrintBoard(Dictionary<char, Dictionary<char, Figura>> board);
		void Clear();
		void MoveCursor(int x, int y, Dictionary<char, Dictionary<char, Figura>> board, out int newX, out int newY);
		char ConvertToTKeyFormat(int x, int y, out char key);
		void CursorVisible(bool visible);
		int ConvertToLocationFormat(char i, char j, out int y);
	}
}
