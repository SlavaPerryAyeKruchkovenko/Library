namespace FunctionBuilber.Console
{
	using System;
	using System.Collections.Generic;
	using FunctionBulber.Logic;
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("Введите выражение");
			string input = Console.ReadLine().Trim();

			Printer print = new Printer();
			ReversePolandLogic stack = new ReversePolandLogic(input, print);
			Calculate calculate = new Calculate(print);

			double[] nums = new double[] { 0, 1, 2 };
			Stack<Element> elements = stack.StacKInstalization();
			WriteStack(elements);
			double answer = calculate.CountRPN(nums, elements);
			Console.WriteLine(answer);
		}
		public static void WriteStack(Stack<Element> stack)
		{
			foreach (var item in stack)
			{
				if (item.Opperation == null && item.Variable == null)
					Console.Write(item.Num);
				else if (item.Variable == null)
					Console.Write(item.Opperation.Name);
				else
					Console.Write(item.Variable);
				Console.Write(" ");
			}
			Console.WriteLine();
		}
		class Printer : IDrawer
		{
			public void Draw(string input)
			{
				Console.WriteLine(input);
			}
		}
	}
}
