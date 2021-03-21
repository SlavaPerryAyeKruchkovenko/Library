using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace FunctionBilder.Dekstop
{
	public class FunctionWindow : Window
	{
		Canvas canvas { get; }
		public FunctionWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.canvas = this.FindControl<Canvas>("BigFunctionCanvas");

		}
		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}	
		public void Canvas_SizeChanged(object sender1, AvaloniaPropertyChangedEventArgs e)
		{
			var window = new MainWindow();
			window.BtnCount_Click(sender1, new RoutedEventArgs());		
		}
	}
}
