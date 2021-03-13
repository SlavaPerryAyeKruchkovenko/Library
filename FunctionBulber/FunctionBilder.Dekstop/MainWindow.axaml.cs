using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBulber.Logic;
using System;
using System.ComponentModel;

namespace FunctionBilder.Dekstop
{
	public class MainWindow : Window
	{
		IDrawer drawer { get; }
		IFunctionDrawer functionDrawer { get; }
		Canvas drawCanvas { get; }
		DataGrid outputBox { get; }
		TextBox inputBox { get; }
		TextBox nowBox { get; }
		Error error { get; set; }

		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
			this.outputBox = this.FindControl<DataGrid>("OutputDataGrid");
			this.inputBox = this.FindControl<TextBox>("FunctuionBox");
			this.nowBox = this.inputBox;
			this.drawer = new Drawer(this.inputBox);
			this.drawCanvas = this.FindControl<Canvas>("FunctionCanvas");
			this.functionDrawer = new FunctionDrawer(this.outputBox, this.drawCanvas, this.drawer);
			this.error = new Error(this.drawer,false, null);
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		public void BtnCalculate_Click(object sender, RoutedEventArgs e)
		{
			Button but = (Button)sender;
			this.nowBox.Text+=but.Content;
		}
		public void BtnClear_Click(object sender, RoutedEventArgs e)=> 
			this.nowBox.Clear();
		public void BtnPrefex_Click(object sender, RoutedEventArgs e) => 
			this.inputBox.Text = $"-({this.inputBox.Text})";
		public void Canvas_SizeChanged(object sender1,AvaloniaPropertyChangedEventArgs e)
		{
			if (this.inputBox != null && this.inputBox.Text != null) 
			BtnCount_Click(sender1, null);
		}
		public void BtnCount_Click(object sender, RoutedEventArgs e)
		{
			//this.outputBox.Clear();
			this.drawCanvas.Children.Clear();
			Point canvasSize= new Point(this.drawCanvas.Bounds.Width, this.drawCanvas.Bounds.Height);

			ReversePolandLogic reversePoland = new ReversePolandLogic(this.inputBox.Text, drawer);
			reversePoland.StacKInstalization();
			TextBox[] boxes = FoundTextBoxs();
			if (CheckOnErrors(boxes))
				return;

			this.functionDrawer.DrawLine(new Point(0, canvasSize.Y / 2),
				new Point(canvasSize.X, canvasSize.Y / 2), Brushes.DeepPink);

			this.functionDrawer.DrawLine(new Point(canvasSize.X / 2, 0),
				new Point(canvasSize.X / 2, canvasSize.Y),Brushes.DeepPink);

			this.functionDrawer.DrawArrows(new Point(canvasSize.X, canvasSize.Y / 2),
				new Point(10,10));

			this.functionDrawer.DrawArrows(new Point(canvasSize.X/2, 0),
				new Point(10,-10));

			this.functionDrawer.DrawFunction(Convert.ToDouble(boxes[0].Text), Convert.ToDouble(boxes[1].Text),
				Convert.ToDouble(boxes[2].Text),reversePoland.GetStack());
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
			foreach (var tBox in textBoxes)
			{
				this.error = new Error(this.drawer,
					!this.error.CanConvertToDouble(tBox.Text,out string errorType), errorType);
				if (this.error.HaveError)
					return true;
			}
			return false;
		}
	}
	public class Drawer : IDrawer
	{
		private TextBox tbx { get; }
		public Drawer(TextBox _tbx)
		{
			tbx = _tbx;
		}
		public void Draw(string input)
		{
			this.tbx.Clear();
			this.tbx.Text += input;
			
		}

	}
}
