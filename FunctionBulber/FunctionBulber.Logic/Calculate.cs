using System;
using System.Collections.Generic;

namespace FunctionBulber.Logic
{
	public class Calculate
	{
		private Operations operation;
		public Calculate()
		{

		}
		public double CountRPN(double[] varabilesMeans, Stack<Element> rpn)
		{
			Stack<Element> nums = new Stack<Element>();

			int size = rpn.Count;
			for (int i = 0; i < size; i++)
			{
				Element element = new Element(null, null, 0);

				if (rpn.Peek().Opperation == null && rpn.Peek().Variable == null)
				{
					element = rpn.Pop();
					nums.Push(element);
				}
				else if (rpn.Peek().Opperation == null)
				{
					nums.Push(ReplaceNum(rpn.Pop(), varabilesMeans));
				}
				else
				{
					this.operation = rpn.Pop().Opperation;

					if (this.operation.CountNum == 2 && !FormatError.CheckOnUncorrectFormula(nums, 2))
					{
						double[] varibile = new double[] { nums.Pop().Num, nums.Pop().Num };
						element = new Element(null, null, this.operation.Count(varibile));
					}

					else if (this.operation.CountNum == 1 && !FormatError.CheckOnUncorrectFormula(nums, 1))
					{
						double[] varibile = new double[] { nums.Pop().Num };
						element = new Element(null, null, this.operation.Count(varibile));
					}
					nums.Push(element);
				}
			}

			if (!ArgumentError.CheckOnUncorrectAnswer(nums))
			{
				return nums.Pop().Num;
			}
			else
			{
				return default;
			}
		}
		private static Element ReplaceNum(Element varabile, double[] means)
		{
			switch (varabile.Variable)
			{
				case "x": return new Element(null, null, means[0]);
				case "y": return new Element(null, null, means[1]);
				default:
					break;
			}
			return default;
		}
	}
}
