using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MuzU_Studio.viewmodel;

internal class MediaControlsViewModel
{
    private readonly AudioService audioService;
    private readonly PianoRollModel pianoRollModel;

    public MediaControlsViewModel(AudioService audioService, PianoRollModel pianoRollModel)
    {
        this.audioService = audioService;
        this.pianoRollModel = pianoRollModel;
    }

    private ICommand? playPauseCommand;
    public ICommand PlayPauseCommand
    {
        get
        {
            playPauseCommand ??= new RelayCommand(param => audioService.PlayPause());
            return playPauseCommand;
        }
    }

    public string PlayPauseStr => audioService.IsPlaying ? "▶️" : "||";

    public double BeatSnapToGrid
    {
        get => pianoRollModel.BeatSnapToGrid;
        set => pianoRollModel.BeatSnapToGrid = value;
    }
}
