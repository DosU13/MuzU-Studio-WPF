using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.viewmodel;
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
/// Interaction logic for MediaControls.xaml
/// </summary>
public partial class MediaControls : UserControl
{
    public MediaControls()
    {
        InitializeComponent();
        App.Current.ServiceManager.ServiceUpdated += ServiceManager_ServiceUpdated;
    }

    private void ServiceManager_ServiceUpdated(IServiceProvider serviceProvider)
    {
        Application.Current.Dispatcher.Invoke(() =>
            DataContext = serviceProvider.GetService<AudioPlayerViewModel>());
    }
}
