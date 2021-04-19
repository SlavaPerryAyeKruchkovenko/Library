using System.Collections.Generic;

namespace FunctionBilber.Test
{
	using FunctionBulber.Logic;
	using System;
	using Xunit;

	public class ErrorTest
	{
		[Theory]
		[MemberData(nameof(ErrorData))]
		public void TestErrorCalculate(string example)
		{
			ReversePolandLogic stack = new ReversePolandLogic(example);
			stack.StackInitialization();
			Calculate calculate = new Calculate();
			double[] nums = new double[] { 0, 1, 2 };

			Assert.Throws<Exception>(() => calculate.CountRPN(nums, stack.GetStack()));
		}
		public static IEnumerable<object[]> ErrorData()
		{
			yield return new object[] { "sin(x)+" };
			yield return new object[] { "log(1)" };
		}
		[Theory]
		[MemberData(nameof(ErrorData2))]
		public void TestErrorRPN(string example)
		{
			ReversePolandLogic stack = new ReversePolandLogic(example);

			Assert.Throws<Exception>(() => stack.StackInitialization());
		}
		public static IEnumerable<object[]> ErrorData2()
		{
			yield return new object[] { "log(a;2)" };
			yield return new object[] { "54)+2" };
		}
	}
}
