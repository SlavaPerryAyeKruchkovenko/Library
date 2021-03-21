using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.viewModel;
using FunctionBulber.Logic;
using System;
using System.ComponentModel;

namespace FunctionBilder.Dekstop
{
	public class MainWindow : Window
	{
		IDrawer drawer { get; }
		Canvas drawCanvas { get; set; }
		DataGrid outputBox { get; }
		TextBox inputBox { get; }
		TextBox nowBox { get; set; }
		Error error { get; set; }
		TextBox[] boxes { get; }

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
			this.error = new Error(this.drawer,false, null);
			this.boxes = FoundTextBoxs();
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
		public void HighlightTextBox_Click(object sender, RoutedEventArgs e)
		{
			this.nowBox = (TextBox)sender;
		}
		public void Canvas_SizeChanged(object sender1,AvaloniaPropertyChangedEventArgs e)
		{
			if (this.inputBox != null && this.inputBox.Text != null) 
			BtnCount_Click(sender1, new RoutedEventArgs());
		}
		public void BtnCount_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Canvas)
				this.drawCanvas = (Canvas)sender;

			this.outputBox.Items = null;
			this.drawCanvas.Children.Clear();

			if (CheckOnErrors(this.boxes))
				return;
			this.drawCanvas.GraphicRender(this.inputBox.Text,this.drawer, this.outputBox, this.boxes.ToDouble());
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
		public void Canvas_Tap(object sender, RoutedEventArgs e)
		{
			var window = new FunctionWindow(this.inputBox.Text,this.drawer,this.boxes.ToDouble());
			window.Show();
		}
	}
	
}
