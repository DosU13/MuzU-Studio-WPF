using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.helper;
using MuzU_Studio.model;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MuzU_Studio.viewmodel;

internal class ToolboxViewModel : BindableBase
{
    private readonly SequenceListModel sequenceListModel;
    private readonly ProjectRepository projectRepository;
    private readonly PianoRollModel pianoRollModel;

    public ToolboxViewModel(SequenceListModel sequenceListModel, ProjectRepository projectRepository, PianoRollModel pianoRollModel)
    {
        this.sequenceListModel = sequenceListModel;
        this.projectRepository = projectRepository;
        this.pianoRollModel = pianoRollModel;

        sequenceListModel.PropertyChanged += SequenceListModel_PropertyChanged;
    }

    private void SequenceListModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == SequenceListModel.Nameof_SelectedSequence)
        {
            SelectedSequence = sequenceListModel.SelectedSequence;
        }
    }

    private SequenceViewModel? _selectedSequence;
    public SequenceViewModel? SelectedSequence
    {
        get { return _selectedSequence; }
        set { 
            if(_selectedSequence != null) _selectedSequence.PropertyChanged -= SelectedSequence_PropertyChanged;
            if (SetProperty(ref _selectedSequence, value))
            {
                OnPropertyChanged(nameof(IsThereSequenceSelected));
                OnPropertyChanged(nameof(AddRemoveLyricsBtn));
                OnPropertyChanged(nameof(LyricsEnabled));
                if(SelectedSequence != null) 
                    LyricsText = LyricsMapper.Map(SelectedSequence);
            }
            if (_selectedSequence != null) _selectedSequence.PropertyChanged += SelectedSequence_PropertyChanged;
        }
    }

    private void SelectedSequence_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == SequenceViewModel.Nameof_LyricsEnabled)
        {
            OnPropertyChanged(nameof(AddRemoveLyricsBtn));
            OnPropertyChanged(nameof(LyricsEnabled));
            if (SelectedSequence != null)
                LyricsText = LyricsMapper.Map(SelectedSequence);
        }
    }

    public bool IsThereSequenceSelected => SelectedSequence != null;

    #region Snap All
    private double snapAllInterval = 0.0;
    public double SnapAllInterval
    {
        get => snapAllInterval;
        set => snapAllInterval = value;
    }

    private ICommand? snapAllCommand;
    public ICommand SnapAllCommand
    {
        get
        {
            snapAllCommand ??= new RelayCommand(param => SnapAll());
            return snapAllCommand;
        }
    }

    public void SnapAll() {
        int count = 0;
        foreach (var note in sequenceListModel.Notes)
        {
            if (note.Parent.Visible)
            {
                var newX = pianoRollModel.SnapToGrid(note.X, SnapAllInterval);
                var oldTime = note.Data.Time;
                note.ForceSetX(newX);
                var newTime = note.Data.Time;
                if (oldTime != newTime) count++;
            }
        }
        MessageBox.Show($"{count} notes snapped");
    }
    #endregion

    #region Melodize
    private MelodizeType choosenMelodizeType = MelodizeType.LoudestThenHighest;
    public string ChoosenMelodizeType
    {
        get => EnumUtils.GetDisplayName(choosenMelodizeType); 
        set => SetProperty(ref choosenMelodizeType, EnumUtils.GetValue<MelodizeType>(value));
    }

    private readonly string[] melodizeTypes = EnumUtils.GetDisplayNames<MelodizeType>().ToArray();
    public string[] MelodizeTypes => melodizeTypes;

    public enum MelodizeType
    {
        Highest,
        Lowest,
        Loudest,
        [Description("Loudest then highest")]
        LoudestThenHighest,
        [Description("Loudest then lowest")]
        LoudestThenLowest
    }

    private ICommand? melodizeCommand;
    public ICommand MelodizeCommand
    {
        get
        {
            melodizeCommand ??= new RelayCommand(param => Melodize());
            return melodizeCommand;
        }
    }

    private void Melodize() => Melodize(choosenMelodizeType);
    private void Melodize(MelodizeType melodizeType)
    {
        if(melodizeType == MelodizeType.LoudestThenHighest)
        {
            Melodize(MelodizeType.Loudest);
            Melodize(MelodizeType.Highest);
            return;
        }
        if (melodizeType == MelodizeType.LoudestThenLowest)
        {
            Melodize(MelodizeType.Loudest);
            Melodize(MelodizeType.Lowest);
            return;
        }
        foreach (var sequence in sequenceListModel.Sequences)
        {
            if (!sequence.Visible) continue;
            var list = sequence.Data.NodeList;
            bool[] removeItems = new bool[list.Count];
            int prev = 0;
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Time == list[prev].Time)
                {
                    int compareResult =
                        melodizeType == MelodizeType.Highest ? list[i].Note!.Value.CompareTo(list[prev].Note) :
                        melodizeType == MelodizeType.Lowest ? list[prev].Note!.Value.CompareTo(list[i].Note) :
                        melodizeType == MelodizeType.Loudest ? 0 : 0;
                    if (compareResult == 0)
                    {
                        prev = i;
                    }
                    else if (compareResult > 0)
                    {
                        removeItems[prev] = true;
                        prev = i;
                    }
                    else
                    {
                        removeItems[i] = true;
                    }
                }
                else prev = i;
            }
            sequence.Data.NodeList = list.Where((item,index) => !removeItems[index]).ToList();
        }
        sequenceListModel.ReinitCollections();
    }
    #endregion

    #region Change BPM
    private double changeBPMParameter;
    public double ChangeBPMParameter { 
        get => changeBPMParameter;
        set => SetProperty(ref changeBPMParameter, value); }


    private ICommand? changeBPMCommand;
    public ICommand ChangeBPMCommand
    {
        get
        {
            changeBPMCommand ??= new RelayCommand(param => ChangeBPM());
            return changeBPMCommand;
        }
    }

    private void ChangeBPM()
    {
        var muzuData = projectRepository.ProjectModel.MuzUProject.MuzUData;
        var changeFactor = muzuData.Tempo.BPM / changeBPMParameter;
        foreach(var note in sequenceListModel.Notes)
        {
            note.ForceSetX(note.X * changeFactor);
            note.ForceSetWidth(note.Width * changeFactor);
        }
        muzuData.Tempo.BPM = changeBPMParameter;
    }
    #endregion

    #region Lyrics

    public bool LyricsEnabled => SelectedSequence?.LyricsEnabled ?? false;

    public string AddRemoveLyricsBtn
    {
        get => LyricsEnabled ? "Remove Lyrics" : "Add Lyrics";
    }

    private string _lyricsText = string.Empty;
    public string LyricsText
    {
        get => _lyricsText;
        set {
            if (SetProperty(ref _lyricsText, value))
            {
                if(SelectedSequence != null)
                    LyricsMapper.MapInto(value, SelectedSequence);
            }
        }
    }

    private ICommand? _addRemoveLyricsCommand;
    public ICommand AddRemoveLyricsCommand
    {
        get
        {
            _addRemoveLyricsCommand ??= new RelayCommand(param => AddRemoveLyrics());
            return _addRemoveLyricsCommand;
        }
    }

    private void AddRemoveLyrics()
    {
        if (SelectedSequence == null) return;
        if(LyricsEnabled)
        {
            SelectedSequence.LyricsEnabled = false;
        }
        else
        {
            SelectedSequence.LyricsEnabled = true;
        }
    }

    private ICommand? _divideByWordsCommand;
    public ICommand DivideByWordsCommand
    {
        get
        {
            _divideByWordsCommand ??= new RelayCommand(param => DivideByWords());
            return _divideByWordsCommand;
        }
    }

    private void DivideByWords()
    {
        LyricsText = LyricsMapper.DivideByWords(LyricsText);
    }

    #endregion
}
