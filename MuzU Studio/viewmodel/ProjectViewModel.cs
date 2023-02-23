using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzUStandard;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace MuzU_Studio.viewmodel;

public class ProjectViewModel: BindableBase
{
    private ProjectRepository projectRepository;

    public ProjectViewModel(ProjectRepository projectModel)
    {
        this.projectRepository = projectModel;
    }

    public MuzUProject MuzUProject => projectRepository.MuzUProject;

    public string? ProjectPath
    {
        get => projectRepository.ProjectPath;
        set
        {
            projectRepository.ProjectPath = value;
            OnPropertyChanged(nameof(ProjectPath));
            OnPropertyChanged(nameof(ExistProjectPath));
        }
    }
    public bool ExistProjectPath => ProjectPath != null;

    public string ProjectName { get => MuzUProject.MuzUData.Identity.Name??""; }

    public async Task<bool> SaveToFile(string filePath)
    {
        using (var stream = File.Create(filePath))
        {
            bool res = false;
            await Task.Factory.StartNew(delegate
            {
                MuzUProject.Save(stream);
                res = true;
            });
            stream.Close();
            return res;
        }
    }

    public Task<bool> SaveProject()
    {
        return SaveToFile(ProjectPath);
    }

    internal void NotifyBindings()
    {
        OnPropertyChanged(nameof(ProjectName));
        string audioPath = MuzUProject.MuzUData.MusicLocal.MusicPath;
        App.Current.Services.GetService<AudioService>()?.UpdateAudio(audioPath);
    }

    internal void ProjectName_Changed()
    {
        throw new NotImplementedException();
    }
}
