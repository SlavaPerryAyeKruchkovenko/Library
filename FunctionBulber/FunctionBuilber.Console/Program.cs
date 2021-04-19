namespace FunctionBuilber.Console
{
	using System;
	using FunctionBulber.Logic;
	class Program
	{
		static void Main(string[] args)
		{
			IDrawer print = new Printer();

			while (true)
			{
				print.Draw(CountFunction);
			}
		}
		static void CountFunction()
		{
			Console.WriteLine("Введите Выражение");
			string input = Console.ReadLine().Trim();
			ReversePolandLogic stack = new ReversePolandLogic(input);
			stack.StackInitialization();
			Calculate calculate = new Calculate();

			Console.WriteLine(stack.ToString());

			double[] nums = new double[] { 0, 1 };
			double answer = calculate.CountRPN(nums, stack.GetStack());
			Console.WriteLine(answer);
		}

		class Printer : IDrawer
		{
			public void Draw(IDrawer.Instalize func)
			{
				try
				{
					func();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					Program.Main(default);
				}
			}
		}

	}
}
