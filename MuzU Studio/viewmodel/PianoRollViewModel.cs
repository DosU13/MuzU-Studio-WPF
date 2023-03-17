using Melanchall.DryWetMidi.MusicTheory;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel;

internal class PianoRollViewModel
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
    }

    public PanAndZoomModel PanAndZoomModel => panAndZoomModel;

    public PianoRollModel PianoRollModel => pianoRollModel;

    public ObservableCollection<SequenceViewModel> Sequences => sequenceModel.Sequences;
    public ObservableCollection<NoteViewModel> Notes => sequenceModel.Notes;
    public AudioService AudioService => audioService;
}
