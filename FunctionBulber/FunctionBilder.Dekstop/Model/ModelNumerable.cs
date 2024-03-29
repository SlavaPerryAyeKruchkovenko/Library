using System.Collections;
using System.Collections.Generic;
using Avalonia;
using FunctionBulber.Logic;

namespace FunctionBilder.Dekstop.Model
{
	static class ModelNumerable
	{
		public static Point YCoordinate(ReversePolandLogic RPN, double[] means)
		{
			var calculate = new Calculate();
			var pointsEnumerable = new CoordinateEnumerable(RPN.GetStack(), means, calculate).GetEnumerator();
			pointsEnumerable.MoveNext();
			return pointsEnumerable.Current;
		}
	}
	public class CoordinateEnumerable : IEnumerable<Point>
	{
		public CoordinateEnumerable(Stack<Element> _elements, double[] _coordinateValue, Calculate _calculate)
		{
			this.calculate = _calculate;
			this.elements = _elements;
			this.coordinateValue = _coordinateValue;
		}
		Calculate calculate;
		Stack<Element> elements;
		double[] coordinateValue;
		public IEnumerator<Point> GetEnumerator()
		{
			double x = coordinateValue[0];
			double y = this.calculate.CountRPN(this.coordinateValue, this.elements.Clone<Element>());
			yield return new Point(x, y);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

}
