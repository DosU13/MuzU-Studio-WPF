﻿using MuzU_Studio.Model;
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
        using (var stream = File.OpenText(path))
        {
            var muzUProject = await Task.FromResult(new MuzUProject(stream));
            stream.Close();
            return new ProjectRepository(new ProjectModel(muzUProject), path);
        }
    }

    public static async Task<ProjectRepository> InitFromMidiFile(string path)
    {
        using (var stream = File.OpenRead(path))
        {
            var muzUProject = await Task.FromResult(MidiImporter.Import(stream, path));
            stream.Close();
            return new ProjectRepository(new ProjectModel(muzUProject));
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
            if(value!=null) AddProjectPathToRecents(value);
        } 
    }

    /// <summary>
    /// Updates MuzUHub User Settings
    /// </summary>
    /// <param name="path"></param>
    private void AddProjectPathToRecents(string path)
    {
        var _projectUrls = MuzUHub.Properties.Settings.Default.ProjectsURLs!;
        if(_projectUrls.Contains(path)) return; 
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
        if(!ProjectExists) return;
        if (Properties.Settings.Default.LastProjectPath == path) return;
        Properties.Settings.Default.LastProjectPath = path;
        Properties.Settings.Default.Save();
    }

    internal bool ProjectExists => ProjectModel is not EmptyProjectModel;
}
