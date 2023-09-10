using Melanchall.DryWetMidi.MusicTheory;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzU_Studio.viewmodel.shared_property;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MuzU_Studio.model.PianoRollModel;
using Point = System.Windows.Point;

namespace MuzU_Studio.viewmodel;

internal class PianoRollViewModel: BindableBase
{
    private PianoRollModel pianoRollModel;
    private SequenceListModel sequenceModel;
    private PanAndZoomModel panAndZoomModel;
    private AudioService audioService;

    public PianoRollViewModel(PianoRollModel pianoRollModel, SequenceListModel sequenceModel, 
        PanAndZoomModel panAndZoomModel, AudioService audioService)
    {
        this.pianoRollModel = pianoRollModel;
        this.sequenceModel = sequenceModel;
        this.panAndZoomModel = panAndZoomModel;
        this.audioService = audioService;
        panAndZoomModel.PropertyChanged += PanAndZoomModel_PropertyChanged;
        audioService.PropertyChanged += AudioService_PropertyChanged;
    }

    private void AudioService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == AudioService.Nameof_PlayheadPosition)
            OnPropertyChanged(nameof(PlayheadThumbLeft));
    }

    private void PanAndZoomModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == PanAndZoomModel.Nameof_ContentWidth)
            OnPropertyChanged(nameof(PlayheadWidthInOverview));
        if (e.PropertyName == PanAndZoomModel.Nameof_ContentViewportWidth)
        {
            OnPropertyChanged(nameof(PlayheadWidthInPianoRoll));
            OnPropertyChanged(nameof(PlayheadThumbWidth));
            OnPropertyChanged(nameof(PlayheadThumbLeft));
        }
    }

    internal void AddNote(NoteViewModel noteVM)
    {
        if (sequenceModel.SelectedSequence is null) return;
        noteVM.Parent = sequenceModel.SelectedSequence;
        Notes.Add(noteVM);
    }

    internal void AddNote(Point point, double width)
    {
        if (sequenceModel.SelectedSequence is null) return;
        Node newNode = new();
        NoteViewModel newNote = new(newNode, sequenceModel.SelectedSequence)
        {
            X = pianoRollModel.SnapToGridByFloor(point.X),
            Y = (int)(point.Y / PanAndZoomModel.NOTE_HEIGHT)*PanAndZoomModel.NOTE_HEIGHT,
            Width = width
        };
        Notes.Add(newNote);
    }

    public PanAndZoomModel PanAndZoomModel => panAndZoomModel;

    public PianoRollModel PianoRollModel => pianoRollModel;

    public ObservableCollection<SequenceViewModel> Sequences => sequenceModel.Sequences;
    public ObservableCollection<NoteViewModel> Notes => sequenceModel.Notes;
    public AudioService AudioService => audioService;

    public double PlayheadWidthInOverview => PanAndZoomModel.ContentWidthUnit * 8;

    public double PlayheadWidthInPianoRoll => PanAndZoomModel.ContentViewportWidthUnit * 8;
    public double PlayheadThumbWidth => PanAndZoomModel.ContentViewportWidthUnit * 128;
    public double PlayheadThumbLeft
    {
        get => AudioService.PlayheadPosition - PlayheadThumbWidth / 2;
        set
        {
            double newPlayheadPosition = value + PlayheadThumbWidth / 2;
            if (AudioService.PlayheadPosition == newPlayheadPosition) return;
            AudioService.PlayheadPosition = newPlayheadPosition;
        }
    }

    public double BeatLength => PianoRollModel.BeatLength;

    public bool EditingLocked => PianoRollModel.EditingLocked;
}
