using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;

namespace MegaChess.Dekstop.Converter
{
	class ImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            if (value != null)
            {
                var name = value as string;
                var path = "C:\\Users\\Slava\\Desktop\\visual\\ClassLibrary\\Library\\MegaChees\\MegaChess.Dekstop\\Assets\\";
                if (name != null && File.Exists(path + name)) 
                {
                    var image = new Bitmap(path + name);
                    return image;
                }
                else
                {
                    return null;
                }
                  
            }
            return null;
        }

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
