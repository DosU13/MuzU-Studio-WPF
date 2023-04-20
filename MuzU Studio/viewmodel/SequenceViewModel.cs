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

internal class SequenceViewModel : BindableBase, ISequenceSharedProperty
{
    private readonly Sequence sequence;
    private bool visible = true;

    private static readonly Random random = new();
    public SequenceViewModel(Sequence sequence)
    {
        this.sequence = sequence;
        var hueProperty = sequence.SequenceTemplate.PropertiesList.List.
            FirstOrDefault(x => x.Name == HueName);
        if (hueProperty == null || !int.TryParse(hueProperty.Value, out int hue)) {
            hue = random.Next(256);
            sequence.SequenceTemplate.PropertiesList.List.Add(
                new Property { Name = HueName, Value = hue.ToString()}); 
        }
        Hue = hue;
    }

    public Sequence Data => sequence;
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
                sequence.SequenceTemplate.PropertiesList.List.
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
        get => sequence.Name;
        set
        {
            sequence.Name = value;
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

    private void Remove()
    {
        var sequenceListModel = App.Current.Services.GetService<SequenceListModel>()!;
        sequenceListModel.Sequences.Remove(this);
        sequenceListModel.ReinitCollections();
    }
}
