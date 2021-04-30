using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FunctionBilder.Dekstop.View
{
	public class GraphicWindow : Window
	{
		private TextBox inputBox { get; }
		private TextBox[] rangeBoxes { get; }
		private Function function { get; set; }
		private IBrush color { get; set; }
		public ObservableCollection<IBrush> colors { get; } 
		public GraphicWindow()
		{
			this.colors = new ObservableCollection<IBrush>() { Brushes.Aqua, Brushes.Purple, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue, Brushes.White, Brushes.YellowGreen };
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			var menu = this.FindControl<ContextMenu>("ColorMenu");
			menu.Items = this.colors;
			this.inputBox = this.FindControl<UserControl>("InputBox").FindControl<TextBox>("FunctuionBox");
			this.rangeBoxes = MainWindow.FoundTextBoxs(this);
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void AddColor(object sender, RoutedEventArgs e)
		{
			this.color = (IBrush)((MenuItem)sender);
		}
		private void OpenMenu(object sender, RoutedEventArgs e)
		{
			((MenuItem)sender).ContextMenu.Open();
		}
		private void AddFunction(object sender, RoutedEventArgs e)
		{			
			var graphic = new Graphic(this.rangeBoxes.ToDouble());
			this.function = new Function(this.inputBox.Text, graphic);
			this.Close();
		}
	}
}
