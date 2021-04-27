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
		private short fontSize { get; set; }
		private CheckBox labelVisible { get; }
		private Slider slider { get; }
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
			this.fontSize = this.field.FontSize;

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
			this.isPressed = true;
			this.range -= e.GetCurrentPoint(this.field.Canvas).Position;
		}
		public void MouseUnpress(object sender, PointerReleasedEventArgs e)
		{
			if(this.isPressed)
			{
				this.range += e.GetCurrentPoint(this.field.Canvas).Position;
				this.drawer.Draw(CreateGraphic);
			}
			this.isPressed = false;
		}
		public void AddNewGraphic(object sender, RoutedEventArgs e)
		{
			throw new Exception("—авелий лох");
		}
		public void DeleteAnyGraphic(object sender, RoutedEventArgs e)
		{
			throw new Exception("—авелий и тут Ћох");
		}
		public void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			ChangeScale((short)e.Delta.Y);
			this.drawer.Draw(CreateGraphic);
			this.slider.Value = this.zoom;
		}
		public void SliderZoom(object sender, PointerEventArgs e)
		{
			ChangeScale((short)(this.slider.Value - this.zoom));
			this.drawer.Draw(CreateGraphic);
		}
		public void ClickCheckBoxLabel(object sender, RoutedEventArgs e)
		{
			this.drawer.Draw(CreateGraphic);
		}
		public void ClickCheckBoxEllipse(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < this.functions.Count;i++)
			{
				var graphic = new Graphic(this.functions[i].Graphic.IsVisibleElipse != true, this.functions[i].Graphic.gap);
				this.functions[i] = new Function(this.functions[i].FunctionText, graphic);
			}
			this.drawer.Draw(CreateGraphic);
		}
		public void FocusCoordinate(object sender, PointerEventArgs e)
		{
			var myToolTip = new MyToolTip();
			string content = "";
			double x = (this.field.LayoutSize.X + this.range.X)/this.zoom;
			Point cursor = e.GetCurrentPoint(this.field.Canvas).Position;

			foreach (var item in this.functions)
			{
				var RPN = new ReversePolandLogic(item.FunctionText);
				RPN.StackInitialization();
				Point point = ModelNumerable.YCoordinate(RPN, new double[] { x });
				content += item.FunctionText;

				if (Math.Abs(cursor.Y - point.Y) < this.zoom)
				{
					content += " " + point.Y.ToString() + "\n";
				}
				else
				{
					content += " Ќе имеет значени€ в данной точке" + "\n";
				}			
			}

			ToolTip.SetTip(this.field.Canvas,myToolTip.Create(cursor, content, this.fontSize));
			ToolTip.SetIsOpen(this.field.Canvas, true);
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
		private void CreateGraphic()
		{
			this.size = this.field.Canvas.Bounds;
			this.field.Canvas.Children.Clear();

			var scales = new short[] { this.field.AxisLineScale, this.zoom, this.fontSize };
			this.field = new Field(this.field.Canvas, this.range, scales, this.labelVisible.IsChecked.Value, null);
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
				this.fontSize -= 5;
				this.range -= this.range * 5 / this.zoom;
			}
			else if (newScale > 0 && this.zoom < 100 || newScale < 0 && this.zoom > 1) 
			{
				this.zoom += newScale;
				this.fontSize += newScale;
				this.range += this.range / this.zoom;
			}
			else
			{
				return;
			}
		}
	}
}
