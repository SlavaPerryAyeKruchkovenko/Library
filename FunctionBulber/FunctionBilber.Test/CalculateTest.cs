﻿using System.Collections.Generic;
using Xunit;

namespace FunctionBilber.Test
{
	using FunctionBulber.Logic;
	public class CalculateTest
	{
		[Theory]
		[MemberData(nameof(FunctionData))]
		public void TestResult(string example, double result)
		{
			ReversePolandLogic RPN = new ReversePolandLogic(example);
			RPN.StackInitialization();
			Calculate calculate = new Calculate();
			double num = calculate.CountRPN(new double[] { 0, 1 }, RPN.GetStack());
			Assert.Equal(result, num);
		}
		public static IEnumerable<object[]> FunctionData()
		{
			yield return new object[] { "log(2;2)", 1 };
			yield return new object[] { "sin(x)", 0 };
			yield return new object[] { "5!", 120 };
			yield return new object[] { "2*(2+2)", 8 };
		}
		[Theory]
		[MemberData(nameof(ExampleData))]
		public void TestStack(string example)
		{
			ReversePolandLogic RPL = new ReversePolandLogic(example);
			RPL.StackInitialization();
			Assert.NotEmpty(RPL.GetStack());
		}
		public static IEnumerable<object[]> ExampleData()
		{
			yield return new object[] { "sin(2+cos(2*2))" };
			yield return new object[] { "sqrt(10*10)" };
			yield return new object[] { "x+y+z--10" };
			yield return new object[] { "2^10/2^5" };
		}
	}
}
