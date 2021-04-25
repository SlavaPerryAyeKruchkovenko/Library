using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.View
{
	public class FunctionWindow : Window
	{
		private IDrawer drawer { get; }
		private Point range { get; set; }
		private short zoom { get; set; }
		private Field field { get; set; }
		private List<Function> functions { get; }
		private Rect size { get; set; }
		private CheckBox labelVisible { get; }
		private CheckBox ellipseVisible { get; }
#pragma warning disable CS8618 // ѕоле, не допускающее значени€ NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. ¬озможно, стоит объ€вить поле как допускающее значени€ NULL.
		public FunctionWindow()
#pragma warning restore CS8618 // ѕоле, не допускающее значени€ NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. ¬озможно, стоит объ€вить поле как допускающее значени€ NULL.
		{
			InstalizeWindow(this);
		}
		public FunctionWindow(Function _function)
		{
			this.functions = new List<Function>();
			this.functions.Add(_function);		

			InstalizeWindow(this);

			this.field = new Field(this.FindControl<Canvas>("BigFunctionCanvas"), null);
			this.labelVisible = this.FindControl<CheckBox>("IsNeedLabel");
			this.ellipseVisible = this.FindControl<CheckBox>("IsNeedEllipse");
			this.zoom = this.field.Scale;

			this.drawer = new Drawer(new object());							
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
			this.range -= e.GetCurrentPoint(this.field.Canvas).Position;
		}
		public void MouseUnpress(object sender, PointerReleasedEventArgs e)
		{
			this.range += e.GetCurrentPoint(this.field.Canvas).Position;
			this.drawer.Draw(CreateGraphic);
		}
		public void AddNewGraphick(object sender, RoutedEventArgs e)
		{
			throw new System.Exception("—авелий лох");
		}
		public void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			this.zoom += (short)e.Delta.Y;
			if (this.zoom >= 1)
			{
				this.drawer.Draw(CreateGraphic);
			}
		}
		public void ClickCheckBoxLabel(object sender, RoutedEventArgs e)
		{
			this.drawer.Draw(CreateGraphic);
		}
		public void ClickCheckBoxEllipse(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < this.functions.Count;i++)
			{
				var graphic = new Graphic(this.functions[i].Graphic.IsVisibleElipse == true ? false : true, this.functions[i].Graphic.gap);
				this.functions[i] = new Function(this.functions[i].FunctionText, graphic);
			}
			this.drawer.Draw(CreateGraphic);
		}
		static void InstalizeWindow(FunctionWindow window)
		{
			window.InitializeComponent();
#if DEBUG
			window.AttachDevTools();
#endif
			window.size = default;
			window.range = default;		
		}
		void CreateGraphic()
		{
			this.size = this.field.Canvas.Bounds;
			this.field.Canvas.Children.Clear();

			var scales = new short[] { this.field.AxisLineScale, this.zoom, this.zoom };
			this.field = new Field(this.field.Canvas, this.range, scales, this.labelVisible.IsChecked.Value, null);
			foreach (var item in this.functions)
			{
				item.Render(this.field);
			}
		}
	}
}
