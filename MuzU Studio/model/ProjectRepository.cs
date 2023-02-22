using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MuzU_Studio.model;

public class ProjectRepository
{
    public MuzUProject MuzUProject { get; set; }

    public string ProjectPath { get; set; } = null;

    internal void AddProjectToSettings(string URL)
    {
        var _projectUrls = MuzUHub.Properties.Settings.Default.ProjectsURLs!;
        _projectUrls.Add(URL);
        MuzUHub.Properties.Settings.Default.ProjectsURLs = _projectUrls;
        MuzUHub.Properties.Settings.Default.Save();
    }
}
