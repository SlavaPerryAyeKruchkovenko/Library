using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using System.Collections.Generic;

namespace FunctionBilder.Dekstop.View
{
	public class GraphicWindow : Window
	{
		private TextBox inputBox { get; }
		private TextBox[] rangeBoxes { get; }
		private Function function { get; set; }

		private static readonly List<IBrush> colors = new List<IBrush>() {Brushes.Aqua,Brushes.Purple,Brushes.Sienna,Brushes.Silver,Brushes.SkyBlue,Brushes.White,Brushes.YellowGreen };
		public GraphicWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.inputBox = this.FindControl<UserControl>("InputBox").FindControl<TextBox>("FunctuionBox");
			this.rangeBoxes = MainWindow.FoundTextBoxs(this);
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void AddFunction(object sender, RoutedEventArgs e)
		{
			var graphic = new Graphic(this.rangeBoxes.ToDouble());
			this.function = new Function(this.inputBox.Text, graphic);
			this.Close();
		}
	}
}
