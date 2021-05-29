using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.ViewModels;
using MegaChess.Logic;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reactive.Subjects;

namespace MegaChess.Dekstop.Converter
{
	class Drawer : IDrawer
	{
		public Drawer(ObservableCollection<Border> _borders , Figura _figura)
		{
			this.borders = _borders;
			this.figura = _figura;
		}
		private ObservableCollection<Border> borders;
		private Figura figura;
		public void Clear()
		{
			
		}

		public Point ConvertToLocationFormat(char i, char j)
		{
			return new Point(1, 1);
		}

		public char[] ConvertToTKeyFormat(int x, int y)
		{
			var key1 = y switch
			{
				0 => '1',
				1 => '2',
				2 => '3',
				3 => '4',
				4 => '5',
				5 => '6',
				6 => '7',
				7 => '8',
				_ => ' ',
			};
			var key2 = x switch
			{
				0 => 'A',
				1 => 'B',
				2 => 'C',
				3 => 'D',
				4 => 'E',
				5 => 'F',
				6 => 'G',
				7 => 'H',
				_ => ' ',
			};
			return new char[] { key1, key2 };
		}

		public void CursorVisible(bool visible)
		{
			
		}

		public Figura MoveCursor(int x, int y, Board board)
		{
			Figura figura = this.figura;
			while(figura == this.figura)
			{
				
			}
			return this.figura;
		}		
		public void PrintBoard(Board board)
		{
			
				int count = 0;
			foreach (var item in board.GetFiguras())
			{
				
				var figuraProperty = new СellProperty ();

				if (item.IsMyFigura == true)
				{
					figuraProperty.Image = item.ToString().ToUpper() switch
					{
						"P" => "Megachess-logo.png",
						"R" => "Megachess-logo.png",
						"H" => "Megachess-logo.png",
						"B" => "Megachess-logo.png",
						"Q" => "Megachess-logo.png",
						"K" => "Megachess-logo.png",
						_ => "",
					};
				}
				else if (item.IsMyFigura == false)
				{
					figuraProperty.Image = item.ToString().ToUpper() switch
					{
						"P" => "Megachess-logo.png",
						"R" => "Megachess-logo.png",
						"H" => "Megachess-logo.png",
						"B" => "Megachess-logo.png",
						"Q" => "Megachess-logo.png",
						"K" => "Megachess-logo.png",
						_ => "",
					};
				}				
				figuraProperty.Color = count%2==0 ? Avalonia.Media.Brushes.White : Avalonia.Media.Brushes.Black;
				figuraProperty.FiguraNow = item;

				Action action = () =>
				{
					if (count<64)
				this.borders[count] = ChangeBorderProperty(this.borders[count], figuraProperty);
				};
				Dispatcher.UIThread.InvokeAsync(action);
				count++;
			}
			
		}
		private Border ChangeBorderProperty(Border border , СellProperty property)
		{
			border.Background = property.Color;
			border.BorderBrush = new ImageBrush()
			{
				Source = (Avalonia.Media.Imaging.Bitmap)new ImageConverter().Convert(property.Image, null, null, null)
			};
			border.DataContext = property.FiguraNow;
			return border;
		}
		public void PrintError(string ex)
		{
			Action action = () =>
			{
				var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
				.GetMessageBoxStandardWindow(new MessageBoxStandardParams
				{
					ButtonDefinitions = ButtonEnum.OkAbort,
					ContentMessage = ex,
					Icon = MessageBox.Avalonia.Enums.Icon.Plus,
					Style = Style.UbuntuLinux
				});
				msBoxStandardWindow.Show();
			};
			Dispatcher.UIThread.InvokeAsync(action);			
		}
	}
}
