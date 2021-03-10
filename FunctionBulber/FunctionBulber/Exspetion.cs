using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace FunctionBulber.Logic
{
	public static class StackExtension
	{
		public static Stack<Element> Reverse(this Stack<Element> stack)
		{
			Element[] lastOpperation = stack.ToArray();

			stack.Clear();
			foreach (var item in lastOpperation)
			{
				stack.Push(item);
			}
			return stack;
		}
		public static Stack<Element> Add(this Stack<Element> stack, Element el)
		{
			stack.Push(el);
			return stack;
		}
		public static Stack<T> Clone<T>(this Stack<T> stack)
		{
			Contract.Requires(stack != null);
			return new Stack<T>(new Stack<T>(stack));
		}
	}
	public static class StringExtension
	{
		public static string ReplaceSeparator(this string example)
		{
			while (example.Contains(";"))
			{
				int index = example.IndexOf("log") + 3;
				example = $"{example.Substring(0, index)}({example[index..]}";
				index = example.CountOpperation(example.IndexOf("(", index)+1);
				example = $"{example.Substring(0, index)}){example[index..]}";
				index = example.IndexOf(";");
				example = $"{example.Substring(0, index)})({example[(index + 1)..]}";
			}
			return example;
		}
		public static int CountOpperation(this string example, int start)
		{
			int open = 0, close = 0;
			for (int i = start; i < example.Length; i++)
			{
				if (example[i] == '(')
					open++;
				else if (example[i] == ')')
					close++;
				if (open == close)
					return i;
			}
			return -1;
		}
	}
}
