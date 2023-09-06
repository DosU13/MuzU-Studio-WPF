using Melanchall.DryWetMidi.Interaction;
using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel.shared_property;
using MuzUStandard.data;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

/// <summary>
/// Defines the data-model for a simple displayable rectangle.
/// </summary>
public class NoteViewModel : BindableBase
{
    static int NOTE_HEIGHT => PanAndZoomModel.NOTE_HEIGHT;

    #region Data Members
    /// <summary>
    /// Xml Data of the note
    /// </summary>
    private readonly Node data;

    private readonly ISequenceSharedProperty parent;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    private bool isSelected = false;

    #endregion Data Members

    public NoteViewModel(Node data, ISequenceSharedProperty parent)
    {
        this.data = data;
        this.parent = parent;
        App.Current.Services.GetService<PanAndZoomModel>()!.PropertyChanged += PanAndZoom_PropertyChanged;
    }

    private void PanAndZoom_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ReversedWidth));
    }

    /// <summary>
    /// Xml Data repository
    /// </summary>
    public Node Data { get { return data; } }

    /// <summary>
    /// The X coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public double X
    {
        get => PanAndZoomModel.FromMicroseconds(data.Time);
        set
        {
            var snappedValue = App.Current.Services.GetService<PianoRollModel>()!.SnapToGrid(value);
            ForceSetX(snappedValue);
        }
    }

    public void ForceSetX(double x)
    {
        var newTime = PanAndZoomModel.ToMicroseconds(x);
        if (newTime != data.Time)
        {
            data.Time = newTime;
            OnPropertyChanged(nameof(X));
            (parent as SequenceViewModel)!.NotifyNoteChanged(this);
        }
    }

    public MusicalTimeSpan MusicalTimeSpan { get {
            var tempoMap = TempoMap.Create(Melanchall.DryWetMidi.Interaction.Tempo.FromBeatsPerMinute(128));
            return TimeConverter.ConvertTo<MusicalTimeSpan>(
                new MetricTimeSpan(data.Time), tempoMap);
        }
    }

    /// <summary>
    /// The Y coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public double Y
    {
        get => (data.Note??64) * NOTE_HEIGHT;
        set {
            int newValue = (int)Math.Round(value / NOTE_HEIGHT);
            if (data.Note == newValue) return;
            data.Note = newValue;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The width of the rectangle (in content coordinates).
    /// </summary>
    public double Width
    {
        get => PanAndZoomModel.FromMicroseconds(data.Length ?? 500_000L / 8);
        set
        {
            var snappedValue = App.Current.Services.GetService<PianoRollModel>()!.SnapToGrid(value);
            ForceSetWidth(snappedValue);
        }
    }

    public double ReversedWidth
    {
        get => Width / App.Current.Services.GetService<PanAndZoomModel>()!.ContentScaleXAspectReverser;
    }

    public void ForceSetWidth(double width)
    {
        var newLength = PanAndZoomModel.ToMicroseconds(width);
        if (newLength != data.Length)
        {
            data.Length = newLength;
            OnPropertyChanged(nameof(Width));
        }
    }

    /// <summary>
    /// The height of the rectangle (in content coordinates).
    /// </summary>
    public double Height
    {
        get => NOTE_HEIGHT;
    }

    public ISequenceSharedProperty Parent => parent;

    public Color BorderColor => IsSelected ? Parent.ReverseColor : Parent.DarkerColor;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    public bool IsSelected
    {
        get => isSelected;
        set { 
            if (SetProperty(ref isSelected, value)) 
                OnPropertyChanged(nameof(BorderColor));
            Console.WriteLine(X);
        }
    }

    public string Lyrics {
        get => data.Lyrics;
        set
        {
            if (data.Lyrics == value) return;
            data.Lyrics = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(LyricsWithoutNewlines));
        }
    }

    public string LyricsWithoutNewlines
    {
        get => data.Lyrics?.Replace("\r\n","")?.Replace("\n", "")??string.Empty;
    }
}
