using Avalonia;
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
using ReactiveUI;

namespace MegaChess.Dekstop.Converter
{
	class Drawer : BaseModel, IDrawer
	{
		public Drawer(ObservableCollection<Border> _borders , Figura _figura)
		{
			this.borders = _borders;
			this.figura1 = _figura;
		}
		private ObservableCollection<Border> borders;
		private Figura figura1;
		public Figura FiguraNow
		{
			get => figura1;
			set => this.RaiseAndSetIfChanged(ref figura1, value);
		}
		public void Clear()
		{
			this.borders.Clear();
		}

		public System.Drawing.Point ConvertToLocationFormat(char i, char j)
		{
			return new System.Drawing.Point(1, 1);
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
			var figura = this.figura1;
			while(this.FiguraNow == figura)
			{

			}
			return this.FiguraNow;

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
						"P" => "White_Pawn.png",
						"R" => "White_Rook.png",
						"H" => "White_Knight.png",
						"B" => "White_Elefant.png",
						"Q" => "White_Queen.png",
						"K" => "White_King.png",
						_ => "",
					};
				}
				else if (item.IsMyFigura == false)
				{
					figuraProperty.Image = item.ToString().ToUpper() switch
					{
						"P" => "Black_pawn.png",
						"R" => "Black_Rook.png",
						"H" => "Black_knight.png",
						"B" => "Black_elefant.png",
						"Q" => "Black_Queen.png",
						"K" => "Black_King.png",
						_ => "",
					};
				}
				var lenght = board.FoundFigureCoordinate(item);
				figuraProperty.Color = (lenght[0]+lenght[1])%2==0 ? Brushes.White : Brushes.Black;
				figuraProperty.FiguraNow = item;

				Dispatcher.UIThread.InvokeAsync(() =>
				{
					if (count < 64)
						this.borders[count] = ChangeBorderProperty(this.borders[count], figuraProperty);
				}).Wait();
				
				count++;
			}
			
		}
		private static Border ChangeBorderProperty(Border border , СellProperty property)
		{
			border.Background = property.Color;
			border.BorderBrush = property.Color;
			border.Child = new Image()
			{			
				Stretch = Stretch.Fill,
				Source = (Bitmap)new ImageConverter().Convert(property.Image, null, null, null)
			};
			border.DataContext = property.FiguraNow;
			return border;
		}
		public void PrintError(string ex)
		{
			Dispatcher.UIThread.InvokeAsync(() =>
			{
				var errorWindow = MessageBox.Avalonia.MessageBoxManager
				.GetMessageBoxStandardWindow(new MessageBoxStandardParams
				{
					ButtonDefinitions = ButtonEnum.OkAbort,
					ContentMessage = ex,
					Icon = MessageBox.Avalonia.Enums.Icon.Plus,
					Style = Style.UbuntuLinux
				});
				errorWindow.Show();
			});			
		}
	}
}
