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
    public EnumEditMode EditMode { 
        get => pianoRollModel.EditMode;
        set
        {
            pianoRollModel.EditMode = pianoRollModel.EditMode == value ? EnumEditMode.None : value;
            OnPropertyChanged(nameof(AddRemoveModeColor));
            OnPropertyChanged(nameof(ChangeLengthModeColor));
            OnPropertyChanged(nameof(TranslateModeColor));
        }
    }

    private Color disabledColor = Color.FromRgb(0x1c, 0x1c, 0x21); 
    private Color enabledColor = Color.FromRgb(0xff, 0x54, 0x00);
    public Color AddRemoveModeColor => EditMode == EnumEditMode.AddRemoveMode ? enabledColor : disabledColor;
    public Color ChangeLengthModeColor => EditMode == EnumEditMode.ChangeLengthMode ? enabledColor : disabledColor;
    public Color TranslateModeColor => EditMode == EnumEditMode.TranslateMode ? enabledColor : disabledColor;
    private ICommand? addRemoveModeCommand;
    public ICommand AddRemoveModeCommand =>
        addRemoveModeCommand ??= new RelayCommand(param => EditMode = EnumEditMode.AddRemoveMode);
    private ICommand? changeLengthModeCommand;
    public ICommand ChangeLengthModeCommand =>
        changeLengthModeCommand ??= new RelayCommand(param => EditMode = EnumEditMode.ChangeLengthMode);
    private ICommand? translateModeCommand;
    public ICommand TranslateModeCommand =>
        translateModeCommand ??= new RelayCommand(param => EditMode = EnumEditMode.TranslateMode);
}
