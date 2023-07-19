using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzUStandard;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
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

    public MuzUProject MuzUProject => projectRepository.ProjectModel.MuzUProject;

    public string? ProjectPath
    {
        get => projectRepository.ProjectPath;
        set
        {
            projectRepository.ProjectPath = value;
            OnPropertyChanged(nameof(ProjectPath));
            OnPropertyChanged(nameof(ProjectPathExists));
        }
    }
    public bool ProjectPathExists => ProjectPath != null;
    public bool ProjectExists => projectRepository.ProjectExists;

    public string ProjectName { get => MuzUProject.MuzUData.Identity.Name; }

    public async Task<bool> SaveToFile(string filePath)
    {
        try
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
        }catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }

    public Task<bool> SaveProject()
    {
        if(ProjectPath == null) throw new ArgumentNullException(nameof(ProjectPath));
        return SaveToFile(ProjectPath);
    }

    internal void ProjectProperties_Changed()
    {
        OnPropertyChanged(nameof(ProjectName));
        string audioPath = MuzUProject.MuzUData.MusicLocal.MusicPath;
        App.Current.Services.GetService<AudioService>()?.UpdateAudio(audioPath);
    }
}
