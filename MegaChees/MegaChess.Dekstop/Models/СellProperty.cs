using System;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using MegaChess.Logic;

namespace MegaChess.Dekstop.Models
{
	public class СellProperty : BaseModel
	{
		private string _image;
		public string Image
		{
			get => _image;
			set => this.RaiseAndSetIfChanged(ref _image, value);
		}
		private Brush _color;
		public Brush Color
		{
			get => _color;
			set => this.RaiseAndSetIfChanged(ref _color, value);
		}
		private Figura _figura;
		public Figura FiguraNow
		{
			get => _figura;
			set => this.RaiseAndSetIfChanged(ref _figura, value);
		}
	}
}
