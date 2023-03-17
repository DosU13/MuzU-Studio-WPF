namespace PianoRoll;

public class TimelineItemViewModel : BindableBase
{
    public string Time { get; set; }
    public double X { get; set; }
    public double Thickness { get; set; }
    private static readonly double baseThickness = 100;

    public TimelineItemViewModel(int time, double x)
    {
        Time = time.ToString();
        X = x;
        if (time % 16 == 0) Thickness = baseThickness * 4;
        else if (time % 4 == 0) Thickness = baseThickness * 2;
        else Thickness= baseThickness * 1;
    }
}
