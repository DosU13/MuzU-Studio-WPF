using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.Model;
using MuzU_Studio.viewmodel;
using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace MuzU_Studio;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; set; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private void ConfigureServices(MuzUProject muzUProject)
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton(new ProjectRepository() { MuzUProject = muzUProject });
        services.AddSingleton<AudioService>();
        services.AddSingleton<PianoRollModel>();
        services.AddSingleton<SequenceListModel>();

        // Viewmodels
        services.AddTransient<MediaPlayerViewModel>();
        services.AddTransient<PianoRollViewModel>();
        services.AddTransient<ProjectPropertiesVM>();
        services.AddTransient<ProjectViewModel>();
        services.AddTransient<SequenceViewModel>();
        services.AddTransient<SequenceListViewModel>();
        services.AddTransient<AudioPlayerViewModel>();

        Services = services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        string projectPath;
        if (e.Args.Length == 0)
        {
            projectPath = MuzU_Studio.Properties.Settings.Default.LastProjectURL;
            if (string.IsNullOrEmpty(projectPath)) 
            {
                ConfigureServices(new MuzUProject());
                return; 
            }

        }else projectPath = e.Args[0];
        LoadProject(projectPath);
    }

    private void LoadProject(string projectPath)
    {
        switch(Path.GetExtension(projectPath).ToLower())
        {
            case ".midi":
                InitProjectFromMidi(projectPath);
                break;
            case ".muzu":
                InitMuzUProject(projectPath);
                break;
        }
    }

    private void InitMuzUProject(string projectPath)
    {
        using (var stream = File.OpenText(projectPath))
        {
            var muzUProject = new MuzUProject(stream);
            stream.Close();
            ConfigureServices(muzUProject);
        }
    }

    private void InitProjectFromMidi(string projectPath)
    {
        using (var stream = File.OpenRead(projectPath))
        {
            var muzUProject = MidiImporter.Import(stream, projectPath);
            ConfigureServices(muzUProject);
        }
    }
}
