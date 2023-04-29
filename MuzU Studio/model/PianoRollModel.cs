using Melanchall.DryWetMidi.MusicTheory;
using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.service;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
using MuzU_Studio.viewmodel.shared_property;
using MuzUStandard.data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MuzU_Studio.model;

public class PianoRollModel
{
    public PianoRollModel() {
        App.Current.Services.GetService<PanAndZoomModel>()!.PropertyChanged += PanAndZoom_PropertyChanged;
    }

    private void PanAndZoom_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not PanAndZoomModel panAndZoomModel) return;
        if (e.PropertyName == PanAndZoomModel.Nameof_ContentWidth) UpdateTimelineItems();
        else if (e.PropertyName == PanAndZoomModel.Nameof_ContentViewportWidth)
            UpdateTimelineItemThickness(panAndZoomModel);
    }

    public static double BeatLength { get {
            var muzuData = App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.MuzUProject.MuzUData;
            return PanAndZoomModel.FromMicroseconds(60_000_000) / muzuData.Tempo.BPM;
        } }
    public double StepLength => BeatLength / StepsPerBeat;
    
    private double snapToGridInterval = 1.0;
    public double SnapToGridInterval
    {
        get => snapToGridInterval;
        set
        {
            if(snapToGridInterval == value) return;
            snapToGridInterval = value;
        }
    }
    public static double SnapToGrid(double value, double intervalInBeats) =>
        Utils.RoundWithInterval(value, BeatLength * intervalInBeats);
    public double SnapToGrid(double value) => SnapToGrid(value, SnapToGridInterval);
    public double SnapToGridByFloor(double value) =>
        Utils.FloorWithInterval(value, BeatLength * SnapToGridInterval);

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
        for (int ind = 0; ind * StepLength < panAndZoomModel.ContentWidth; ind++)
        {
            ThicknessSharedProperty thickness;
            if (ind % StepsPerSection == 0) thickness = _sectionsThickness;
            else if (ind % StepsPerWhole == 0) thickness = _wholesThickness;
            else if(ind % StepsPerBeat == 0) thickness = _beatsThickness;
            else if(ind % StepsPerSubbeat == 0) thickness = _subbeatThickness;
            else thickness = _stepsThickness;
            timelineItems.Add(new TimelineItemViewModel(ind, ind * StepLength, thickness));
        }
    }

    private const int MAX_VISIBLE_LINES = 1 << 6;
    private void UpdateTimelineItemThickness(PanAndZoomModel panAndZoomModel)
    {
        var selectionsCount = panAndZoomModel.ContentViewportWidth / (StepLength * StepsPerSection);
        var wholesCount = panAndZoomModel.ContentViewportWidth / (StepLength * StepsPerWhole);
        var beatsCount = panAndZoomModel.ContentViewportWidth / BeatLength;
        var subbeatsCount = panAndZoomModel.ContentViewportWidth / (StepLength * StepsPerSubbeat);
        var stepsCount = panAndZoomModel.ContentViewportWidth / StepLength;
        int[] factors;
        if (stepsCount <= MAX_VISIBLE_LINES) factors = new[] {1, 2, 4, 8, 16};
        else if (subbeatsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 1, 2, 4, 8};
        else if (beatsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 0, 1, 2, 4};
        else if (wholesCount <= MAX_VISIBLE_LINES) factors = new[] {0, 0, 0, 1, 2};
        else if (selectionsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 0, 0, 0, 1};
        else factors = new[] { 0, 0, 0, 0, 0};
        _stepsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[0];
        _subbeatThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[1];
        _beatsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[2];
        _wholesThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[3];
        _sectionsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[4];
    }

    private MuzUData MuzUData => App.Current.Services.GetService<ProjectRepository>()!.ProjectModel.MuzUProject.MuzUData;
    private int StepsPerBeat = 8;
    private int StepsPerSubbeat = 2;
    private int StepsPerWhole => MuzUData.Tempo.TimeSignature.Denominator * StepsPerBeat;
    private int StepsPerSection => 4 * StepsPerWhole;
    private readonly ThicknessSharedProperty _stepsThickness = new(1);
    private readonly ThicknessSharedProperty _subbeatThickness = new(1);
    private readonly ThicknessSharedProperty _beatsThickness = new(2);
    private readonly ThicknessSharedProperty _wholesThickness = new(4);
    private readonly ThicknessSharedProperty _sectionsThickness = new(8);

    #endregion

    public void UpdateWidth()
    {
        var audioService = App.Current.Services.GetService<AudioService>()!;
        double max = PanAndZoomModel.FromMicroseconds((long)audioService.AudioDurationMicroseconds);
        var notes = App.Current.Services.GetService<SequenceListModel>()!.Notes;
        foreach (var n in notes) if (max < n.Width + n.X) max = n.Width + n.X;
        App.Current.Services.GetService<PanAndZoomModel>()!.ContentWidth = max;
    }
}
