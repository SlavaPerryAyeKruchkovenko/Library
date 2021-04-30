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
		private IBrush lineColor { get; set; } = Brushes.Yellow;
		private IBrush pointColor { get; set; } = Brushes.Red;
		private MenuItem lineItem { get; }
		private MenuItem pointItem { get; }		
		public GraphicWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.lineItem = this.Find<MenuItem>("LineItem");
			this.pointItem = this.Find<MenuItem>("PointItem");
			this.FindControl<ContextMenu>("LineColorMenu").Items = Graphic.Colors;
			this.FindControl<ContextMenu>("PointColorMenu").Items = Graphic.Colors;
			this.inputBox = this.FindControl<UserControl>("InputBox").FindControl<TextBox>("FunctuionBox");
			this.rangeBoxes = MainWindow.FoundTextBoxs(this);
			
		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void AddColor(object sender, RoutedEventArgs e)
		{
			var menu = (ContextMenu)sender;
			var color = menu.SelectedItem == null ? Brushes.Aqua : (IBrush)menu.SelectedItem;
			if (menu.Name== "LineColorMenu")
			{				
				this.lineItem.Header = color;
				this.lineColor = color;
			}			
			else
			{
				this.pointItem.Header = color;
				this.pointColor = color;
			}
		}
		private void OpenMenu(object sender, RoutedEventArgs e)
		{
			((MenuItem)sender).ContextMenu.Open();
		}
		private void AddFunction(object sender, RoutedEventArgs e)
		{
			var checkBox = this.Find<UserControl>("PointCheckBox").Find<CheckBox>("IsNeedEllipse");
			IBrush[] brushes = new IBrush[] { this.pointColor, this.lineColor };

			var graphic = new Graphic(brushes, checkBox.IsChecked.Value, this.rangeBoxes.ToDouble());
			var function = new Function(this.inputBox.Text, graphic);
			this.Close();
		}
	}
}
