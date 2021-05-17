using Xunit;

namespace MegaChess.Test
{
	using MegaChess.Logic;
	using System.Collections.Generic;

	public class SteepTest
	{
		[Theory]
		[MemberData(nameof(PawnData))]
		public void TestPawn(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			Assert.True(board.GetFigure(a,b).IsCorrectMove(board, dX, dY));
		}
		public static IEnumerable<object[]> PawnData()
		{
			yield return new object[] { 0, 2, '2', 'A' };
			yield return new object[] { 0, 1, '2', 'B' };
			yield return new object[] { 0, 2, '2', 'C' };
			yield return new object[] { 0, 1, '2', 'D' };
			yield return new object[] { 0, 2, '2', 'E' };
			yield return new object[] { 0, 1, '2', 'F' };
			yield return new object[] { 0, 2, '2', 'G' };
			yield return new object[] { 0, 1, '2', 'H' };
		}
		[Theory]
		[InlineData(1, 2, '1', 'B')]
		[InlineData(1, 2, '1', 'G')]
		public void TestKnight(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			Assert.True(board.GetFigure(a, b).IsCorrectMove
				(board, dX, dY));
		}
		[Theory]
		[InlineData(0, 6, '1', 'H')]
		[InlineData(-1, 0, '1', 'H')]
		public void TestRook(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			var figura = board.GetFigure('2', 'H');
			board.TryReplaceFigure(figura, null);
			board.ChessBoard['1']['G'] = null;
			Assert.True(board.ChessBoard[a][b].IsCorrectMove
				(board.ChessBoard, dX, dY, a, b));
		}
		[Theory]
		[InlineData(2, 2, '1', 'F')]
		[InlineData(-2, 2, '1', 'F')]
		public void TestBishop(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			board.ChessBoard['2']['G'] = null;
			board.ChessBoard['2']['E'] = null;
			Assert.True(board.ChessBoard[a][b].IsCorrectMove
				(board.ChessBoard, dX, dY, a, b));
		}
		[Theory]
		[MemberData(nameof(KingData))]
		public void TestKing(int dX, int dY, char a, char b)
		{
			Board board = new Board();

			board.ChessBoard['2']['E'] = null;
			board.ChessBoard['2']['F'] = null;
			board.ChessBoard['2']['D'] = null;
			board.ChessBoard['1']['F'] = null;
			board.ChessBoard['1']['D'] = null;
			Assert.True(board.ChessBoard[a][b].IsCorrectMove
				(board.ChessBoard, dX, dY, a, b));
		}
		public static IEnumerable<object[]> KingData()
		{
			yield return new object[] { 1, 1, '1', 'E' };
			yield return new object[] { -1, 1, '1', 'E' };
			yield return new object[] { 0, 1, '1', 'E' };
			yield return new object[] { 1, 0, '1', 'E' };
			yield return new object[] { -1, 0, '1', 'E' };
		}
	}
}
