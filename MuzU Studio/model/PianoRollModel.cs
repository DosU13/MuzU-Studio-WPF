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
    private ProjectRepository _projectRepository;
    private PanAndZoomModel _panAndZoomModel;

    public PianoRollModel(ProjectRepository projectRepository, PanAndZoomModel panAndZoomModel) {
        _projectRepository = projectRepository;
        _panAndZoomModel = panAndZoomModel;
        _panAndZoomModel.PropertyChanged += PanAndZoom_PropertyChanged;
    }

    private void PanAndZoom_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not PanAndZoomModel panAndZoomModel) return;
        if (e.PropertyName == PanAndZoomModel.Nameof_ContentWidth) UpdateTimelineItems();
        else if (e.PropertyName == PanAndZoomModel.Nameof_ContentViewportWidth)
            UpdateTimelineItemThickness(panAndZoomModel);
    }

    public double BeatLength { get {
            var muzuData = _projectRepository.ProjectModel.MuzUProject.MuzUData;
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
    public double SnapToGrid(double value, double intervalInBeats) =>
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

    public bool EditingLocked = true;

    #region TimeLine
    private readonly ObservableCollection<TimelineItemViewModel> timelineItems = new();
    public ObservableCollection<TimelineItemViewModel> TimelineItems => timelineItems;

    private void UpdateTimelineItems()
    {
        // Populate the collection with PianoKeyViewModels for all 128 MIDI keys
        timelineItems.Clear();
        for (int ind = 0; ind * StepLength < _panAndZoomModel.ContentWidth; ind++)
        {
            ThicknessSharedProperty thickness;
            if (ind % StepsPerSection == 0) thickness = _sectionsThickness;
            else if (ind % StepsPerBar == 0) thickness = _barsThickness;
            else if(ind % StepsPerBeat == 0) thickness = _beatsThickness;
            else thickness = _stepsThickness;
            timelineItems.Add(new TimelineItemViewModel(ind, ind * StepLength, thickness));
        }
    }

    private const int MAX_VISIBLE_LINES = 1 << 6;
    private void UpdateTimelineItemThickness(PanAndZoomModel panAndZoomModel)
    {
        var selectionsCount = panAndZoomModel.ContentViewportWidth / (StepLength * StepsPerSection);
        var barsCount = panAndZoomModel.ContentViewportWidth / (StepLength * StepsPerBar);
        var beatsCount = panAndZoomModel.ContentViewportWidth / BeatLength;
        var stepsCount = panAndZoomModel.ContentViewportWidth / StepLength;
        int[] factors;
        if (stepsCount <= MAX_VISIBLE_LINES) factors = new[] {1, 4, 8, 16};
        else if (beatsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 1, 4, 8};
        else if (barsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 0, 1, 4};
        else if (selectionsCount <= MAX_VISIBLE_LINES) factors = new[] {0, 0, 0, 1};
        else factors = new[] { 0, 0, 0, 0, 0};
        _stepsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[0];
        _beatsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[1];
        _barsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[2];
        _sectionsThickness.Value = panAndZoomModel.ContentViewportWidthUnit * factors[3];
    }

    private MuzUData MuzUData => _projectRepository.ProjectModel.MuzUProject.MuzUData;
    private int StepsPerBeat => 16 / MuzUData.Tempo.TimeSignature.Denominator;
    private int StepsPerBar => MuzUData.Tempo.TimeSignature.Numerator * StepsPerBeat;
    private int StepsPerSection => 4 * StepsPerBar;

    private readonly ThicknessSharedProperty _stepsThickness = new(1);
    private readonly ThicknessSharedProperty _beatsThickness = new(2);
    private readonly ThicknessSharedProperty _barsThickness = new(4);
    private readonly ThicknessSharedProperty _sectionsThickness = new(8);

    #endregion
}
