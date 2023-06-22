using MuzU_Studio.model;
using MuzU_Studio.util;
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
        ServiceManager.ConfigureServices(await ProjectRepository.InitDefault());
    }
}
