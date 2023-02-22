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

internal class ProjectRepository
{
    public static ProjectRepository InitDefault()
    {
        var projectPath = MuzU_Studio.Properties.Settings.Default.LastProjectURL;
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

    private ProjectRepository(MuzUProject muzUProject, string? projectPath = null)
    {
        MuzUProject = muzUProject;
        ProjectPath = projectPath;
    }

    public MuzUProject MuzUProject { get; set; }

    public string? ProjectPath { get; set; }

    internal void AddProjectToSettings(string path)
    {
        var _projectUrls = MuzUHub.Properties.Settings.Default.ProjectsURLs!;
        _projectUrls.Add(path);
        MuzUHub.Properties.Settings.Default.ProjectsURLs = _projectUrls;
        MuzUHub.Properties.Settings.Default.Save();
    }
}
