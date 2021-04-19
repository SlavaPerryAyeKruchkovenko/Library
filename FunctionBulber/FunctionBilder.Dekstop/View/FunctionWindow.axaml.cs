using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System.Collections.ObjectModel;

namespace FunctionBilder.Dekstop.View
{
	public class FunctionWindow : Window
	{
		private Canvas canvas { get; }
		private ObservableCollection<Point> �oordinates { get; set; }
		private string function { get; }
		private IDrawer drawer { get; }
		private Point range { get; set; }
		private double zoom { get; set; }
		private double[] restriction { get; }
		private Rect size { get; set; }
#pragma warning disable CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		public FunctionWindow()
#pragma warning restore CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		{
			InstalizeWindow(this);
		}
		public FunctionWindow(string _function, double[] _restriction)
		{
			InstalizeWindow(this);

			this.drawer = new Drawer(new object());
			this.canvas = this.FindControl<Canvas>("BigFunctionCanvas");
			this.function = _function;
			this.restriction = _restriction;
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		public void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.size != this.Bounds)
			{
				this.CreateGraphic();
			}
		}
		public void MousePress(object sender, PointerPressedEventArgs e)
		{
			this.�oordinates.Add(e.GetCurrentPoint(this.canvas).Position);
			this.range -= e.GetCurrentPoint(this.canvas).Position;
		}
		public void MouseUnpress(object sender, PointerReleasedEventArgs e)
		{
			this.range += e.GetCurrentPoint(this.canvas).Position;
			CreateGraphic();
		}
		public void AddNewGraphick(object sender, RoutedEventArgs e)
		{
			throw new System.Exception("������� ���");
		}
		public void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			this.zoom += e.Delta.Y;
			if (this.zoom > 0)
			{
				CreateGraphic();
			}
		}
		static void InstalizeWindow(FunctionWindow window)
		{
			window.InitializeComponent();
#if DEBUG
			window.AttachDevTools();
#endif
			window.�oordinates = new ObservableCollection<Point>();
			window.size = window.Bounds;
			window.range = default;
			window.zoom = Field.BeautifulScale;
		}
		void CreateGraphic()
		{
			this.size = Bounds;
			this.canvas.Children.Clear();
			this.canvas.GraphicRender(this.function, this.restriction, this.range, this.zoom, Field.StandartGraphicColor());
		}
	}
}
