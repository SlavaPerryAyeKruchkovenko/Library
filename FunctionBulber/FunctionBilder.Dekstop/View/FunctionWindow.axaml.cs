using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

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
		private Slider slider { get; }
		private Point lastCutrsorPosition { get; set; }
		private bool isPressed { get; set; } = false;
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
			this.slider = this.FindControl<Slider>("sliderScale");
			this.zoom = this.field.Scale;

			this.drawer = new Drawer(new object());
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			var window = new MainWindow(this.functions[0]);
			window.Show();
			this.OnClosed(e);
		}
		private void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.size != ((Canvas)sender1).Bounds)
			{			
				this.drawer.Draw(CreateGraphic);
			}
		}
		private void MousePress(object sender, PointerPressedEventArgs e)
		{
			this.isPressed = true;
			this.range -= e.GetCurrentPoint(this.field.Canvas).Position;
		}
		private void MouseUnpress(object sender, PointerReleasedEventArgs e)
		{
			if(this.isPressed)
			{
				this.range += e.GetCurrentPoint(this.field.Canvas).Position;
				this.drawer.Draw(CreateGraphic);
			}
			this.isPressed = false;
		}
		private void AddNewGraphic(object sender, RoutedEventArgs e)
		{
			var graphicWindow = new GraphicWindow();
			graphicWindow.Show();
		}
		private void DeleteAnyGraphic(object sender, RoutedEventArgs e)
		{
			throw new Exception("—авелий и тут Ћох");
		}
		private void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			ChangeScale((short)e.Delta.Y);
			this.drawer.Draw(CreateGraphic);
			this.slider.Value = this.zoom;
		}
		private void SliderZoom(object sender, PointerEventArgs e)
		{
			ChangeScale((short)(this.slider.Value - this.zoom));
			this.drawer.Draw(CreateGraphic);
		}
		private void BackToStart(object sender, RoutedEventArgs e)
		{
			this.range = default;
			this.drawer.Draw(CreateGraphic);
			this.isPressed = false;
		}
		private void ClickCheckBoxLabel(object sender, RoutedEventArgs e)
		{
			this.drawer.Draw(CreateGraphic);
		}
		public void ClickCheckBoxEllipse(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < this.functions.Count; i++) 
			{
				var graphic = new Graphic(this.functions[i].Graphic.IsVisibleElipse != true, this.functions[i].Graphic.gap);
				this.functions[i] = new Function(this.functions[i].FunctionText, graphic);
			}
			this.drawer.Draw(CreateGraphic);
		}
		private void FocusCoordinate(object sender, PointerEventArgs e)
		{								
			Point cursor = e.GetCurrentPoint(this.field.Canvas).Position;
			if (Math.Abs(this.lastCutrsorPosition.X - cursor.X) < 10 && Math.Abs(this.lastCutrsorPosition.Y - cursor.Y) < 10)
			{
				ToolTip.SetIsOpen(this.field.Canvas, true);
				return;
			}
			Point pointNow = (cursor - this.range - this.field.LayoutSize / 2) / this.zoom;
			var myToolTip = new MyToolTip();
			string content = "";

			foreach (var item in this.functions)
			{
				content += item.GetCoordinateInPoint(pointNow);
			}
			ToolTip.SetTip(this.field.Canvas, myToolTip.Create(pointNow, content, 16));
			ToolTip.SetIsOpen(this.field.Canvas, false);
			this.lastCutrsorPosition = cursor;
		}
		private static void InstalizeWindow(FunctionWindow window)
		{
			window.InitializeComponent();
#if DEBUG
			window.AttachDevTools();
#endif
			window.range = default;
			window.size = default;
		}
		private void CreateGraphic()
		{
			this.size = this.field.Canvas.Bounds;
			this.field.Canvas.Children.Clear();

			var scales = new short[] { this.field.AxisLineScale, this.zoom };
			this.field = new Field(this.field.Canvas, this.range, scales, this.labelVisible.IsChecked.Value, null);

			this.field.RenderField();
			foreach (var item in this.functions)
			{
				item.Render(this.field);
			}
		}
		private void ChangeScale(short newScale)
		{
			if (newScale < 0 && this.zoom > 5)
			{
				this.zoom -= 5;
				this.range -= this.range * 4.2 / this.zoom;
			}
			else if (newScale > 0 && this.zoom < 100 || newScale < 0 && this.zoom > 1) 
			{
				this.zoom += newScale;
				this.range += this.range * 1.12 / this.zoom;
			}
			else
			{
				return;
			}
		}
	}
}
