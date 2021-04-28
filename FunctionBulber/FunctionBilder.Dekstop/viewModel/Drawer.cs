using Avalonia.Controls;
using FunctionBulber.Logic;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;

namespace FunctionBilder.Dekstop.ViewModel
{
	public class Drawer : IDrawer
	{
		private object input;
		public Drawer(object _input)
		{
			this.input = _input;
		}
		public void Draw(IDrawer.Instalize func)
		{
			try
			{
				func();
			}
			catch(Exception ex)
			{
				if (this.input is TextBox box)
				{
					box.Text = ex.Message;
				}
				else
				{
					var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
					.GetMessageBoxStandardWindow(new MessageBoxStandardParams
					{
						ButtonDefinitions = ButtonEnum.OkCancel,
						ContentTitle = "Error",
						ContentMessage = ex.Message,
						Icon = Icon.Error,
						Style = Style.MacOs
					});
					msBoxStandardWindow.Show();
				}
			}
		}
	}
}
