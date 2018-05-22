using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageServiceGUI.View
{
    public class ColorConverter : IValueConverter
    {
        /// <summary>
        /// Converting our message type to its appropriate color
        /// </summary>
        /// <param name="value"> The message type</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "INFO":
                    return Brushes.LightGreen;
                case "ERROR":
                    return Brushes.Red;
                case "WARNING":
                    return Brushes.Yellow;
            }
            return Brushes.Violet;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
