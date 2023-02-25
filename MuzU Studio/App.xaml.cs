using Melanchall.DryWetMidi.Core;
using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.Model;
using MuzU_Studio.viewmodel;
using MuzUHub;
using MuzUStandard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
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
    private void ConfigureServices(ProjectRepository projectRepository)
    {
        var services = new ServiceCollection();

        services.AddSingleton(projectRepository);
        // Models
        services.AddSingleton<AudioService>();
        services.AddSingleton<PianoRollModel>();
        services.AddSingleton<SequenceListModel>();
        // ViewModels
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
        var args = e.Args;
        //args = new[] {"MuzU_FILE", "D:\\Desktop\\Time to Share.muzu"};
        //args = new[] {"MIDI_FILE", "D:\\Desktop\\Piano Hero 019 - Gemini - Time To Share.mid"};
        ProjectRepository? projectRepository = null;
        if (args.Length == 0 || !Enum.TryParse(args[0], out MuzUStudio_ArgType argType))
        {
            projectRepository = ProjectRepository.InitDefault();
        }
        else
        {
            switch (argType)
            {
                case MuzUStudio_ArgType.MuzU_FILE:
                    projectRepository = ProjectRepository.InitFromMuzUFile(args[1]);
                    break;
                case MuzUStudio_ArgType.MIDI_FILE:
                    projectRepository = ProjectRepository.InitFromMidiFile(args[1]);
                    break;
                case MuzUStudio_ArgType.NEW_PROJECT:
                    projectRepository = ProjectRepository.InitNew();
                    break;
            }
        }
        ConfigureServices(projectRepository?? ProjectRepository.InitDefault());
    }
}
