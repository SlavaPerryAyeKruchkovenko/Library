using MegaChess.Dekstop.Models;
using MegaChess.Dekstop.ViewModels;
using MegaChess.Logic;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;

namespace MegaChess.Dekstop.Converter
{
	class Drawer : IDrawer
	{
		public Drawer(ObservableCollection<СellProperty > _figuraProperties)
		{
			this.figuraProperties = _figuraProperties;
		}
		private ObservableCollection<СellProperty > figuraProperties;
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
			var figura = GameWindowViewModel.SelectedFigura;
			while(figura == GameWindowViewModel.SelectedFigura)
			{
				Thread.Sleep(1);
			}
			return GameWindowViewModel.SelectedFigura;
		}		
		public void PrintBoard(Board board)
		{
			this.figuraProperties = new ObservableCollection<СellProperty >();
			bool IsWhiteFigure = true;

			foreach (var item in board.GetFiguras())
			{
				var figuraProperty = new СellProperty ();

				if (item.IsMyFigura == true)
				{
					figuraProperty.Image = item.ToString().ToUpper() switch
					{
						"P" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"R" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"H" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"B" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"Q" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"K" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						_ => "",
					};
				}
				else if (item.IsMyFigura == false)
				{
					figuraProperty.Image = item.ToString().ToUpper() switch
					{
						"P" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"R" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"H" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"B" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"Q" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						"K" => "avares://MegaChess.Dekstop/Assets/Megachess-logo.png",
						_ => "",
					};
				}
				figuraProperty.Color = IsWhiteFigure ? SettingWindowViewModel.FirstColor : SettingWindowViewModel.SecondColor;
				figuraProperty.FiguraNow = item;
				this.figuraProperties.Add(figuraProperty);

				IsWhiteFigure = !IsWhiteFigure;
			}
		}

		public void PrintError(string ex)
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
		}
	}
}
