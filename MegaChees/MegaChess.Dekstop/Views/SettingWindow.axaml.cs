using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MegaChess.Dekstop.ViewModels;

namespace MegaChess.Dekstop.Views
{
	public partial class SettingWindow : Window
	{
		public SettingWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.viewModel = new SettingWindowViewModel();
			this.FindControl<Button>("1BtnColor").ContextMenu.Items = this.viewModel.Images;
			this.FindControl<ContextMenu>("FirstColors").Items = this.viewModel.OntherColor;
			this.FindControl<ContextMenu>("SecondColors").Items = this.viewModel.OntherColor;
			this.FindControl<Button>("3BtnColor").ContextMenu.Items = this.viewModel.OntherColor;
		}

		readonly SettingWindowViewModel viewModel;
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		private void CloseWindow(object sender , RoutedEventArgs e)
		{
			this.Close();
		}
		private void ChangeColor(object sender, RoutedEventArgs e)
		{
			var container = this.FindControl<ItemsControl>("container");
			foreach (var item in container.Items)
			{
				if(item is Button btn)
				{
					btn.Foreground = Brushes.Black;
					if(btn.ContextMenu != null)
					{
						btn.ContextMenu.Close();
					}					
				}
			}
			var button = sender as Button;
			button.Foreground = new SolidColorBrush(Color.FromRgb(0xd4, 0x43, 0x3e));
			button.ContextMenu.Open();
		}
		private void SelectImages(object sender, SelectionChangedEventArgs e)
		{
			var menu = sender as ContextMenu;
			if (menu.SelectedItem == null)
			{
				return;
			}
			else
			{
				this.viewModel.ChangeFiguraImage(menu.SelectedItem as string);
			}
		}
		private void SelectColor(object sender, SelectionChangedEventArgs e)
		{
			var menu = sender as ContextMenu;
			if (menu.SelectedItem == null)
			{
				return;
			}
			var color = menu.SelectedItem as IBrush;
			if (menu.Name == "FirstColors")
			{
				this.viewModel.ChangeBoardColor(color, true);
			}
			if (menu.Name == "SecondColors")
			{
				this.viewModel.ChangeBoardColor(color, false);
			}
			else
			{
				this.viewModel.ChangeOtherColor(color);
			}
		}
	}
}
