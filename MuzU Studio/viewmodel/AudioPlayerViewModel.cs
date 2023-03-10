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

internal class AudioPlayerViewModel
{
    private readonly AudioService audioService;

    public AudioPlayerViewModel(AudioService audioService)
    {
        this.audioService = audioService;
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
}
