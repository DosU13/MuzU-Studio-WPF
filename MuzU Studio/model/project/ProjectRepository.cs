using MuzU_Studio.Model;
using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MuzU_Studio.model;

public class ProjectRepository
{
    #region static Init methods
    public static async Task<ProjectRepository> InitDefault()
    {
        var projectPath = Properties.Settings.Default.LastProjectPath;
        return string.IsNullOrEmpty(projectPath) ? InitEmpty() : await InitFromMuzUFile(projectPath);
    }

    public static ProjectRepository InitEmpty()
    {
        return new ProjectRepository(new EmptyProjectModel());
    }

    public static ProjectRepository InitNew()
    {
        return new ProjectRepository(new ProjectModel(new MuzUProject()));
    }

    public static async Task<ProjectRepository> InitFromMuzUFile(string path)
    {
        try
        {
            using var stream = File.OpenText(path);
            var muzUProject = await Task.FromResult(new MuzUProject(stream.BaseStream));
            if (muzUProject.MuzUData == null) throw new Exception("Couldn't read MuzU file");
            stream.Close();
            return new ProjectRepository(new ProjectModel(muzUProject), path);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return InitEmpty();
        }
    }

    public static async Task<ProjectRepository> InitFromMidiFile(string path)
    {
        try
        {
            using var stream = File.OpenRead(path);
            var muzUProject = await Task.FromResult(MidiImporter.Import(stream, Path.GetFileName(path)));
            stream.Close();
            return new ProjectRepository(new ProjectModel(muzUProject));
        }
        catch (Exception)
        {
            return InitEmpty();
        }
    }
    #endregion

    private ProjectRepository(IProjectModel muzUProject, string? projectPath = null)
    {
        ProjectModel = muzUProject;
        ProjectPath = projectPath;
    }

    public IProjectModel ProjectModel { get; }

    private string? projectPath;
    public string? ProjectPath {  
        get { return projectPath; } 
        set {
            if(value != null && !File.Exists(value)) throw new FileNotFoundException(value);
            projectPath = value;
            SetLastProjectPath(value);
        } 
    }

    /// <summary>
    /// Updates MuzU Studio User Settings
    /// </summary>
    /// <param name="path"></param>
    private void SetLastProjectPath(string? path)
    {
        if(!ProjectExists) return;
        if (Properties.Settings.Default.LastProjectPath == path) return;
        Properties.Settings.Default.LastProjectPath = path;
        Properties.Settings.Default.Save();
    }

    internal bool ProjectExists => ProjectModel is not EmptyProjectModel;
}
