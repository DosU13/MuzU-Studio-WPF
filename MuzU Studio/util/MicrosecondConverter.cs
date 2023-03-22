using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MuzU_Studio.util;

public class MicrosecondConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.GetType() != typeof(long)) throw new InvalidCastException("long type needed: " + value);
        return ((long)value) / 1_000_000d;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is not string str) throw new InvalidCastException("string type needed: " + value);
        str = new string(str.Replace(',', '.').Where(x => char.IsDigit(x) || x == '.' || x=='-').ToArray());
        return (long)(double.Parse(str, CultureInfo.InvariantCulture) * 1_000_000);
    }
}
