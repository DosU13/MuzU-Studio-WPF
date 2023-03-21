using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.viewmodel.shared_property;
using MuzUHub;
using MuzUStandard.data;
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
    private double _x;
    public double X { 
        get => _x; 
    }
    public ThicknessSharedProperty LineThickness { get; }

    public TimelineItemViewModel(int time, double x, ThicknessSharedProperty thickness)
    {
        Time = time.ToString();
        _x = x;
        LineThickness = thickness;
    }
}
