using System.Collections.Generic;

namespace FunctionBilber.Test
{
	using FunctionBulber.Logic;
	using System;
	using System.Threading.Tasks;
	using Xunit;

	public class ErrorTest
	{
		delegate int Test(int x, int y);
		[Theory]
		[MemberData(nameof(ErrorData))]
		public void TestErrorGuard(string example)
		{
			ReversePolandLogic stack = new ReversePolandLogic(example, null);
			stack.StacKInstalization();
			Calculate calculate = new Calculate(null,stack.GetStack());
			double[] nums = new double[] { 0, 1, 2 };

			Assert.Throws<NullReferenceException>(() => calculate.CountRPN(nums));
		}
		public static IEnumerable<object[]> ErrorData()
		{
			yield return new object[] { "log(a;2)"};
			yield return new object[] { "sin(x)+"};
			yield return new object[] { "54)+2"};
			yield return new object[] { "log(1)"};
		}
	}
}
