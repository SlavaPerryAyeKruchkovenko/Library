using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public interface IDrawer
	{
		abstract void Draw(string input);
	}
	public class Error
	{
		private bool _haveRrror;
		private string _errorType;
		public IDrawer _draw { get; }
		public Error(IDrawer drawer, bool haveError,string errorType)
		{
			this._draw = drawer;
			this._haveRrror = haveError;
			this._errorType = errorType;
		}
		public bool HaveError
		{
			get
			{
				if (this._haveRrror)
				{
					_draw.Draw(this._errorType);
				}
				return this._haveRrror;
			}
		}
		public string ErrorType
		{
			get
			{
				if (this._haveRrror)
					return this._errorType;
				else
					return "NONE";
			}
		}
		public static bool CheckOnPriority(string example,out string error)
		{
			error = "Неправельное количество скобочек";
			if (example.Contains("("))
			{
				if (example.CountOpperation(example.IndexOf("(")) == -1)
				{
					return true;
				}	
				else
				{
					return false;
				}
					
			}
			else return default;
		}
		public bool CheckOnCorrectAnswer(Stack<Element> el,out string error)
		{
			error = "Неправельное количество операций";
			if (el.Count != 1)
				return true;
			else
				return false;
		}
		public bool CheckOnCorrectFormula(Stack<Element> el,int num,out string error)
		{
			error = "Неправельно сбалансировання формула";
			if (el.Count < num)
				return true;
			else
				return false;
		}
		public bool CanConvertToDouble(string num,out string error)
		{
			error = "Неправельный тип данных в параметре";
			return double.TryParse(num, out _); 
		}
	}
	
}
