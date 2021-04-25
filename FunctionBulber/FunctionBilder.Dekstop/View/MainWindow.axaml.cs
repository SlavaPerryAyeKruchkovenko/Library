using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;

namespace FunctionBilder.Dekstop.View
{
	public class MainWindow : Window
	{
		private IDrawer drawer { get; }
		private TextBox inputBox { get; }
		private TextBox nowBox { get; set; }
		private TextBox[] boxes { get; }
		private Rect size { get; set; }
		private Field field { get; set; }
		private Function function { get; set; }

		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.inputBox = this.FindControl<TextBox>("FunctuionBox");
			this.nowBox = inputBox;
			this.drawer = new Drawer(inputBox);
			this.boxes = FoundTextBoxs();
			this.size = Bounds;
			this.field = new Field(this.FindControl<Canvas>("FunctionCanvas"), this.FindControl<DataGrid>("OutputDataGrid"));
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
			if (this.inputBox != null && this.inputBox.Text != null && this.size != this.Bounds)
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
		private void CreateGraphic()
		{
			if (CheckOnErrors(this.boxes))
			{
				this.field.Input.Items = null;
				this.field.Canvas.Children.Clear();

				var scales = new short[] { 1, 1, 1 };
				this.field = new Field(this.field.Canvas, default, scales, false, this.field.Input);

				var graphic = new Graphic(false, this.boxes.ToDouble());
				this.function = new Function(this.inputBox.Text, graphic);
				this.function.Render(this.field);
			}
		}
	}
}
