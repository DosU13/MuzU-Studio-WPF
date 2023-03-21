using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
using MuzU_Studio.viewmodel.shared_property;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MuzU_Studio.model;

public partial class PianoRollModel
{
    public PianoRollModel() {
        App.Current.Services.GetService<PanAndZoomModel>()!.PropertyChanged += PanAndZoom_PropertyChanged;
        UpdateTempo();
    }

    private void PanAndZoom_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not PanAndZoomModel panAndZoomModel) return;
        if(e.PropertyName == PanAndZoomModel.Nameof_ContentWidth) UpdateTimelineItems();
        else if(e.PropertyName == PanAndZoomModel.Nameof_ContentViewportWidth) 
            UpdateTimelineItemThickness(panAndZoomModel);
    }

    private double _beatLength = PanAndZoomModel.FromMicroseconds(1_000_000);
    public double BeatLength { get => _beatLength;
        set {
            if (_beatLength == value) return;
            _beatLength = value;
            UpdateTimelineItems();
        } 
    }
    private double beatSnapToGrid = 0.0;
    public double BeatSnapToGrid
    {
        get => beatSnapToGrid;
        set
        {
            if(beatSnapToGrid == value) return;
            beatSnapToGrid = value;
        }
    }
    public double SnapToGrid(double value)
    {
        if (BeatSnapToGrid == 0) return value;
        return RoundI(value, BeatLength * BeatSnapToGrid);
    }
    public static double RoundI(double number, double roundingInterval)
    {
        return (double)((decimal)roundingInterval * Math.Round((decimal)number / (decimal)roundingInterval, MidpointRounding.AwayFromZero));
    }

    #region Piano Keys

    private List<PianoKeyViewModel>? pianoKeys;
    public List<PianoKeyViewModel> PianoKeys => pianoKeys ??= InitPianoKeys();

    private List<PianoKeyViewModel> InitPianoKeys()
    {
        // Populate the collection with PianoKeyViewModels for all 128 MIDI keys
        pianoKeys = new();
        for (int i = 0; i < 128; i++)
        {
            pianoKeys.Add(new PianoKeyViewModel(i));
        }
        return pianoKeys;
    }
    #endregion

    #region TimeLine
    private readonly ObservableCollection<TimelineItemViewModel> timelineItems = new();
    public ObservableCollection<TimelineItemViewModel> TimelineItems => timelineItems;

    private void UpdateTimelineItems()
    {
        // Populate the collection with PianoKeyViewModels for all 128 MIDI keys
        timelineItems.Clear();
        var panAndZoomModel = App.Current.Services.GetService<PanAndZoomModel>()!;
        for (int ind = 0; ind * BeatLength < panAndZoomModel.ContentWidth; ind++)
        {
            ThicknessSharedProperty thickness;
            if (ind % BeatsPerSection == 0) thickness = _sectionsThickness;
            else if (ind % BeatsPerWhole == 0) thickness = _wholesThickness;
            else thickness = _beatsThickness;
            timelineItems.Add(new TimelineItemViewModel(ind, ind*BeatLength, thickness));
        }
    }

    private const int MAX_VISIBLE_LINES = 1 << 6;
    private void UpdateTimelineItemThickness(PanAndZoomModel panAndZoomModel)
    {
        var selectionsCount = panAndZoomModel.ContentViewportWidth / (BeatLength * BeatsPerSection);
        var wholesCount = panAndZoomModel.ContentViewportWidth / (BeatLength * BeatsPerWhole);
        var beatsCount = panAndZoomModel.ContentViewportWidth / BeatLength;
        int[] factors;
        if (beatsCount <= MAX_VISIBLE_LINES) factors = new[] {1,2,4};
        else if (wholesCount <= MAX_VISIBLE_LINES) factors = new[] {0,1,2};
        else if (selectionsCount <= MAX_VISIBLE_LINES) factors = new[] {0,0,1};
        else factors = new[] { 0, 0, 0 };
        _beatsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[0];
        _wholesThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[1];
        _sectionsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[2];
    }

    private MuzUData MuzUData => App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.MuzUProject.MuzUData;
    private int BeatsPerWhole => MuzUData.Tempo.TimeSignature.Denominator;
    private int BeatsPerSection => 4 * MuzUData.Tempo.TimeSignature.Denominator;
    private readonly ThicknessSharedProperty _beatsThickness = new(1);
    private readonly ThicknessSharedProperty _wholesThickness = new(2);
    private readonly ThicknessSharedProperty _sectionsThickness = new(4);

    #endregion

    internal void UpdateTempo()
    {
        var muzuData = App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.MuzUProject.MuzUData;
        BeatLength = PanAndZoomModel.FromMicroseconds(60_000_000) / muzuData.Tempo.BPM;
    }
}
