using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public class Calculate
	{
		private Error error { get; }
		private Stack<Element> nums = new Stack<Element>();
		private Operations operation { get; set; }
		public Calculate(IDrawer drawer)
		{
			error = new Error(drawer);

		}
		public double CountRPN(double[] varabilesMeans, Stack<Element> rpn)
		{
			int size = rpn.Count;
			for (int i = 0; i < size; i++)
			{
				Element el = new Element(null, null, 0);
				if (rpn.Peek().Opperation == null && rpn.Peek().Variable == null)
				{
					el.Num = rpn.Pop().Num;
					nums.Push(el);
				}
				else if (rpn.Peek().Opperation == null)
					nums.Push(ReplaceNum(rpn.Pop(), varabilesMeans));
				else
				{
					operation = rpn.Pop().Opperation;
					
					if (operation.CountNum == 2)
					{
						error.HaveError = error.CheckOnCorrectFormula(nums, 2);
						if (error.HaveError)
							break;
						el.Num = operation.Count(new double[] { nums.Pop().Num, nums.Pop().Num });					
					}
						
					else if (operation.CountNum == 1)
					{
						error.HaveError = error.CheckOnCorrectFormula(nums, 1);
						if (error.HaveError)
							break;
						el.Num = operation.Count(new double[] { nums.Pop().Num });					
					}
					nums.Push(el);
				}			
			}
			error.HaveError = error.CheckOnCorrectAnswer(nums);

			if (!error.HaveError)
				return nums.Pop().Num;
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
