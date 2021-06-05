using System.Collections.Generic;
using System.Drawing;

namespace MegaChess.Logic
{
	public interface IDrawer
	{
		void PrintBoard(Board board);
		void Clear();
		Figura MoveCursor(int x, int y, Board board);
		char[] ConvertToTKeyFormat(int x, int y);
		void CursorVisible(bool visible);
		Point ConvertToLocationFormat(char i, char j);
		void PrintError(string ex);
		void ChangePawn(Pawn pawn, Board board);
	}
}
