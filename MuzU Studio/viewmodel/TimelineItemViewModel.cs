using MuzUHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

public class TimelineItemViewModel : BindableBase
{
    public string Time { get; set; }
    public double X { get; set; }

    public TimelineItemViewModel(int time, double x)
    {
        Time = time.ToString();
        X = x;
    }
}
