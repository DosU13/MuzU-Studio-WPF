using MuzU_Studio.util;
using MuzU_Studio.viewmodel.shared_property;

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
