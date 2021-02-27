using System;


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
			int fucktorial = 1;
			for (int i = 1; i <= nums[0]; i++)
			{
				fucktorial *= i;
			}
			return fucktorial;
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
		public override int Priority => 3;
		public override double Count(double[] nums) => Math.Sin(nums[0] * Math.PI / 180);
		public override bool Equals(Operations opp)
		{
			return opp is Sin;
		}
	}
	public class Cos : Operations
	{
		public override string Name => "cos";
		public override short CountNum => 1;
		public override int Priority => 3;
		public override double Count(double[] nums) => Math.Cos(nums[0] * Math.PI / 180);
		public override bool Equals(Operations opp)
		{
			return opp is Cos;
		}
	}
	public class Tan : Operations
	{
		public override string Name => "tg";
		public override short CountNum => 1;
		public override int Priority => 3;
		public override double Count(double[] nums) => Math.Tan(nums[0] * Math.PI / 180);
		public override bool Equals(Operations opp)
		{
			return opp is Tan;
		}
	}
	public class Ctg : Operations
	{
		public override string Name => "ctg";
		public override short CountNum => 1;
		public override int Priority => 3;
		public override double Count(double[] nums) => 1 / Math.Tan(nums[0] * Math.PI / 180);
		public override bool Equals(Operations opp)
		{
			return opp is Ctg;
		}
	}
	public class Ln : Operations
	{
		public override string Name => "ln";
		public override short CountNum => 1;
		public override int Priority => 3;
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
		public override int Priority => 3;
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
		public override int Priority => 3;
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
		public override int Priority => 4;
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
}
