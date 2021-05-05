using Avalonia.Controls;
using System;

namespace FunctionBilder.Dekstop.Model
{
	static class DoubleExtensions
	{
		public static int Length(this double num)
		{
			double num2 = (int)num;
			if (num - num2 == Math.Round(num - num2))
				return 0;
			string s = (num - num2).ToString();
			return s.Length-2;
		}
	}
	static class DoubleArrayExtensions
	{
		public static double[] Mul(this double[] nums, double num)
		{
			double[] countNums = new double[nums.Length];
			for (int i = 0; i < nums.Length; i++)
			{
				countNums[i] = nums[i] * num;
			}
			return countNums;
		}
	}
	static class TextboxArrayExtension
	{
		public static double[] ToDouble(this TextBox[] boxes)
		{
			return new double[]
			{
				Convert.ToDouble(boxes[0].Text),
				Convert.ToDouble(boxes[1].Text),
				Convert.ToDouble(boxes[2].Text)
			};
		}
	}
}
