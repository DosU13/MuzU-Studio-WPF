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
    private ProjectRepository projectModel;
    internal VMRefreshableView VMRefreshableView;

    public ProjectViewModel(ProjectRepository projectModel)
    {
        this.projectModel = projectModel;
    }

    public MuzUProject MuzUProject { get => projectModel.MuzUProject; set
        {
            projectModel.MuzUProject = value;
            App.Current.Services.GetService<SequenceListModel>().Update(projectModel);
            App.Current.Services.GetService<AudioService>().UpdateAudio(
                projectModel.MuzUProject.MuzUData.MusicLocal.MusicPath);
            OnPropertyChanged(string.Empty);
            VMRefreshableView?.RefreshViewModel();
        } }

    public string ProjectPath { get => projectModel.ProjectPath; set => projectModel.ProjectPath = value; }
    public bool ExistProject => MuzUProject != null;
    public bool ExistProjectPath => ProjectPath != null;

    public string ProjectName { get => MuzUProject?.MuzUData.Identity.Name??""; }

    public void NewEmptyProject()
    {
        MuzUProject = new MuzUProject();
        ProjectPath = null;
    }

    public async Task<bool> SaveToFile(string filePath)
    {
        using (var stream = File.Create(filePath))
        {
            bool res = false;
            await Task.Factory.StartNew(delegate
            {
                MuzUProject.Save(stream);
                if (!ExistProjectPath) ProjectPath = filePath;
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
    }

    internal async Task<bool> LoadFromFile(string filePath)
    {
        using (var stream = File.OpenText(filePath))
        {
            bool res = false;
            await Task.Factory.StartNew(delegate
            {
                MuzUProject = new MuzUProject(stream);
                ProjectPath = filePath;
                res = true;
            });
            stream.Close();
            return res;
        }
    }
}
