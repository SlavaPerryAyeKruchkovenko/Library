using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public interface IDrawer
	{
		void Draw(string input);
	}
	public class Error
	{
		private bool haveRrror;
		private string errorType;
		private IDrawer _draw { get; set; }
		public Error(IDrawer draw)
		{
			_draw = draw;
		}
		public bool HaveError
		{
			get
			{
				if (haveRrror)
				{
					_draw.Draw(errorType);
				}
				return haveRrror;
			}
			set
			{
				haveRrror = value;
			}
		}
		public string ErrorType
		{
			get
			{
				if (haveRrror)
					return errorType;
				else
					return "NONE";
			}
			set
			{
				errorType = value;
			}
		}
		public bool CheckOnPriority(string example)
		{
			ErrorType = "Неправельное количество скобочек";
			if (example.Contains("("))
			{
				if (example.CountOpperation(example.IndexOf("(")) == -1)
					return true;
				else
					return false;
			}
			else return default;
		}
		public bool CheckOnCorrectAnswer(Stack<Element> el)
		{
			errorType = "Неправельное количество операций";
			if (el.Count != 1)
				return true;
			else
				return false;
		}
		public bool CheckOnCorrectFormula(Stack<Element> el,int num)
		{
			errorType = "Неправельно сбалансировання формула";
			if (el.Count < num)
				return true;
			else
				return false;
		}
	}
	public static class StringExtension
	{
		public static string ReplaceSeparator(this string example)
		{
			while (example.Contains(";"))
			{
				int index = example.IndexOf("log") + 3;
				example = example.Substring(0, index) + "(" + example.Substring(index);
				index = example.CountOpperation(example.IndexOf("(", index) + 1);
				example = example.Substring(0, index) + ")" + example.Substring(index);
				index = example.IndexOf(";");
				example = example.Substring(0, index) + ")(" + example.Substring(index + 1);
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
				{
					return i;
				}
			}
			return -1;
		}
	}
}
