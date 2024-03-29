﻿using System;
using ReactiveUI;
using Avalonia.Media;
using MegaChess.Logic;


namespace MegaChess.Dekstop.Models
{
	public class СellProperty : BaseModel
	{
		private string _image;
		public string Image
		{
			get => this._image;
			set => this.RaiseAndSetIfChanged(ref _image, value);
		}
		private IBrush _color;
		public IBrush Color
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
