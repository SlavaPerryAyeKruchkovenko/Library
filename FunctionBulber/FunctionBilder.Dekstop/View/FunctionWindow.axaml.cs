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
		private ObservableCollection<Point> Сoordinates { get; set; }
		private string function { get; }
		private IDrawer drawer { get; }
		private Point range { get; set; }
		private double zoom { get; set; }
		private Field field { get; }
		private Rect size { get; set; }
		private double[] gap { get; }
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		public FunctionWindow()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
			InstalizeWindow(this);
		}
		public FunctionWindow(string _function, Field _field,double[] _gap)
		{
			this.field = _field;

			InstalizeWindow(this);

			this.gap = _gap;
			this.canvas = this.FindControl<Canvas>("BigFunctionCanvas");
			this.drawer = new Drawer(new object());			
			this.function = _function;					
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		public void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.size != ((Canvas)sender1).Bounds)
			{
				this.drawer.Draw(CreateGraphic);
			}
		}
		public void MousePress(object sender, PointerPressedEventArgs e)
		{
			this.Сoordinates.Add(e.GetCurrentPoint(this.canvas).Position);
			this.range -= e.GetCurrentPoint(this.canvas).Position;
		}
		public void MouseUnpress(object sender, PointerReleasedEventArgs e)
		{
			this.range += e.GetCurrentPoint(this.canvas).Position;
			this.drawer.Draw(CreateGraphic);
		}
		public void AddNewGraphick(object sender, RoutedEventArgs e)
		{
			throw new System.Exception("Савелий лох");
		}
		public void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			this.zoom += e.Delta.Y;
			if (this.zoom >= 1)
			{
				this.drawer.Draw(CreateGraphic);
			}
		}
		static void InstalizeWindow(FunctionWindow window)
		{
			window.InitializeComponent();
#if DEBUG
			window.AttachDevTools();
#endif
			window.Сoordinates = new ObservableCollection<Point>();
			window.size = default;
			window.range = default;
			window.zoom = window.field.Scale;
		}
		void CreateGraphic()
		{
			this.size = this.canvas.Bounds;
			this.canvas.Children.Clear();

			Point size = new Point(this.size.Size.Width, this.size.Size.Height);
			var myField = new MyField(this.field, this.range, size);
			this.canvas.GraphicRender(this.function, this.gap, myField, this.zoom);
		}
	}
}
