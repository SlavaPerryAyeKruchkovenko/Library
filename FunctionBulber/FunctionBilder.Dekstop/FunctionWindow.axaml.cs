using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.viewModel;
using FunctionBulber.Logic;

namespace FunctionBilder.Dekstop
{
	public class FunctionWindow : Window
	{
		Canvas canvas { get; }
		string function { get; }
		IDrawer drawer { get; }
		double[] location { get; }
#pragma warning disable CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		public FunctionWindow()
#pragma warning restore CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}
		public FunctionWindow(string _function,IDrawer _drawer,double[] _location)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.canvas = this.FindControl<Canvas>("BigFunctionCanvas");
			this.function = _function;
			this.drawer = _drawer;
			this.location = _location;
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}	
		public void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.canvas != null)
			{
				this.canvas.Children.Clear();
				this.canvas.GraphicRender(this.function, this.drawer, null, this.location);
			}				
		}
	}
}
