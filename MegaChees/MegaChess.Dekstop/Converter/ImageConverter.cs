using Avalonia.Data.Converters;
using System;
using System.Drawing;
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
                if (name != null && File.Exists(name))
                {
                    var image = new Bitmap(name);
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
