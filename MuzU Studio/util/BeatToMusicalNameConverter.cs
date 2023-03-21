using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using Mehroz;

namespace MuzU_Studio.util;

public class BeatToMusicalNameConverter : IValueConverter
{
    /// <summary>
    /// When you wirte there "1/2 beat" It sets SnapToGridBeats property to 0.5D. 
    /// Text format "n/n word". Word can be Bar (4 beat), beat and step (0.128 beat). 
    /// If there is no word than it's beat. Text validates and sets back. Text format is:
    /// if(SnapToGridBeats > 1) "n/n bar"
    /// else if(SnapToGridBeats > 1/timeSignatureDenominator) "n/n beat"
    /// else "n/n step"
    /// Also if n/n is not fraction then "n word" and if its 1 then "word"
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double _double) return "none";
        if (_double == 0) return "none";
        TimeType timeType;
        double val;
        var timeSignDen = App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.
            MuzUProject.MuzUData.Tempo.TimeSignature.Denominator;
        if (_double > 1)
        {
            timeType = TimeType.BAR;
            val = _double / timeSignDen;
        } else if(_double > 1.0 / timeSignDen)
        {
            timeType = TimeType.BEAT;
            val = _double;
        }
        else
        {
            timeType = TimeType.STEP;
            val = _double * 8;
        }
        return ToFractionString(val)+" "+timeType.ToString().ToLower();
    }

    private static string ToFractionString(double val)
    {
        if((int)val == val) return val.ToString();
        foreach(var den in MusicalNameRule.Denominators)
        {
            var num = den * val;
            if ((int)num == num) return $"{num}/{den}";
        }
        MessageBox.Show("Unproper Denominator");
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string str) return 0.0;
        str = str.ToLower().Replace(" ", "");
        if (str == "none") return 0.0;
        TimeType? timeType = null;
        if(str.EndsWith("bar"))
        {
            str = str[..^3];
            timeType = TimeType.BAR;
        }else if (str.EndsWith("beat"))
        {
            str = str[..^4];
            timeType = TimeType.BEAT;
        }else if (str.EndsWith("step"))
        {
            str = str[..^4];
            timeType = TimeType.STEP;
        }
        if (str == "") str = "1";       
        double res;
        if (str.Contains('/'))
        {
            int ind = str.IndexOf('/');
            string numStr = str[..ind], denStr = str[(ind + 1)..];
            if (int.TryParse(numStr, out int num) && int.TryParse(denStr, out int den))
                res = (double)num / den;
            else throw new Exception();
        }
        else if (int.TryParse(str, out int val)) res = val;
        else throw new Exception();
        var muzuData = App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.MuzUProject.MuzUData;
        if (timeType == TimeType.BAR) res *= muzuData.Tempo.TimeSignature.Denominator;
        else if (timeType == TimeType.STEP) res /= 8;
        return res;
    }

    private enum TimeType{
        BAR, BEAT, STEP
    }
}
