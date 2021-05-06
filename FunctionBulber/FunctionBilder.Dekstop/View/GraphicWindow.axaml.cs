using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using FunctionBilder.Dekstop.Model;
using FunctionBilder.Dekstop.ViewModel;
using FunctionBulber.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static FunctionBulber.Logic.IDrawer;

namespace FunctionBilder.Dekstop.View
{
	public class GraphicWindow : Window
	{
		private TextBox inputBox;

		private TextBox[] rangeBoxes;

		private IDrawer drawer;

		private IBrush lineColor = Brushes.Yellow;

		private IBrush pointColor = Brushes.Red;

		private MenuItem lineItem;

		private MenuItem pointItem;

		private Instalize RedrawFunction;

		private ObservableCollection<Function> functions;
#pragma warning disable CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		public GraphicWindow ()
#pragma warning restore CS8618 // ����, �� ����������� �������� NULL, ������ ��������� ��������, �������� �� NULL, ��� ������ �� ������������. ��������, ����� �������� ���� ��� ����������� �������� NULL.
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}
		public GraphicWindow(ObservableCollection<Function> _functions, Instalize func)
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
			this.RedrawFunction = func;
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
			if (menu.Name == "LineColorMenu") 
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
			this.drawer.Draw(this.RedrawFunction);
		}
		private void CreateFunction()
		{
			var checkBox = this.Find<UserControl>("PointCheckBox").Find<CheckBox>("IsNeedEllipse");
			var brushes = new IBrush[] { this.pointColor, this.lineColor };

			var graphic = new Graphic(brushes, checkBox.IsChecked.Value, this.rangeBoxes.ToDouble());
			var function = new Function(this.inputBox.Text, graphic);
			this.functions.Add(function);
			
			this.Close();
		}
	}
}
