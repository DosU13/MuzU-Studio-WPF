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
using System.Windows.Input;

namespace MuzU_Studio.model;

public class ProjectRepository
{
    #region static Init methods
    public static ProjectRepository InitDefault()
    {
        var projectPath = MuzU_Studio.Properties.Settings.Default.LastProjectPath;
        return string.IsNullOrEmpty(projectPath) ? InitNew() : InitFromMuzUFile(projectPath);
    }

    public static ProjectRepository InitNew()
    {
        return new ProjectRepository(new MuzUProject());
    }

    public static ProjectRepository InitFromMuzUFile(string path)
    {
        using (var stream = File.OpenText(path))
        {
            var muzUProject = new MuzUProject(stream);
            stream.Close();
            return new ProjectRepository(muzUProject, path);
        }
    }

    public static ProjectRepository InitFromMidiFile(string path)
    {
        using (var stream = File.OpenRead(path))
        {
            var muzUProject = MidiImporter.Import(stream, path);
            stream.Close();
            return new ProjectRepository(muzUProject, path);
        }
    }
    #endregion

    private ProjectRepository(MuzUProject muzUProject, string? projectPath = null)
    {
        MuzUProject = muzUProject;
        ProjectPath = projectPath;
    }

    public MuzUProject MuzUProject { get; }

    private string? projectPath;
    public string? ProjectPath { 
        get { return projectPath; } 
        set {
            projectPath = value;
            SetLastProjectPath(value);
            if (value != null)
            {
                if (!File.Exists(value)) throw new FileNotFoundException(value);
                AddProjectPathToRecents(value);
            }
        } 
    }

    /// <summary>
    /// Updates MuzUHub User Settings
    /// </summary>
    /// <param name="path"></param>
    private void AddProjectPathToRecents(string path)
    {
        var _projectUrls = MuzUHub.Properties.Settings.Default.ProjectsURLs!;
        _projectUrls.Add(path);
        MuzUHub.Properties.Settings.Default.ProjectsURLs = _projectUrls;
        MuzUHub.Properties.Settings.Default.Save();
    }

    /// <summary>
    /// Updates MuzU Studio User Settings
    /// </summary>
    /// <param name="path"></param>
    private void SetLastProjectPath(string? path)
    {
        Properties.Settings.Default.LastProjectPath = path;
        Properties.Settings.Default.Save();
    }
}
