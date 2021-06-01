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

using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive.Subjects;
using System;
using System.Threading;

namespace MegaChess.Dekstop.Converter
{
	class Drawer : BaseModel, IDrawer
	{
		public Drawer(ObservableCollection<Border>[] _borders , IObservable<Figura> _figura)
		{		
			this.borders = _borders[0];

			_figura.Subscribe<Figura>(ChangeFigura);		
		}
		private readonly ObservableCollection<Border> borders;
		private Figura figura1;
		private Board board;
		private bool isFirstMove = true;
		public void Clear()
		{
			this.borders.Clear();
		}
		private void ChangeFigura(Figura figura)
		{
			this.figura1 = figura;
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
			while(this.figura1 == figura)
			{
				
			}
			if(this.isFirstMove)
			{
				if (this.figura1 is Empty)
				{
					return MoveCursor(x, y, board);
				}				
				CircleCorectCell(this.figura1, this.borders, this.board);
			}
			this.isFirstMove = !this.isFirstMove;
			return this.figura1;
		}	
		public static void CircleCorectCell(Figura figura , ObservableCollection<Border> borders , Board board)
		{
			foreach (var item in borders)
			{
				Dispatcher.UIThread.InvokeAsync(() =>
				{
					var lenght = board.CountLengh(figura, (Figura)item.DataContext);
					try
					{
						if (!figura.HaveUnrealSteep(board, lenght))
						{
							item.BorderBrush = Field.GetBorderBrushesColor();
						}
					}
					catch (Exception) { }
				}).Wait();					
			}
		}
		public void PrintBoard(Board board)
		{
			this.board = board;
			PrintGameBoard(board);
		}
		private void PrintGameBoard(Board board)
		{			
			int count = 0;
			foreach (var item in board.GetFiguras())
			{
				var figuraProperty = new СellProperty
				{
					Image = SelectImageRef(item),
					Color = Field.GetColor(item, board),
					FiguraNow = item
				};

				Dispatcher.UIThread.InvokeAsync(() =>
				{
					if (count < 64)
						this.borders[count] = ChangeBorderProperty(this.borders[count], figuraProperty);
				}).Wait();

				count++;
			}
		}
		private static string SelectImageRef(Figura figura)
		{
			if (figura.IsMyFigura == true)
			{
				return figura.ToString().ToUpper() switch
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
			else if (figura.IsMyFigura == false)
			{
				return figura.ToString().ToUpper() switch
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
			return null;
		}
		private static Border ChangeBorderProperty(Border border , СellProperty property)
		{			
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
