using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FunctionBilder.Dekstop.UserControls
{
	public class MyDataGrid : UserControl
	{
		public MyDataGrid()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
