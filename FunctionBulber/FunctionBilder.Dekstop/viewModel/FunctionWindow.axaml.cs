using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using FunctionBilder.Dekstop.Model;
using FunctionBulber.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class FunctionWindow : Window
	{
		private IDrawer drawer;

		private Point range;

		private short zoom;

		private Field field;

		private List<Function> functions;

		private Rect size;
		private CheckBox LabelVisible { get; }
		private Slider Slider { get; }

		private Point lastCutrsorPosition;

		private bool isPressed = false;

		private Window window;

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		public FunctionWindow()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
			InstalizeWindow(this);			
		}
		public FunctionWindow(Function _function)
		{
			InstalizeWindow(this);

			this.functions.Add(_function);
		
			this.LabelVisible = this.FindControl<CheckBox>("IsNeedLabel");
			this.Slider = this.FindControl<Slider>("SliderScale");			

			this.window = new GraphicWindow(this.functions, this.CreateGraphic);

			var label = this.FindControl<Label>("ScaleLabel");
			label.DataContext = this;					
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			Window window1;
			if(this.functions.Count>0)
			{
				window1 = new MainWindow(this.functions[0]);
			}
			else
			{
				window1 = new MainWindow();
			}
			window1.Show();
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
			if (!this.window.IsVisible) 
			{
				this.window = new GraphicWindow(this.functions, this.CreateGraphic);
				this.window.Show();
				this.window.Topmost = true;
			}
		}
		private void DeleteAnyGraphic(object sender, RoutedEventArgs e)
		{
			if (!this.window.IsVisible)
			{
				this.window = new FunctionListWindow(this.functions, this.CreateGraphic);
				this.window.Show();
				this.window.Topmost = true;
			}
		}
		private void ZoomGraphick(object sender, PointerWheelEventArgs e)
		{
			ChangeScale((short)e.Delta.Y);
			this.drawer.Draw(CreateGraphic);
			this.Slider.Value = this.zoom;
		}
		private void SliderZoom(object sender, PointerEventArgs e)
		{
			ChangeScale((short)(this.Slider.Value - this.zoom));
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
				var graphic = new Graphic(this.functions[i].Graphic.GraphicColor(),this.functions[i].Graphic.IsVisibleElipse != true, this.functions[i].Graphic.gap);
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
			window.functions = new List<Function>();
			window.range = default;
			window.size = default;
			window.drawer = new Drawer(new object());
			window.field = new Field(window.FindControl<Canvas>("BigFunctionCanvas"), null);
			window.zoom = window.field.Scale;
		}
		private void CreateGraphic()
		{
			this.size = this.field.Canvas.Bounds;
			this.field.ClearCanvas();

			var scales = new short[] { this.field.AxisLineScale, this.zoom };
			this.field = new Field(this.field.Canvas, this.range, scales, this.LabelVisible.IsChecked.Value, null);

			this.field.RenderField();
			foreach (var item in this.functions)
			{				
				new Thread(()=>
				{
					Action action = () =>
					{
						item.Render(this.field);
					};
					Dispatcher.UIThread.InvokeAsync(action);
				}).Start();
			}
		}
		private void ChangeScale(short newScale)
		{
			var startCenter = this.field.BeginOfCountdown/this.zoom;
			if (newScale < 0 && this.zoom > 5) 
			{
				this.zoom -= 5;
			}
			else if (newScale > 0 && this.zoom < 100 || newScale < 0 && this.zoom > 1) 
			{
				this.zoom += newScale;	
			}		
			else
			{
				return;
			}
			var finishCenter = this.field.BeginOfCountdown / this.zoom;
			this.range -= (finishCenter - startCenter) * this.zoom;
		}
	}
}
