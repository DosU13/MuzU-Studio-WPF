using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace MuzU_Studio.util;

internal class WhiteSpaceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;
        if (value is not string text) throw new NotSupportedException();
        return text.Replace("\r\n", "¶").Replace('\n', '¶').Replace(' ', '•');
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) return null;
        if (value is not string text) throw new NotSupportedException();
        return text.Replace('¶', '\n').Replace('•', ' ');
    }
}
