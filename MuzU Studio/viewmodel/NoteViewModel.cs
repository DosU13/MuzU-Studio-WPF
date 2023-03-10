using Melanchall.DryWetMidi.Interaction;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel.util;
using MuzUStandard.data;
using System;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

/// <summary>
/// Defines the data-model for a simple displayable rectangle.
/// </summary>
public class NoteViewModel : BindableBase
{
    static double HOR_SCALE => PanAndZoomModel.HOR_SCALE;
    static int VER_SCALE => PanAndZoomModel.VER_SCALE;

    #region Data Members
    /// <summary>
    /// Xml Data of the note
    /// </summary>
    private readonly Node node;

    private readonly ISequenceViewModel parent;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    private bool isSelected = false;

    #endregion Data Members

    public NoteViewModel(Node node, ISequenceViewModel parent)
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
        get => node.Time * HOR_SCALE;
        set
        {
            var newTime = (long)(value / HOR_SCALE);
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
        get => node.Note.Value * VER_SCALE;
        set {
            int newValue = (int)Math.Round((double)value / VER_SCALE);
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
        get => (node.Length ?? 500_000.0/8) * HOR_SCALE;
        set
        {
            var newLength = (long)(value / HOR_SCALE);
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
        get => VER_SCALE;
    }

    public ISequenceViewModel Parent => parent;

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
