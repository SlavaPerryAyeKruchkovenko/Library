using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MegaChess.Dekstop.Views
{
	public partial class GameBoard : UserControl
	{
		public GameBoard()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}
