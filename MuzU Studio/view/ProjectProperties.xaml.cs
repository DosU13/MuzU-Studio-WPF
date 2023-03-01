using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuzU_Studio.view;

/// <summary>
/// Interaction logic for ProjectProperties.xaml
/// </summary>
public partial class ProjectProperties : Window
{
    public ProjectProperties()
    {
        InitializeComponent();
        App.Current.ServiceManager.ServiceUpdated += ServiceManager_ServiceUpdated;
    }

    private void ServiceManager_ServiceUpdated(IServiceProvider serviceProvider)
    {
        Application.Current.Dispatcher.Invoke(() =>
            DataContext = serviceProvider.GetService<ProjectPropertiesVM>());
    }

    private ProjectPropertiesVM projectPropertiesVM => (ProjectPropertiesVM)DataContext;

    private void MusicLocalPath_Click(object sender, RoutedEventArgs e)
    {
        var picker = new OpenFileDialog();
        picker.Filter = "Audio (*.mp3,*.acc,*wma)|*.acc;*.mp3;*.wma|All Files (*.*)|*.*";
        if (picker.ShowDialog() ?? false)
        {
            projectPropertiesVM.MusicLocalPath = picker.FileName;
        }
    }
}
