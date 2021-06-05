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
using System.Collections.Generic;
using MegaChess.Dekstop.Views;
using System.Linq;
using Avalonia.Controls.Shapes;

namespace MegaChess.Dekstop.Converter
{
	class Drawer : BaseModel, IDrawer
	{
		public Drawer(ObservableCollection<Border>[] _borders , IObservable<Figura> _figura , IObserver<bool> isWhiteMove)
		{
			if (_borders.Length != 3)
				throw new Exception("В параметре нужно передать 3 коллекции: Игровое поле, белые мертвые фигуры , черные мертвые фигуры");

			this.whiteOrBlackObserver = isWhiteMove;
			this.gameBorders = _borders[0];
			this.diedWhiteBorders = _borders[1];
			this.diedBlackBorders = _borders[2];

			_figura.Subscribe<Figura>(ChangeFigura);		
		}
		private readonly ObservableCollection<Border> gameBorders;
		private readonly ObservableCollection<Border> diedWhiteBorders;
		private readonly ObservableCollection<Border> diedBlackBorders;
		private readonly IObserver<bool> whiteOrBlackObserver;
		private Figura figura1;
		private Board board;
		private bool isFirstMove = true;
		public void Clear()
		{
			this.gameBorders.Clear();
			this.diedWhiteBorders.Clear();
			this.diedBlackBorders.Clear();
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
			this.whiteOrBlackObserver.OnNext(board.IsWhiteMove);
			var figura = this.figura1;
			while(this.figura1 == figura)
			{
				// тут будет таймер
			}
			if(this.isFirstMove)
			{				
				if (this.figura1 is Empty || this.figura1.IsMyFigura!=board.IsWhiteMove)// если выбирается фигура , не того человека кто ходит происходит скип
				{
					return MoveCursor(x, y, board);
				}				
				CircleCorectCell(this.figura1, this.gameBorders, this.board);
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
							if (item.Child is Image)
								item.BorderBrush = GameField.GetBorderBrushesColor();
							else
								item.Child = new Ellipse()
								{
									Width = item.Width / 3,
									Height = item.Height / 3,
									Fill = GameField.GetBorderBrushesColor()
								};
						}
					}
					catch (Exception) { }
				}).Wait();					
			}
		}
		public void PrintBoard(Board board)
		{
			this.board = board;
			PrintGameBoard(board, this.gameBorders);// print game figuras;
			PrintGameFiguras(board.GetDiedFiguras(true), this.diedWhiteBorders); // print died white figuras
			PrintGameFiguras(board.GetDiedFiguras(false), this.diedBlackBorders); // print died black figuras			
		}
		private static void PrintGameBoard(Board board, ObservableCollection<Border> borders)
		{
			int count = 0;
			foreach (var item in board.GetFiguras())
			{
				var figuraProperty = new СellProperty
				{
					Image = SelectImageRef(item),
					Color = GameField.GetColor(item , board),
					FiguraNow = item
				};
				Dispatcher.UIThread.InvokeAsync(() =>
				{
					if (count < borders.Count)
						borders[count] = ChangeBorderProperty(borders[count], figuraProperty , true);
				}).Wait();

				count++;
			}
		}
		private static void PrintGameFiguras(IEnumerable<Figura> figuras , ObservableCollection<Border> borders)
		{
			int count = 0;
			foreach (var item in figuras)
			{
				var figuraProperty = new СellProperty
				{
					Image = item == null ?  "" : SelectImageRef(item)
				};
				Dispatcher.UIThread.InvokeAsync(() =>
				{
					if (count < borders.Count)
						borders[count] = ChangeBorderProperty(borders[count], figuraProperty , false);
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
		private static Border ChangeBorderProperty(Border border , СellProperty property , bool needChange)
		{			
			if(needChange)
			{
				border.BorderBrush = property.Color;
				border.DataContext = property.FiguraNow;
			}		
			if(property.Image != null)
			{
				border.Child = new Image()
				{
					Stretch = Stretch.Fill,
					Source = (Bitmap)new ImageConverter().Convert(property.Image, null, null, null)
				};
			}
			else
			{
				border.Child = null;
			}
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

		public void ChangePawn(Pawn pawn, Board board)
		{
			var subject = new Subject<Figura>();
			subject.Subscribe(ChangeFigura);
			short[] counts = CountNums(board);

			Dispatcher.UIThread.InvokeAsync(() =>
			{
				var windowPanel = new FigurasPanel(board.IsWhiteMove, counts, subject);
				windowPanel.Topmost = true;
				windowPanel.Show();				
			}).Wait();
			var figura = this.figura1;
			while (this.figura1 == figura)
			{
				// здесь тоже будет таймер
			}
			board.ReplaceFigura(pawn, this.figura1);
		}
		private static short[] CountNums(Board board)
		{
			short[] counts = new short[4];
			counts[0] = CountNewFiuguraNum(board, new Queen(board.IsWhiteMove, 2));
			counts[1] = CountNewFiuguraNum(board, new Rook(board.IsWhiteMove, 2));
			counts[2] = CountNewFiuguraNum(board, new Bishop(board.IsWhiteMove, 2));
			counts[3] = CountNewFiuguraNum(board, new Knight(board.IsWhiteMove, 2));
			return counts;
		}
		private static short CountNewFiuguraNum(Board board , Figura figura)
		{
			foreach (var item in board.GetFiguras().Where(x => x.IsMyFigura == figura.IsMyFigura))
			{
				if(item.Equals(figura))
				{
					return CountNewFiuguraNum(board, SpotFiguraType(figura));
				}
			}
			return figura.Number;
		}
		private static Figura SpotFiguraType(Figura figura)
		{
			if (figura is Queen)
				return new Queen(figura.IsMyFigura, (short)(figura.Number + 1));
			else if (figura is Knight)
				return new Knight(figura.IsMyFigura, (short)(figura.Number + 1));
			else if (figura is Bishop)
				return new Bishop(figura.IsMyFigura, (short)(figura.Number + 1));
			else if (figura is Rook)
				return new Rook(figura.IsMyFigura, (short)(figura.Number + 1));
			else 
				throw new Exception("Не правельный тип фигуры");
		}
	}
}
