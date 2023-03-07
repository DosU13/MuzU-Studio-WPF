using Microsoft.VisualStudio.Modeling.Diagrams;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel.util;
using MuzUStandard.data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

internal class SequenceViewModel : BindableBase, ISequenceViewModel
{
    private Sequence data;
    private Color color;
    private bool visible = true;
    private ObservableCollection<NoteViewModel> notes;

    private static Random random = new Random();
    public SequenceViewModel(Sequence data)
    {
        this.data = data;
        HslColor hslColor = new HslColor(Convert.ToByte(random.Next(256)), 256, 192);
        var drColor = hslColor.ToRgbColor();
        color = Color.FromRgb(drColor.R, drColor.G, drColor.B);
        Console.WriteLine($"{color}");

        notes = new ObservableCollection<NoteViewModel>();
        foreach (var note in data.NodeList.List)
            notes.Add(new NoteViewModel(note, this));
    }

    public Sequence Data => data;
    public ObservableCollection<NoteViewModel> Notes => notes;
    public Color Color { get => color; set => SetProperty(ref color, value); }
    
    public Color VisibilityColor => Visible ? Color : Color.FromArgb(0,0,0,0);
    public bool Visible { get => visible;
        set { 
            if (SetProperty(ref visible, value))
                OnPropertyChanged(nameof(VisibilityColor));
        } }

    public string Name
    {
        get => data.Name;
        set
        {
            data.Name = value;
            OnPropertyChanged();
        }
    }

    private ICommand visibilityCommand;
    public ICommand VisibilityCommand
    {
        get
        {
            if (visibilityCommand == null)
                visibilityCommand = new RelayCommand(param => ToggleVisibility());
            return visibilityCommand;
        }
    }
    public void ToggleVisibility() => Visible = !Visible;

    public void Sequence_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        ToggleVisibility();
    }
}
