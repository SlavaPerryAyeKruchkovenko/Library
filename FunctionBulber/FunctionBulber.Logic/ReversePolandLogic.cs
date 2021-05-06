using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace FunctionBulber.Logic
{
	public struct Element
	{
		public Operations Opperation { get; }
		public string Variable { get; }
		public double Num { get; }
		public Element(Operations opperation, string var, double num)
		{
			this.Opperation = opperation;
			this.Variable = var;
			this.Num = num;
		}
		public bool Equals(Element element2)
		{
			if (this.Num == element2.Num && this.Variable == element2.Variable)
			{
				if (this.Opperation == null)
				{
					return (element2.Opperation == null);
				}
				else
				{
					return this.Opperation.Equals(element2.Opperation);
				}
			}
			else return false;
		}
	}

#pragma warning disable CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
	public class ReversePolandLogic
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
	{
		private Stack<Element> reversePolandNotation { get; set; }
		private Stack<Element> signs { get; set; }
		private IEnumerable<Type> operations { get; set; }
		private string function { get; }
		public Stack<Element> GetStack() => this.reversePolandNotation;
		public ReversePolandLogic(string _function)
		{
			this.function = _function.Replace(" ", "").Trim().ToLower();
			this.function = this.function.Replace(":", ";");

			this.reversePolandNotation = new Stack<Element>();
			this.signs = new Stack<Element>();

			if (FormatError.CheckOnPriority(this.function))
				this.function = function.ReplaceSeparator();

			this.operations = Assembly
			.GetAssembly(typeof(Operations))
			.GetTypes()
			.Where(t => t.IsSubclassOf(typeof(Operations)));
		}

		private static readonly List<string> priority = new List<string>
		{ "+", "-", "*", "/", "%", "^", "!","sin", "cos", "tg", "ctg","sqrt",
			"ln","log","asin","acos","atg","actg","abs","(", ")" };
		private static readonly List<string> variables = new List<string>
		{ "x","y","z"};
		private static readonly List<string> consts = new List<string>
		{ "pi","e"};

		public void StackInitialization()
		{
			if (this.function == null || this.function == "")
			{
				string error = "Пустое выражение";
				_ = new FormatError(error);
			}
			for (int i = 0; i < this.function.Length; i++)
			{

				if (Char.IsDigit(this.function[i]))
				{
					Element element = new Element(null, null, FoundNum(ref i, this.function));
					this.reversePolandNotation.Push(element);
				}
				else if (IsConsts(this.function, ref i, out double num))
				{
					Element element = new Element(null, null, num);
					this.reversePolandNotation.Push(element);
				}
				else if (variables.Contains(this.function[i].ToString()))
				{
					Element element = new Element(null, this.function[i].ToString(), 0);
					this.reversePolandNotation.Push(element);
				}
				else if (this.function[i] == '-' && (i == 0 || FoundPostfix(this.function[i - 1])))
				{
					Element element = new Element(new Postfix(), null, 0);
					PushOperation(element, i);
				}
				else if (IsOperation(i, this.function, out int shift))
				{
					Element element = new Element(FoundOpperation(this.function[i..(i + shift)], this.operations), null, 0);
					i += shift - 1;
					PushOperation(element, i);
				}
				else
				{
					string error = "Ошибка недопостимый символ";
					_ = new FormatError(error);
				}
			}
			this.reversePolandNotation = new Stack<Element>(UnificationStack(this.reversePolandNotation, this.signs));
		}
		private static double FoundNum(ref int startNum, string example)
		{
			int finishNum = startNum + 1;
			while (example.Length > finishNum && (Char.IsDigit(example[finishNum]) || example[finishNum] == ','))
			{
				finishNum++;
			}
			double answer = Convert.ToDouble(example[startNum..finishNum]);
			startNum += finishNum - startNum - 1;
			return answer;
		}
		private static bool IsOperation(int functionStart, string example, out int shift)
		{
			shift = default;
			for (int i = 1; i <= 4; i++)
			{
				shift = i;
				if (priority.Contains(example[functionStart..(functionStart + i)]))
				{
					return true;
				}

			}
			return false;
		}
		private static Operations FoundOpperation(string opp, IEnumerable<Type> operations)
		{
			Operations operation = null;
			if (priority.Contains(opp))
			{
				foreach (Type item in operations)
				{
					operation = Activator.CreateInstance(item) as Operations;
					if (operation.Name == opp)
					{
						return operation;
					}
				}
			}
			return null;
		}
		private static bool FoundPostfix(char opperation)
		{
			if (opperation == ')')
				return false;
			return priority.Contains(opperation.ToString());
		}
		private void PushOperation(Element opperation, int index)
		{
			if (this.signs.Count == 0)
			{
				if (opperation.Opperation.Name != ")")
				{
					this.signs.Push(opperation);
				}
				else
				{
					string error = "Выражение не может начинаться с закрытой скобочки";
					_ = new FormatError(error);
				}
			}
			else if (opperation.Opperation.Name == "(" && index == function.Length - 1)
			{
				string error = "Выражение не может заканчиваться открывающей скобочкой";
				_ = new FormatError(error);
			}
			else if (opperation.Opperation.Name == ")")
			{
				MixingOpperation(opperation, 0, true);
			}
			else if (opperation.Opperation.Name == "(")
			{
				this.signs.Push(opperation);
			}
			else
			{
				MixingOpperation(opperation, opperation.Opperation.Priority, false);
			}
		}
		private static bool IsConsts(string example, ref int startIndex, out double num)
		{
			num = default;
			if (startIndex + 1 < example.Length && example[startIndex..(startIndex + 2)] == consts[0])
			{
				startIndex++;
				num = Math.PI;
				return true;
			}
			if (example[startIndex].ToString() == consts[1])
			{
				num = Math.E;
				return true;
			}
			else
			{
				return false;
			}
		}
		private void MixingOpperation(Element opperation, int index, bool needDelete)
		{
			int size = this.signs.Count;
			for (int i = 0; i < size; i++)
			{
				if (this.signs.Peek().Opperation.Name == "(" && needDelete)
				{
					this.signs.Pop();
					break;
				}
				else if (this.signs.Peek().Opperation.Priority >= index && this.signs.Peek().Opperation.Name != "(")
				{
					this.reversePolandNotation.Push(this.signs.Pop());
				}
				else
				{
					break;
				}

			}
			if (!needDelete)
			{
				this.signs.Push(opperation);
			}
		}
		public new string ToString()
		{
			string result = null;
			foreach (var item in this.reversePolandNotation)
			{
				if (item.Opperation == null && item.Variable == null)
				{
					result += item.Num;
				}
				else
				{
					result += item.Variable ?? item.Opperation.Name;
				}
				result += " ";
			}
			return result.Trim();
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
		public override bool Equals(object obj)
		{
			Stack<Element> exStack = (Stack<Element>)obj;
			if (this.reversePolandNotation.Count == exStack.Count)
			{
				foreach (var item in this.reversePolandNotation)
				{
					if (!item.Equals(exStack.Pop()))
					{
						return false;
					}

				}
				return true;
			}
			else
			{
				return false;
			}

		}
	}

}
