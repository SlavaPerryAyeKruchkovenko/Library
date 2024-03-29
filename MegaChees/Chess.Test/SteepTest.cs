using Xunit;

namespace MegaChess.Test
{
	using MegaChess.Logic;
	using System.Collections.Generic;
	using System.Drawing;

	public class SteepTest
	{
		[Theory]
		[MemberData(nameof(PawnData))]
		public void TestPawn(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			Assert.True(board.GetFigure(a,b).IsCorrectMove(board, new Point(dX, dY)));
		}
		public static IEnumerable<object[]> PawnData()
		{
			yield return new object[] { 0, -2, '7', 'A' };
			yield return new object[] { 0, -1, '7', 'B' };
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
		[InlineData(-1, -2, '8', 'B')]
		[InlineData(-1, -2, '8', 'G')]
		public void TestKnight(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			Assert.True(board.GetFigure(a, b).IsCorrectMove
				(board, new Point(dX, dY)));
		}
		[Theory]
		[InlineData(0, 6, '1', 'H')]
		[InlineData(-1, 0, '1', 'H')]
		public void TestRook(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			var figura1 = board.GetFigure('2', 'H');
			board.MakeStep(figura1, new Empty(null,1),false);
			var figura2 = board.GetFigure('1', 'G');
			board.MakeStep(figura2, new Empty(null, 2) ,false);
			Assert.True(board.GetFigure(a, b).IsCorrectMove(board, new Point(dX, dY)));
		}
		[Theory]
		[InlineData(2, 2, '1', 'F')]
		[InlineData(-2, 2, '1', 'F')]
		public void TestBishop(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			var figura1 = board.GetFigure('2', 'G');
			board.MakeStep(figura1, new Empty(null, 1),false);
			var figura2 = board.GetFigure('2', 'E');
			board.MakeStep(figura2, new Empty(null, 2),false);
			Assert.True(board.GetFigure(a, b).IsCorrectMove(board, new Point(dX, dY)));
		}
		[Theory]
		[MemberData(nameof(KingData))]
		public void TestKing(int dX, int dY, char a, char b)
		{
			Board board = new Board();
			var figura1 = board.GetFigure('2', 'E');
			board.MakeStep(figura1, new Empty(null, 1),false);
			var figura2 = board.GetFigure('2', 'F');
			board.MakeStep(figura2, new Empty(null, 2),false);
			var figura3 = board.GetFigure('2', 'D');
			board.MakeStep(figura3, new Empty(null, 3),false);
			var figura4 = board.GetFigure('1', 'F');
			board.MakeStep(figura4, new Empty(null, 4),false);
			var figura5 = board.GetFigure('1', 'D');
			board.MakeStep(figura5, new Empty(null, 5), false);
			Assert.True(board.GetFigure(a, b).IsCorrectMove(board, new Point(dX, dY)));
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
