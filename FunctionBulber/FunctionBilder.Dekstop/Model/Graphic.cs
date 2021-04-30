using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System.Collections.ObjectModel;

namespace FunctionBilder.Dekstop
{
	public class Graphic
	{
		public static readonly ObservableCollection<IBrush> Colors = new ObservableCollection<IBrush>() { Brushes.Aqua, Brushes.Purple, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue, Brushes.White, Brushes.YellowGreen };
		public readonly double Ratio = 0.4;
		public IBrush PointColor { get; } = Brushes.Red;
		public IBrush LineColor { get; } = Brushes.Yellow;
		public bool IsVisibleElipse { get; } = true;
		public IBrush[] GraphicColor() => new IBrush[] { this.PointColor, this.LineColor};
		public double[] gap { get; }
		public Graphic(IBrush[] brushes , bool isVisible,double[] _gap)
		{
			this.PointColor = brushes[0];
			this.LineColor = brushes[1];
			this.IsVisibleElipse = isVisible;
			this.gap = _gap;
		}
		public Graphic(bool isVisible, double[] _gap)
		{
			this.IsVisibleElipse = isVisible;
			this.gap = _gap;
		}
		public Graphic(double[] _gap)
		{
			this.gap = _gap;
		}
		public static bool CanConvertBoxes(TextBox[] textBoxes)
		{
			bool isDouble = default;
			foreach (var tBox in textBoxes)
			{
				isDouble = NaNError.CanConvertToDouble(tBox.Text);
			}
			return isDouble;
		}
	}
}
