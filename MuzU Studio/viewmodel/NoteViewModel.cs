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
    private readonly Node node;

    private readonly ISequenceSharedProperty parent;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    private bool isSelected = false;

    #endregion Data Members

    public NoteViewModel(Node node, ISequenceSharedProperty parent)
    {
        this.node = node;
        this.parent = parent;
    }

    /// <summary>
    /// Xml Data repository
    /// </summary>
    public Node Node { get { return node; } }

    /// <summary>
    /// The X coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public double X
    {
        get => PanAndZoomModel.FromMicroseconds(node.Time);
        set
        {
            var snappedValue = App.Current.Services.GetService<PianoRollModel>()!.SnapToGrid(value);
            var newTime = PanAndZoomModel.ToMicroseconds(snappedValue);
            if (newTime != node.Time) {
                node.Time = newTime;
                OnPropertyChanged();
            }
        }
    }

    public MusicalTimeSpan MusicalTimeSpan { get {
            var tempoMap = TempoMap.Create(Melanchall.DryWetMidi.Interaction.Tempo.FromBeatsPerMinute(128));
            return TimeConverter.ConvertTo<MusicalTimeSpan>(
                new MetricTimeSpan(node.Time), tempoMap);
        }
    }

    /// <summary>
    /// The Y coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public int Y
    {
        get => node.Note.Value * NOTE_HEIGHT;
        set {
            int newValue = (int)Math.Round((double)value / NOTE_HEIGHT);
            if (node.Note.Value == newValue) return;
            node.Note = newValue;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The width of the rectangle (in content coordinates).
    /// </summary>
    public double Width
    {
        get => PanAndZoomModel.FromMicroseconds(node.Length ?? 500_000L/8);
        set
        {
            var newLength = PanAndZoomModel.ToMicroseconds(value);
            if (newLength != node.Length)
            {
                node.Length = newLength;
                OnPropertyChanged();
            }
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

    public Color BorderColor => IsSelected ? Color.FromRgb(250,100,0) : Parent.Color;

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
}
