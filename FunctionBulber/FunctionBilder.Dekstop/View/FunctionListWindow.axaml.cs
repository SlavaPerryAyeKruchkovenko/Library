using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static FunctionBulber.Logic.IDrawer;

namespace FunctionBilder.Dekstop.View
{
	public class FunctionListWindow : Window, INotifyPropertyChanged
	{
		public ObservableCollection<Function> Functions { get; }
		private readonly Instalize RenderGraphic;
		private short id = -1;
		public FunctionListWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.Functions = new ObservableCollection<Function>();
		}
		public FunctionListWindow(ObservableCollection<Function> _functions, Instalize func)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.Functions = _functions;
			var listBox = this.FindControl<ListBox>("Functions");
			listBox.DataContext = this;
			this.RenderGraphic = func;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void SelectFunction(object sender, SelectionChangedEventArgs e)
		{
			var item = (ListBox)sender;
			if(item.SelectedItem == null)
			{
				return;
			}
			else
			{
				this.id = (short)item.SelectedIndex;
			}
		}
		private void DeleteFunc(object sender , RoutedEventArgs e)
		{
			this.Functions.RemoveAt(this.id);
			this.RenderGraphic();
			if (this.Functions.Count < 1) 
			{
				this.Close();
			}
		}
	}
}
