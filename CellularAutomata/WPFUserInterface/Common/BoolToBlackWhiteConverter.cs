using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFUserInterface.Common;

public class BoolToBlackWhiteConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Brushes.White;
        if (value is bool val)
        {
            return val ? Brushes.ForestGreen : Brushes.White;
        }

        return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return false;
        if (value is SolidColorBrush brush)
        {
            if (brush == Brushes.White)
                return false;
            else
                return true;
        }

        return false;
    }
}