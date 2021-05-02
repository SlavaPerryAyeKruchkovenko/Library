using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System.Collections.Generic;


namespace FunctionBilder.Dekstop.View
{
	public class GraphicWindow : Window
	{
		private TextBox inputBox { get; }
		private TextBox[] rangeBoxes { get; }
		private IDrawer drawer { get; }
		private IBrush lineColor { get; set; } = Brushes.Yellow;
		private IBrush pointColor { get; set; } = Brushes.Red;
		private MenuItem lineItem { get; }
		private MenuItem pointItem { get; }
		private List<Function> functions { get; }
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		public GraphicWindow ()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}
		public GraphicWindow(List<Function> _functions)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.functions = _functions;
			this.lineItem = this.Find<MenuItem>("LineItem");
			this.pointItem = this.Find<MenuItem>("PointItem");
			this.FindControl<ContextMenu>("LineColorMenu").Items = Graphic.Colors;
			this.FindControl<ContextMenu>("PointColorMenu").Items = Graphic.Colors;
			this.inputBox = this.FindControl<UserControl>("InputBox").FindControl<TextBox>("FunctuionBox");
			this.rangeBoxes = MainWindow.FoundTextBoxs(this);
			this.drawer = new Drawer(this.inputBox);
		}	 
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void AddColor(object sender, SelectionChangedEventArgs e)
		{
			var menu = (ContextMenu)sender;
			if (menu.SelectedItem == null)
			{
				return;
			}				
			var color = (IBrush)menu.SelectedItem;
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
			this.drawer.Draw(CreateFunction);
		}
		private void CreateFunction()
		{
			var checkBox = this.Find<UserControl>("PointCheckBox").Find<CheckBox>("IsNeedEllipse");
			IBrush[] brushes = new IBrush[] { this.pointColor, this.lineColor };

			var graphic = new Graphic(brushes, checkBox.IsChecked.Value, this.rangeBoxes.ToDouble());
			var function = new Function(this.inputBox.Text, graphic);
			this.functions.Add(function);
			this.Close();
		}
	}
}
