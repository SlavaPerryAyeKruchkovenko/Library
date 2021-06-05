using Avalonia.Media;
using MegaChess.Dekstop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MegaChess.Dekstop.ViewModels
{
	class SettingWindowViewModel : ViewModelBase
	{
		public ReadOnlyObservableCollection<string> Images;
		public ReadOnlyObservableCollection<IBrush> OntherColor;
		public SettingWindowViewModel()
		{
			this.Images = CreateColorCollection();
			this.OntherColor = CreateBackColorCollection();
		}
		private ReadOnlyObservableCollection<string> CreateColorCollection()
		{
			var collection = new ObservableCollection<string>();
			foreach (var item in GameField.GetColors())
				collection.Add(item);

			return new ReadOnlyObservableCollection<string>(collection);
		}
		private ReadOnlyObservableCollection<IBrush> CreateBackColorCollection()
		{
			var collection = new ObservableCollection<IBrush>();
			collection.Add(Brushes.Aqua);
			collection.Add(Brushes.White);
			collection.Add(Brushes.Red);
			collection.Add(Brushes.Black);
			collection.Add(Brushes.Azure);
			collection.Add(Brushes.Brown);
			collection.Add(Brushes.CornflowerBlue);
			collection.Add(Brushes.DarkCyan);
			collection.Add(Brushes.DarkGray);
			collection.Add(Brushes.Green);
			collection.Add(Brushes.Yellow);
			return new ReadOnlyObservableCollection<IBrush>(collection);
		}
		//private ReadOnlyObservableCollection<IBrush[]> CreateBoardColors()
		//{
		//	var brushes = new ObservableCollection<IBrush[]> ();
		//	foreach (var brush in CreateBackColorCollection())
		//	{
		//		foreach (var brush2 in CreateBackColorCollection())
		//		{
		//			if (brush.ToString() != brush2.ToString())
		//			{
		//				IBrush[] brushes1 = { brush, brush2 };
		//				brushes.Add(brushes1);
		//			}
		//		}
		//	}
		//	return new ReadOnlyObservableCollection<IBrush[]>(brushes);
		//}
		public void ChangeFiguraImage(string key)
		{
			GameField.ImageKey = key;
		}
		public void ChangeBoardColor(IBrush color, bool isFirstColor)
		{
			if (isFirstColor)
				GameField.SetColor(color, 1);
			else
				GameField.SetColor(color, 2);
		}
		public void ChangeOtherColor(IBrush color) => GameField.SetColor(color, 3);
	}
}
