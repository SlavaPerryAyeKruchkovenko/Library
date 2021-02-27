using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public class Calculate
	{
		private Stack<Element> nums = new Stack<Element>();
		private Operations operation { get; set; }
		private Error error { get; set; }
		public Calculate(IDrawer drawer)
		{
			this.error = new Error(drawer, false, null);
		}
		public double CountRPN(double[] varabilesMeans, Stack<Element> rpn)
		{
			int size = rpn.Count;
			string errorType;
			for (int i = 0; i < size; i++)
			{
				Element el = new Element(null, null, 0);

				if (rpn.Peek().Opperation == null && rpn.Peek().Variable == null)
				{
					el.Num = rpn.Pop().Num;
					this.nums.Push(el);
				}
				else if (rpn.Peek().Opperation == null)
				{
					this.nums.Push(ReplaceNum(rpn.Pop(), varabilesMeans));
				}		
				else
				{
					this.operation = rpn.Pop().Opperation;
					
					if (this.operation.CountNum == 2)
					{
						this.error = new Error(this.error._draw, Error.CheckOnCorrectFormula
							(this.nums, 2, out errorType),errorType);
						if (this.error.HaveError)
							break;
						el.Num = this.operation.Count(new double[] { nums.Pop().Num, nums.Pop().Num });					
					}
						
					else if (this.operation.CountNum == 1)
					{
						this.error = new Error(this.error._draw, Error.CheckOnCorrectFormula
							(this.nums, 1, out errorType), errorType);
						if (this.error.HaveError)
							break;
						el.Num = this.operation.Count(new double[] { nums.Pop().Num });					
					}
					this.nums.Push(el);
				}			
			}
			this.error = new Error(this.error._draw, Error.CheckOnCorrectAnswer
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
				case "z": element.Num = means[2]; break;
			}
			return element;
		}
	}
}
