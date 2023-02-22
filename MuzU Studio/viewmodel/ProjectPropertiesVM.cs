using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel;

public class ProjectPropertiesVM: BindableBase
{
    private MuzUData _muzUData;
    private MuzUData MuzUData => _muzUData;

    public ProjectPropertiesVM(ProjectRepository projectModel)
    {
        _muzUData = projectModel.MuzUProject.MuzUData;
    }

    public string ProjectName { 
        get => MuzUData.Identity.Name;
        set => MuzUData.Identity.Name = value;}

    public string ProjectCreator
    {
        get => MuzUData.Identity.Creator;
        set => MuzUData.Identity.Creator = value;
    }

    public string ProjectDescription
    {
        get => MuzUData.Identity.Description; 
        set => MuzUData.Identity.Description = value;
    }

    public string MusicName
    {
        get => MuzUData.Music.Name;
        set => MuzUData.Music.Name = value;
    }

    public string MusicAuthor
    {
        get => MuzUData.Music.Author;
        set => MuzUData.Music.Author = value;
    }

    public string MusicVersion
    {
        get => MuzUData.Music.Version;
        set => MuzUData.Music.Version = value;
    }

    public string MusicLocalPath
    {
        get => MuzUData.MusicLocal.MusicPath;
        set { MuzUData.MusicLocal.MusicPath = value;
            App.Current.Services.GetService<AudioService>().UpdateAudio(value);
            OnPropertyChanged();}
    }

    public long MusicLocalOffsetMicroseconds
    {
        get => MuzUData.MusicLocal.MusicOffsetMicroseconds; 
        set => MuzUData.MusicLocal.MusicOffsetMicroseconds = value;
    }

    public double TempoBPM
    {
        get => MuzUData.Tempo.BPM;
        set => MuzUData.Tempo.BPM = value;
    }

    public int TempoTimeSignNumerator
    {
        get => MuzUData.Tempo.TimeSignature.Numerator;
        set => MuzUData.Tempo.TimeSignature.Numerator = value;
    }

    public int TempoTimeSignDenominator
    {
        get => MuzUData.Tempo.TimeSignature.Denominator;
        set => MuzUData.Tempo.TimeSignature.Denominator = value;
    }
}
