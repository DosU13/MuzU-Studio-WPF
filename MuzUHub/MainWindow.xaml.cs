using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace MuzUHub;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private MainViewModel MainViewModel => (DataContext as MainViewModel)!;

    private void NewProject_Click(object sender, RoutedEventArgs e)
    {
        MainViewModel.OpenEmtpyProject();
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
        var picker = new OpenFileDialog
        {
            DefaultExt = ".muzu",
            Filter = "MuzU file (*.muzu)|*.muzu"
        };
        if (picker.ShowDialog() ?? false)
        {
            MainViewModel.OpenExistingProject(picker.FileName);
        }
    }

    private void NewFromMidi_Click(object sender, RoutedEventArgs e)
    {
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
            MainViewModel.NewProjectFromMidi(picker.FileName);
        }
    }
}
