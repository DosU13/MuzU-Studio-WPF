using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel;

public class ProjectPropertiesVM: BindableBase
{
    private ProjectRepository projectRepository;
    private MuzUData MuzUData => projectRepository.MuzUProject.MuzUData;

    public ProjectPropertiesVM(ProjectRepository projectRepository)
    {
        this.projectRepository = projectRepository;
    }

    public string ProjectName
    {
        get => MuzUData.Identity.Name;
        set
        {
            MuzUData.Identity.Name = value;
            App.Current.Services.GetService<ProjectViewModel>()!.ProjectName_Changed();
        }
    }

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
        set {
            if (File.Exists(value) == false) throw new FileNotFoundException(value);
            MuzUData.MusicLocal.MusicPath = value;
            App.Current.Services.GetService<AudioService>()!.UpdateAudio(value);
            MusicTempoChanged();
            OnPropertyChanged();
        }
    }

    public long MusicLocalOffsetMicroseconds
    {
        get => MuzUData.MusicLocal.MusicOffsetMicroseconds;
        set
        {
            MuzUData.MusicLocal.MusicOffsetMicroseconds = value;
            MusicTempoChanged();
        }
    }

    public double TempoBPM
    {
        get => MuzUData.Tempo.BPM;
        set {
            MuzUData.Tempo.BPM = value;
            MusicTempoChanged();
        }
    }

    public int TempoTimeSignNumerator
    {
        get => MuzUData.Tempo.TimeSignature.Numerator;
        set {
            MuzUData.Tempo.TimeSignature.Numerator = value;
            MusicTempoChanged();
        }
    }

    public int TempoTimeSignDenominator
    {
        get => MuzUData.Tempo.TimeSignature.Denominator;
        set {
            MuzUData.Tempo.TimeSignature.Denominator = value;
            MusicTempoChanged();
        }
    }

    private void MusicTempoChanged()
    {
        throw new NotImplementedException();
    }
}
