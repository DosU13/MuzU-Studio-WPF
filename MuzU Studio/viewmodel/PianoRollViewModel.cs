using Melanchall.DryWetMidi.MusicTheory;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel;

internal class PianoRollViewModel
{
    private PianoRollModel pianoRollModel;
    private SequenceListModel sequenceModel;

    public PianoRollViewModel(PianoRollModel pianoRollModel, SequenceListModel sequenceModel)
    {
        this.pianoRollModel = pianoRollModel;
        this.sequenceModel = sequenceModel;

        double max = 0;
        foreach(var s in sequenceModel.Sequences) foreach(var n in s.Notes) if(max < n.Width + n.X) max = n.Width + n.X;
        pianoRollModel.ContentWidth = max;
    }

    public PianoRollModel PianoRollModel
    {
        get { return pianoRollModel; }
        set { pianoRollModel = value; }
    }

    public SequenceListModel SequenceModel { 
        get { return sequenceModel; }
        set { sequenceModel = value; } }

}
