using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace FunctionBilder.Dekstop
{
	public class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
		public void BtnCalculate_Click(object sender, RoutedEventArgs e)
		{
			var tbX = this.FindControl<TextBox>("OutputBox");
			tbX.Text += " Hello avalonia";
		}
	}
}
