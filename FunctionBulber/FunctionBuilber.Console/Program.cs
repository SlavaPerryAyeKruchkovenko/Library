﻿namespace FunctionBuilber.Console
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

			Stack<Element> elements = stack.StacKInstalization();
			Console.WriteLine(stack.ToString());

			double[] nums = new double[] { 0, 1, 2 };
			double answer = calculate.CountRPN(nums, elements);
			Console.WriteLine(answer);
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
