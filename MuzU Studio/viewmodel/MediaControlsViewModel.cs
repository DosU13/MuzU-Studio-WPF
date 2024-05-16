using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using static MuzU_Studio.model.PianoRollModel;

namespace MuzU_Studio.viewmodel;

internal class MediaControlsViewModel : BindableBase
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

    public double SnapToGridInterval
    {
        get => pianoRollModel.SnapToGridInterval;
        set => pianoRollModel.SnapToGridInterval = value;
    }

    public bool EditingLocked
    {
        get => pianoRollModel.EditingLocked;
        set
        {
            pianoRollModel.EditingLocked = value;
            OnPropertyChanged();
        }
    }

    public bool RecordEnabled
    {
        get => pianoRollModel.RecordEnabled;
        set
        {
            pianoRollModel.RecordEnabled = value;
            OnPropertyChanged();
        }
    }

    private ICommand? toggleEditableCommand;

    public ICommand ToggleEditableCommand =>
            toggleEditableCommand ??= new RelayCommand(param => EditingLocked = !EditingLocked);

    public ICommand? toggleRecordCommand;
    public ICommand ToggleRecordCommand =>
        toggleRecordCommand ??= new RelayCommand(param => RecordEnabled = !RecordEnabled);
}
