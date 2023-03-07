using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MuzU_Studio.helper;
using MuzU_Studio.model;
using MuzU_Studio.Model;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzU_Studio.viewmodel;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MuzU_Studio;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent(); 
        App.Current.ServiceManager.ServiceUpdated += ServiceManager_ServiceUpdated;
    }

    private void ServiceManager_ServiceUpdated(IServiceProvider serviceProvider)
    {
        Application.Current.Dispatcher.Invoke(() =>
            DataContext = serviceProvider.GetService<ProjectViewModel>());
    }

    private ProjectViewModel ProjectVM => (ProjectViewModel)DataContext;

    private void OpenMuzUHub_Click(object sender, RoutedEventArgs e)
    {
        MuzUHubRunner.Run();
    } 

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectVM.ProjectPathExists) await ProjectVM.SaveProject();
        else await SaveWithFilePicker();
    }

    private async void SaveAs_Click(object sender, RoutedEventArgs e)
    {
        await SaveWithFilePicker();
    }

    private async Task<bool> SaveWithFilePicker()
    {
        var picker = new SaveFileDialog();
        picker.DefaultExt = ".muzu";
        picker.Filter = "MuzU file (*.muzu)|*.muzu";
        if (picker.ShowDialog() ?? false)
        {
            if (await ProjectVM.SaveToFile(picker.FileName))
            {
                ProjectVM.ProjectPath = picker.FileName;
                return true;
            }
            else MessageBox.Show("Couldn't save the project");
        } 
        return false;
    }

    private ProjectProperties? projectProperties;
    private void ProjectProperties_Click(object sender, RoutedEventArgs e)
    {
        if (projectProperties == null)
        {
            projectProperties = new ProjectProperties();
            projectProperties.Show();
            projectProperties.Closed += (x, y) => { 
                projectProperties = null;
                ProjectVM.ProjectProperties_Changed();
            };
        }
        else projectProperties.Focus();
    }

    private async void Window_Closing(object sender, CancelEventArgs e)
    {
        if(!await SaveWorkDialog()) e.Cancel = true;
    }

    private async Task<bool> SaveWorkDialog()
    {
        if (!ProjectVM.ProjectExists) return true;
        var resp = MessageBox.Show("Do you want to save changes?", "MuzU Studio", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        if (resp == MessageBoxResult.Yes)
        {
            if (ProjectVM.ProjectPathExists)
            {
                if (await ProjectVM.SaveProject() == false)
                {
                    MessageBox.Show("Unable to save Project");
                    return false;
                }
            }
            else if (await SaveWithFilePicker() == false) return false;
        }
        else if (resp == MessageBoxResult.Cancel) return false;
        return true;
    }

    private async void NewProject_Click(object sender, RoutedEventArgs e)
    {
        if(await SaveWorkDialog()) App.Current.NewProject();
    }

    private async void NewProjectFromMIDI_Click(object sender, RoutedEventArgs e)
    {
        if (!await SaveWorkDialog()) return;
        var picker = new OpenFileDialog
        {
#if DEBUG
            InitialDirectory = "D:\\DosU\\Documents\\midi",
#endif
            DefaultExt = ".mid",
            Filter = "Midi file (*.mid)|*.mid"
        };
        if (picker.ShowDialog() ?? false)
        {
            App.Current.NewProjectFromMIDI(picker.FileName);
        }
    }

    private async void Open_Click(object sender, RoutedEventArgs e)
    {
        if(!await SaveWorkDialog()) return;
        var picker = new OpenFileDialog
        {
            DefaultExt = ".muzu",
            Filter = "MuzU file (*.muzu)|*.muzu"
        };
        if (picker.ShowDialog() ?? false)
        {
            App.Current.OpenMuzUProject(picker.FileName);
        }
    }
}
