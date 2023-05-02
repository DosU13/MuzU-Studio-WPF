using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MuzU_Studio.util;

public class ToggleButtonBrushConverter : IValueConverter
{
    private static readonly SolidColorBrush disabledColor = 
        new SolidColorBrush(Color.FromRgb(0x1c, 0x1c, 0x21));
    private static readonly SolidColorBrush enabledColor = 
        new SolidColorBrush(Color.FromRgb(0xff, 0x54, 0x00));
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? enabledColor : disabledColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
