using Avalonia;
using Avalonia.Controls;
<<<<<<< HEAD
=======
using Avalonia.Input;
>>>>>>> FunctionBuilder
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;

namespace FunctionBilder.Dekstop.View
{
	public class MainWindow : Window
	{
<<<<<<< HEAD
		private IDrawer drawer { get; }
		private Canvas drawCanvas { get; set; }
		private DataGrid outputBox { get; }
		private TextBox inputBox { get; }
		private TextBox nowBox { get; set; }
		private TextBox[] boxes { get; }
		private Rect size { get; set; }
=======
		private IDrawer drawer;

		private TextBox inputBox;

		private TextBox nowBox;

		private TextBox[] boxes;

		private Rect size;

		private Field field;

		private Function function;
>>>>>>> FunctionBuilder

		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
<<<<<<< HEAD
			this.outputBox = this.FindControl<DataGrid>("OutputDataGrid");
			this.inputBox = this.FindControl<TextBox>("FunctuionBox");
			this.drawCanvas = this.FindControl<Canvas>("FunctionCanvas");
			this.nowBox = inputBox;
			this.drawer = new Drawer(inputBox);
			this.boxes = FoundTextBoxs();
			this.size = Bounds;
=======
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
>>>>>>> FunctionBuilder
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
<<<<<<< HEAD
			if (this.inputBox != null && this.inputBox.Text != null && this.size != this.Bounds)
			{
				drawer.Draw(CreateGraphic);
=======
			if (this.inputBox != null && this.size != ((Canvas)sender1).Bounds)
			{
				this.drawer.Draw(CreateGraphic);
>>>>>>> FunctionBuilder
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
<<<<<<< HEAD
			var window = new FunctionWindow(this.inputBox.Text, this.boxes.ToDouble());
			window.Show();
		}
		private void CreateGraphic()
		{
			if (CheckOnErrors(boxes))
			{
				this.outputBox.Items = null;
				this.drawCanvas.Children.Clear();

				this.outputBox.Items = this.drawCanvas.GraphicRender(this.inputBox.Text, this.boxes.ToDouble(), default,
					Field.StandartScale, Field.StandartGraphicColor());
			}
		}
		private TextBox[] FoundTextBoxs()
		{
			var startBox = this.FindControl<TextBox>("StartNum");
			var finishBox = this.FindControl<TextBox>("FinishNum");
			var rangeBox = this.FindControl<TextBox>("RangeNum");
			return new TextBox[] { startBox, finishBox, rangeBox };
		}
		private bool CheckOnErrors(TextBox[] textBoxes)
		{
			bool isDouble = default;
			foreach (var tBox in textBoxes)
			{
				isDouble = NaNError.CanConvertToDouble(tBox.Text);
			}
			return isDouble;
		}

=======
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
				this.field.ClearCanvas();

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
			window.field = new Field(window.FindControl<Canvas>("FunctionCanvas"), window.FindControl<UserControl>("Table").FindControl<DataGrid>("OutputDataGrid"));
		}
>>>>>>> FunctionBuilder
	}
}
