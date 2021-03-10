using System;
using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public class Calculate
	{
		private Stack<Element> nums = new Stack<Element>();
		private Stack<Element> rpn { get; }
		private Operations operation { get; set; }
		private Error error { get; set; }
		public Calculate(IDrawer drawer, Stack<Element> _rpn)
		{
			this.error = new Error(drawer, false, null);
			this.rpn = _rpn;
		}
		public double CountRPN(double[] varabilesMeans)
		{
			int size = this.rpn.Count;
			string errorType;
			for (int i = 0; i < size; i++)
			{
				Element el = new Element(null, null, 0);

				if (this.rpn.Peek().Opperation == null && this.rpn.Peek().Variable == null)
				{
					el.Num = this.rpn.Pop().Num;
					this.nums.Push(el);
				}
				else if (this.rpn.Peek().Opperation == null)
				{
					this.nums.Push(ReplaceNum(this.rpn.Pop(), varabilesMeans));
				}		
				else
				{
					this.operation = this.rpn.Pop().Opperation;
					
					if (this.operation.CountNum == 2)
					{
						this.error = new Error(this.error._draw, this.error.CheckOnCorrectFormula
							(this.nums, 2, out errorType),errorType);
						if (this.error.HaveError)
							break;
						el.Num = Math.Round(this.operation.Count(new double[] 
						{ this.nums.Pop().Num, this.nums.Pop().Num }),3);					
					}
						
					else if (this.operation.CountNum == 1)
					{
						this.error = new Error(this.error._draw, this.error.CheckOnCorrectFormula
							(this.nums, 1, out errorType), errorType);
						if (this.error.HaveError)
							break;
						el.Num = Math.Round(this.operation.Count(new double[] 
						{ this.nums.Pop().Num }),3);					
					}
					this.nums.Push(el);
				}			
			}
			this.error = new Error(this.error._draw, this.error.CheckOnCorrectAnswer
				(this.nums,out errorType),errorType);

			if (!this.error.HaveError)
				return this.nums.Pop().Num;
			else
				return default;
		}
		private static Element ReplaceNum(Element varabile, double[] means)
		{
			Element element = new Element(null, null, 0);
			switch (varabile.Variable)
			{
				case "x": element.Num = means[0]; break;
				case "y": element.Num = means[1]; break;
			}
			return element;
		}
	}
}
