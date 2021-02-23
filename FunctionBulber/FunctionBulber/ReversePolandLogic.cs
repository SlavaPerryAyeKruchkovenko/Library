using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace FunctionBulber.Logic
{
	public struct Element
	{
		public Operations Opperation { get; set; }
		public string Variable { get; set; }
		public double Num { get; set; }
		public Element(Operations opperation, string var, double num)
		{
			Opperation = opperation;
			Variable = var;
			Num = num;
		}
	}
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
	}
	public class ReversePolandLogic
	{
		private Stack<Element> reversePolandNotation { get; set; }
		private Stack<Element> signs { get; set; }
		private IEnumerable<Type> operations { get; set; }
		private string example { get; }

		private Error error { get; }
		public ReversePolandLogic(string input, IDrawer draw)
		{
			example = input.Replace(" ", "").Trim().ToLower();
			example = example.Replace(":", ";");

			error = new Error(draw);
			error.HaveError = error.CheckOnPriority(example);

			reversePolandNotation = new Stack<Element>();
			signs = new Stack<Element>();

			if (!error.HaveError)
				example = example.ReplaceSeparator();
			operations = Assembly
			.GetAssembly(typeof(Operations))
			.GetTypes()
			.Where(t => t.IsSubclassOf(typeof(Operations)));
		}


		private static List<string> priority = new List<string>
		{ "+", "-", "*", "/", "%", "^", "!","sin", "cos", "tg", "ctg","sqrt",
			"ln","log" , "(", ")" };
		private static List<string> variables = new List<string>
		{ "x","y","z"};
		public Stack<Element> StacKInstalization()
		{
			for (int i = 0; i < example.Length; i++)
			{
				Element element = new Element(null, null, 0);
				if (double.TryParse(example[i].ToString(), out _))
				{
					element.Num = FoundNum(i, example, out int shift);
					i += shift;
					reversePolandNotation.Push(element);
				}
				else if (FoundOpperation(example[i].ToString(), operations, out Operations opp))
				{
					if (FoundPostfix(example[i - 1].ToString()) && opp.Name == "-")
						element.Opperation = new Postfix();
					else
						element.Opperation = opp;
					PushOperation(element, i);
				}
				else if (example.Length > i + 4 && FoundFunction(i, example, out int shift))
				{
					FoundOpperation(example[i..(i + shift)], operations, out opp);
					element.Opperation = opp;
					i += shift - 1;
					PushOperation(element, i);
				}
				else if (variables.Contains(example[i].ToString()))
				{
					element.Variable = example[i].ToString();
					reversePolandNotation.Push(element);
				}
				else
				{
					error.HaveError = true;
					error.ErrorType = "Ошибка недопостимый символ";
				}
				if (error.HaveError)
					break;
			}
			return UnificationStack(reversePolandNotation, signs).Reverse();

		}
		private static double FoundNum(int startNum, string example, out int shift)
		{
			int finishNum = startNum + 1;
			while (example.Length > finishNum &&
				(double.TryParse(example[finishNum].ToString(), out _) || example[finishNum] == ','))
			{
				finishNum++;
			}

			shift = finishNum - startNum - 1;
			return Convert.ToDouble(example[startNum..finishNum]);
		}
		private static bool FoundFunction(int functionStart, string example, out int shift)
		{
			shift = 0;
			for (int i = 1; i <= 4; i++)
			{
				shift = i;
				if (priority.Contains(example[functionStart..(functionStart + i)]))
					return true;
			}
			return false;
		}
		private static bool FoundOpperation(string opp, IEnumerable<Type> operations, out Operations operation)
		{
			operation = null;
			if (priority.Contains(opp))
			{
				foreach (Type item in operations)
				{
					operation = Activator.CreateInstance(item) as Operations;
					if (operation.Name == opp)
					{
						return true;
					}
				}
			}
			return false;
		}
		private static bool FoundPostfix(string opperation)
		{
			if (opperation == ")")
				return false;
			return priority.Contains(opperation);
		}
		private void PushOperation(Element opperation, int index)
		{
			if (signs.Count == 0)
			{
				if (opperation.Opperation.Name != ")")
					signs.Push(opperation);
				else
				{
					error.HaveError = true;
					error.ErrorType = "Выражение не может начинаться с закрытой скобочки";
				}
			}
			else if (opperation.Opperation.Name == "(" && index == example.Length - 1)
			{
				error.HaveError = true;
				error.ErrorType = "Выражение не может заканчиваться открывающей скобочкой";
			}
			else if (opperation.Opperation.Name == ")")
				MixingOpperation(opperation, 0, true);
			else if (opperation.Opperation.Name == "(")
				signs.Push(opperation);
			else
				MixingOpperation(opperation, opperation.Opperation.Priority, false);
		}
		private void MixingOpperation(Element opperation, int index, bool needDelete)
		{
			int size = signs.Count;
			for (int i = 0; i < size; i++)
			{
				if (signs.Peek().Opperation.Name == "(" && needDelete)
				{
					signs.Pop();
					break;
				}
				else if (signs.Peek().Opperation.Priority >= index &&
				signs.Peek().Opperation.Name != "(")
				{
					reversePolandNotation.Push(signs.Pop());
				}
				else
					break;
			}
			if (!needDelete)
				signs.Push(opperation);

		}
		private static Stack<Element> UnificationStack(Stack<Element> elements, Stack<Element> sign)
		{
			int size = sign.Count;
			for (int i = 0; i < size; i++)
			{
				if (sign.Peek().Opperation.Name == "(" || sign.Peek().Opperation.Name == ")")
					sign.Pop();
				else
					elements.Push(sign.Pop());
			}
			return elements;
		}

	}
}
