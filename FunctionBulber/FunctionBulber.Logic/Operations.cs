﻿using System;


namespace FunctionBulber.Logic
{
	public abstract class Operations
	{
		public abstract string Name { get; }
		public abstract short CountNum { get; }
		public abstract int Priority { get; }
		public abstract double Count(double[] nums);
		public abstract bool Equals(Operations opp);
	}
	public class Add : Operations
	{
		public override string Name => "+";
		public override short CountNum => 2;
		public override int Priority => 1;
		public override double Count(double[] nums) => nums[1] + nums[0];
		public override bool Equals(Operations opp)
		{
			return opp is Add;
		}

	}
	public class Sub : Operations
	{
		public override string Name => "-";
		public override short CountNum => 2;
		public override int Priority => 1;
		public override double Count(double[] nums) => nums[1] - nums[0];
		public override bool Equals(Operations opp)
		{
			return opp is Sub;
		}
	}
	public class Mul : Operations
	{
		public override string Name => "*";
		public override short CountNum => 2;
		public override int Priority => 2;
		public override double Count(double[] nums) => nums[1] * nums[0];
		public override bool Equals(Operations opp)
		{
			return opp is Mul;
		}
	}
	public class Div : Operations
	{
		public override string Name => "/";
		public override short CountNum => 2;
		public override int Priority => 2;
		public override double Count(double[] nums) => nums[1] / nums[0];
		public override bool Equals(Operations opp)
		{
			return opp is Div;
		}
	}
	public class Mod : Operations
	{
		public override string Name => "%";
		public override short CountNum => 2;
		public override int Priority => 2;
		public override double Count(double[] nums) => nums[1] % nums[0];
		public override bool Equals(Operations opp)
		{
			return opp is Mod;
		}
	}
	public class Fucktorial : Operations
	{
		public override string Name => "!";
		public override short CountNum => 1;
		public override int Priority => 3;
		public override double Count(double[] nums)
		{
			if (nums[0] >= 0)
			{
				if (nums[0] <= 0)
					return 1;
				else
					return nums[0] * Count(new double[] { nums[0] - 1 });
			}
			else
				return double.NaN;
		}
		public override bool Equals(Operations opp)
		{
			return opp is Fucktorial;
		}
	}
	public class Degree : Operations
	{
		public override string Name => "^";
		public override short CountNum => 2;
		public override int Priority => 3;
		public override double Count(double[] nums) => Math.Pow(nums[1], nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Degree;
		}
	}
	public class Sin : Operations
	{
		public override string Name => "sin";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Sin(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Sin;
		}
	}
	public class Asin : Operations
	{
		public override string Name => "asin";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Asin(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Asin;
		}
	}
	public class Cos : Operations
	{
		public override string Name => "cos";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Cos(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Cos;
		}
	}
	public class Acos : Operations
	{
		public override string Name => "acos";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Acos(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Acos;
		}
	}
	public class Tan : Operations
	{
		public override string Name => "tg";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Tan(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Tan;
		}
	}
	public class Atan : Operations
	{
		public override string Name => "atg";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Atan(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Atan;
		}
	}
	public class Ctg : Operations
	{
		public override string Name => "ctg";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => 1 / Math.Tan(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Ctg;
		}
	}
	public class Actg : Operations
	{
		public override string Name => "actg";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => 1 / Math.Atan(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Actg;
		}
	}
	public class Ln : Operations
	{
		public override string Name => "ln";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Log(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Ln;
		}
	}
	public class Log : Operations
	{
		public override string Name => "log";
		public override short CountNum => 2;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Log(nums[0], nums[1]);
		public override bool Equals(Operations opp)
		{
			return opp is Log;
		}
	}
	public class Sqrt : Operations
	{
		public override string Name => "sqrt";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Sqrt(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Sqrt;
		}
	}
	public class Postfix : Operations
	{
		public override string Name => "postfix";
		public override short CountNum => 1;
		public override int Priority => 2;
		public override double Count(double[] nums) => nums[0] * -1;
		public override bool Equals(Operations opp)
		{
			return opp is Postfix;
		}
	}
	public class Other : Operations
	{
		public override string Name => ")";
		public override short CountNum => 0;
		public override int Priority => 5;
		public override double Count(double[] nums) => 0;
		public override bool Equals(Operations opp)
		{
			return opp is Other;
		}
	}
	public class Other2 : Operations
	{
		public override string Name => "(";
		public override short CountNum => 0;
		public override int Priority => 0;
		public override double Count(double[] nums) => 0;
		public override bool Equals(Operations opp)
		{
			return opp is Other2;
		}
	}
	public class Abs : Operations
	{
		public override string Name => "abs";
		public override short CountNum => 1;
		public override int Priority => 4;
		public override double Count(double[] nums) => Math.Abs(nums[0]);
		public override bool Equals(Operations opp)
		{
			return opp is Abs;
		}
	}
}
