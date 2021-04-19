using System;
using Xunit;
using System.Linq;

namespace FunctionBilber.Test
{
	using FunctionBulber.Logic;
	using System.Collections.Generic;

	public class ReversePolandNatacionTest
	{
		[Theory]
		[InlineData("2+2", "2 2 +")]
		[InlineData("2 * log(4 - 2 * 1;(2 + 2) * 2)+1", "2 4 2 1 * - 2 2 + 2 * log * 1 +")]
		[InlineData("2*(4-2*1^(2+2)*2)+1", "2 4 2 1 2 2 + ^ * 2 * - * 1 +")]
		public void RPN_Test(string exseption, string comparable)
		{
			ReversePolandLogic rpl = new ReversePolandLogic(exseption);
			rpl.StackInitialization();
			Assert.Equal(comparable, rpl.ToString());
		}
		[Theory]
		[MemberData(nameof(StackData))]
		public void Stack_Test(string exseption, Stack<Element> exStack)
		{
			ReversePolandLogic rpl = new ReversePolandLogic(exseption);
			rpl.StackInitialization();
			Stack<Element> stack = rpl.GetStack();
			Assert.True(rpl.Equals(new Stack<Element>(exStack)));
		}
		public static IEnumerable<object[]> StackData()
		{
			//Operations opperation, string var, double num
			yield return new object[]{ "2+2*8--4",new Stack<Element>
			{
			new Element(null,null,2),new Element(null,null,2),new Element(null,null,8),
			new Element(new Mul(),null,0),new Element(new Add(),null,0),
			new Element(null,null,4),new Element(new Postfix(),null,0),
			new Element(new Sub(),null,0)
			}
			};
			yield return new object[] {"x*sin(y)", new Stack<Element>
			{
				new Element(null,"x",0),new Element(null,"y",0),
				new Element(new Sin(),null,0),new Element(new Mul(),null,0)
			} };
		}
		[Fact]
		public void CheckSeparationWork()
		{
			string example = "log(log(log(sin(2);5!);log(2;2));cos(30))";
			example = example.ReplaceSeparator();
			Assert.DoesNotContain(example, ";");
		}
	}
}
