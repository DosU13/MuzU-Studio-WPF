using Melanchall.DryWetMidi.Core;
using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.Model;
using MuzU_Studio.util;
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
using System.Windows.Shapes;

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
    public IServiceProvider Services => ServiceManager.Services;

    public ServiceManager ServiceManager { get; } = new ServiceManager();

    internal void NewProject()
    {
        ServiceManager.ConfigureServices(ProjectRepository.InitNew());
    }
    
    internal async void NewProjectFromMIDI(string fileName)
    {
        ServiceManager.ConfigureServices(
            await ProjectRepository.InitFromMidiFile(fileName));
    }

    internal async void OpenMuzUProject(string fileName)
    {
        ServiceManager.ConfigureServices(
            await ProjectRepository.InitFromMuzUFile(fileName));
    }

    private async void Application_Startup(object sender, StartupEventArgs e)
    {
        var args = e.Args;
        //args = new[] {"MuzU_FILE", "D:\\Desktop\\Time to Share.muzu"};
        //args = new[] {"MIDI_FILE", "D:\\Desktop\\Piano Hero 019 - Gemini - Time To Share.mid"};
        if (args.Length == 0)
        {
            ServiceManager.ConfigureServices(await ProjectRepository.InitDefault());
            return;
        }
        if(Enum.TryParse(args[0], out MuzUStudio_ArgType argType))
        {
            ProjectRepository projectRepository = argType switch
            {
                MuzUStudio_ArgType.MuzU_FILE => await ProjectRepository.InitFromMuzUFile(args[1]),
                MuzUStudio_ArgType.MIDI_FILE => await ProjectRepository.InitFromMidiFile(args[1]),
                MuzUStudio_ArgType.NEW_PROJECT => ProjectRepository.InitNew(),
                _ => throw new Exception(),
            };
            ServiceManager.ConfigureServices(projectRepository);
            return;
        }
        MessageBox.Show($"Application started with not proper arguments: {string.Join(' ', args)}");
    }
}
