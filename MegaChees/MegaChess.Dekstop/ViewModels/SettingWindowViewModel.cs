using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaChess.Dekstop.ViewModels
{
	class SettingWindowViewModel
	{
		public static Brush FirstColor { get; private set; } = (Brush)Brushes.Black;
		public static Brush SecondColor { get; private set; } = (Brush)Brushes.White;
	}
}
