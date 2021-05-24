using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class DeadFiguresBoard : UserControl
	{
		public DeadFiguresBoard()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
