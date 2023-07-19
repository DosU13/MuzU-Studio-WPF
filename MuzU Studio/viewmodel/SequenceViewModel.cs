using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Modeling.Diagrams;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel.shared_property;
using MuzUStandard.data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

public class SequenceViewModel : BindableBase, ISequenceSharedProperty
{
    public readonly Sequence Sequence;
    private bool visible = true;

    private static readonly Random random = new();
    public SequenceViewModel(Sequence sequence)
    {
        this.Sequence = sequence;
        var hueProperty = sequence.SequenceTemplate.PropertiesList.
            FirstOrDefault(x => x.Name == HueName);
        if (hueProperty == null || !int.TryParse(hueProperty.Value, out int hue)) {
            hue = random.Next(256);
            sequence.SequenceTemplate.PropertiesList.Add(
                new Property { Name = HueName, Value = hue.ToString()}); 
        }
        Hue = hue;
        foreach(var node in sequence.NodeList) 
            Notes.Add(new NoteViewModel(node, this));

        var list = Notes.Select(x => x.X).ToList();
    }

    public Sequence Data => Sequence;
    private Color color;
    private Color darkerColor;
    private Color reverseColor;
    public Color Color
    {
        get => color;
        set
        {
            if (SetProperty(ref color, value))
                OnPropertyChanged(nameof(VisibilityColor));
        }
    }
    public Color DarkerColor
    {
        get => darkerColor;
        set => SetProperty(ref darkerColor, value);
    }
    public Color ReverseColor
    {
        get => reverseColor;
        set => SetProperty(ref reverseColor, value);
    }

    private const string HueName = "Hue";
    private int _hue = -1;
    public int Hue { 
        get => _hue;
        set
        {
            if (SetProperty(ref _hue, value))
            {
                Sequence.SequenceTemplate.PropertiesList.
                    First(x => x.Name == HueName).Value = value.ToString();
                HslColor hslColor = new(value, 240, 176);
                var drColor = hslColor.ToRgbColor();
                Color = Color.FromRgb(drColor.R, drColor.G, drColor.B);
                HslColor hslDarkerColor = new(value, 240, 40);
                var drDarkerColor = hslDarkerColor.ToRgbColor();
                DarkerColor = Color.FromRgb(drDarkerColor.R, drDarkerColor.G, drDarkerColor.B);
                HslColor hslReverseColor = new((value+120)%240, 240, 176);
                var drReverseColor = hslReverseColor.ToRgbColor();
                ReverseColor = Color.FromRgb(drReverseColor.R, drReverseColor.G, drReverseColor.B);
            }
        }
    }
    
    public Color VisibilityColor => Visible ? Color : Color.FromArgb(0,0,0,0);
    public bool Visible { get => visible;
        set { 
            if (SetProperty(ref visible, value))
                OnPropertyChanged(nameof(VisibilityColor));
        } }

    public string Name
    {
        get => Sequence.Name;
        set
        {
            Sequence.Name = value;
            OnPropertyChanged();
        }
    }

    private ICommand? visibilityCommand;
    public ICommand VisibilityCommand
    {
        get
        {
            visibilityCommand ??= new RelayCommand(param => ToggleVisibility());
            return visibilityCommand;
        }
    }
    public void ToggleVisibility() => Visible = !Visible;

    private ICommand? removeCommand;
    public ICommand RemoveCommand =>
        removeCommand ??= new RelayCommand(param => Remove());

    public bool LyricsEnabled
    {
        get => Sequence.SequenceTemplate.LyricsEnabled;
        set
        {
            if (Sequence.SequenceTemplate.LyricsEnabled == value) return;
            Sequence.SequenceTemplate.LyricsEnabled = value;
            OnPropertyChanged();
        }
    }

    public static readonly string Nameof_LyricsEnabled = nameof(LyricsEnabled);

    public readonly SortedSet<NoteViewModel> Notes = new(new NoteViewModelComparer());

    private void Remove()
    {
        var sequenceListModel = App.Current.Services.GetService<SequenceListModel>()!;
        sequenceListModel.Sequences.Remove(this);
        sequenceListModel.ReinitCollections();
    }

    internal void AddNote(NoteViewModel note)
    {
        Notes.Add(note);
        Data.NodeList.Add(note.Data);
    }

    internal void RemoveNote(NoteViewModel note)
    {
        Notes.Remove(note);
        Data.NodeList.Remove(note.Data);
    }

    internal void NotifyNoteChanged(NoteViewModel note)
    {
        Notes.Remove(note);
        Notes.Add(note);
    }

    class NoteViewModelComparer : IComparer<NoteViewModel>
    {
        public int Compare(NoteViewModel? x, NoteViewModel? y)
        {
            if (y == null) return 1;
            if (x == null) return -1;
            return x.X.CompareTo(y.X);
        }
    }
}
