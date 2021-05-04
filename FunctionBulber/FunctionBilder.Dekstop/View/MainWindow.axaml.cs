using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;

namespace FunctionBilder.Dekstop.View
{
	public class MainWindow : Window
	{
		private IDrawer drawer;

		private TextBox inputBox;

		private TextBox nowBox;

		private TextBox[] boxes;

		private Rect size;

		private Field field;

		private Function function;

		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			Initialize(this);
		}
		public MainWindow(Function _function)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			Initialize(this);

			this.function = _function;
			this.inputBox.Text = this.function.FunctionText;
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		public void BtnCalculate_Click(object sender, RoutedEventArgs e)
		{
			Button but = (Button)sender;
			this.nowBox.Text += but.Content;
		}
		public void BtnClear_Click(object sender, RoutedEventArgs e) => this.nowBox.Clear();
		public void BtnPrefex_Click(object sender, RoutedEventArgs e) => this.inputBox.Text = $"-({this.inputBox.Text})";
		public void HighlightTextBox_Click(object sender, RoutedEventArgs e)
		{
			this.nowBox = (TextBox)sender;
		}
		public void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			if (this.inputBox != null && this.size != this.Bounds)
			{
				this.drawer.Draw(CreateGraphic);
			}
			this.size = this.Bounds;
		}
		public void BtnCount_Click(object sender, RoutedEventArgs e)
		{
			this.size = this.Bounds;
			this.drawer.Draw(CreateGraphic);
		}
		public void Canvas_Tap(object sender, RoutedEventArgs e)
		{
			var window = new FunctionWindow(this.function);
			window.Show();
			this.Close();
		}
		public void PressEnter(object sender, KeyEventArgs e)
		{
			if (e.Key.Equals(Key.Enter))
				this.drawer.Draw(CreateGraphic);
		}
		public static TextBox[] FoundTextBoxs(Window window)
		{
			var startBox = window.FindControl<UserControl>("GapBoxs").FindControl<TextBox>("StartNum");
			var finishBox = window.FindControl<UserControl>("GapBoxs").FindControl<TextBox>("FinishNum");
			var rangeBox = window.FindControl<UserControl>("GapBoxs").FindControl<TextBox>("RangeNum");
			if (rangeBox.Text.Contains("."))
			{
				rangeBox.Text = rangeBox.Text.Replace('.', ',');
			}
			return new TextBox[] { startBox, finishBox, rangeBox };
		}
		
		private void CreateGraphic()
		{			
			if (Graphic.CanConvertBoxes(this.boxes))
			{
				this.field.Input.Items = null;
				this.field.Canvas.Children.Clear();

				var scales = new short[] { 1, 1, 1 };
				this.field = new Field(this.field.Canvas, default, scales, true, this.field.Input);
				this.field.RenderField();

				var graphic = new Graphic(false, this.boxes.ToDouble());
				this.function = new Function(this.inputBox.Text, graphic);			
				this.function.Render(this.field);
			}
		}
		private static void Initialize(MainWindow window)
		{
			window.inputBox = window.FindControl<UserControl>("InputBox").FindControl<TextBox>("FunctuionBox");
			window.nowBox = window.inputBox;
			window.drawer = new Drawer(window.inputBox);
			window.boxes = FoundTextBoxs(window);
			window.size = window.Bounds;
			window.field = new Field(window.FindControl<Canvas>("FunctionCanvas"), window.FindControl<DataGrid>("OutputDataGrid"));
		}
	}
}
