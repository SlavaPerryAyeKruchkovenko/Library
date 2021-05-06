using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FunctionBilder.Dekstop.UserControls
{
	public class RangePanel : UserControl
	{
		public RangePanel()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
