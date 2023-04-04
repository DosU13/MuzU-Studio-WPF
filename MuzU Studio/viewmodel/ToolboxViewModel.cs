using Microsoft.Extensions.DependencyInjection;
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

    public ToolboxViewModel(SequenceListModel sequenceListModel, ProjectRepository projectRepository)
    {
        this.sequenceListModel = sequenceListModel;
        this.projectRepository = projectRepository;
    }

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
                var newX = App.Current.Services.GetService<PianoRollModel>()!
                                      .SnapToGrid(note.X, SnapAllInterval);
                var oldTime = note.Node.Time;
                note.ForceSetX(newX);
                var newTime = note.Node.Time;
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
            var list = sequence.Data.NodeList.List;
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
            sequence.Data.NodeList.List = list.Where((item,index) => !removeItems[index]).ToList();
        }
        sequenceListModel.Update();
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
        foreach(var sequence in muzuData.SequenceList.List)
        {
            foreach(var node in sequence.NodeList.List)
            {
                node.Time = (long)(node.Time * changeFactor);
                if(node.Length != null) node.Length = (long)(node.Length * changeFactor);
            }
        }
        muzuData.Tempo.BPM = changeBPMParameter;
        sequenceListModel.Update();
    }
    #endregion
}
