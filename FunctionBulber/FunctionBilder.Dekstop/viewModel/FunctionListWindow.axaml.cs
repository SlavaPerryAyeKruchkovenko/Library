using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FunctionBilder.Dekstop.Model;
using System.Collections.Generic;
using System.Linq;
using static FunctionBulber.Logic.IDrawer;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class FunctionListWindow : Window
	{
		public List<Function> Functions { get; }
		private readonly Instalize RenderGraphic;
		private short id = -1;
		public FunctionListWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.Functions = new List<Function>();
		}
		public FunctionListWindow(List<Function> _functions, Instalize func)
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.Functions = _functions;
			var listBox = this.FindControl<ListBox>("Functions");
			listBox.DataContext = this;
			listBox.Items = this.Functions.Select(x => x.FunctionText);
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
		}
	}
}
