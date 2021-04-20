using System;
using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public abstract class Error
	{
		public Error(string errorType)
		{
			throw new Exception(errorType);
		}
	}
	public class FormatError : Error
	{
		public FormatError(string errorType) : base(errorType)
		{
			throw new FormatException(errorType);
		}
		public static bool CheckOnPriority(string example)
		{
			string error = "Неправельное количество скобочек";
			if (example.Contains("("))
			{
				if (example.CountOpperation(example.IndexOf("(")) == -1)
				{
					throw new FormatException(error);
				}
				else
				{
					return true;
				}
			}
			else return true;
		}
		public static bool CheckOnUncorrectFormula(Stack<Element> el, int num)
		{
			string error = "Неправельно сбалансировання формула";
			if (el.Count < num)
				_ = new FormatError(error);

			return false;
		}
	}
	public class ArgumentError : Error
	{
		public ArgumentError(string errorType) : base(errorType)
		{
			throw new ArgumentException(errorType);
		}

		public static bool CheckOnUncorrectAnswer(Stack<Element> el)
		{
			string error = "Неправельное количество операций";
			if (el.Count != 1)
				_ = new ArgumentError(error);

			return false;
		}
	}
	public class NaNError : Error
	{
		public NaNError(string errorType) : base(errorType)
		{
			throw new NotFiniteNumberException(errorType);
		}
		public static bool CanConvertToDouble(string num)
		{
			string error = "Неправельный тип данных в параметре";
			if (!double.TryParse(num, out _))
				_ = new NaNError(error);

			return true;
		}
	}

}
