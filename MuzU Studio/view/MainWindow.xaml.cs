using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MuzU_Studio.model;
using MuzU_Studio.Model;
using MuzU_Studio.util;
using MuzU_Studio.view;
using MuzU_Studio.viewmodel;
using MuzUStandard;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MuzU_Studio;

public sealed partial class MainWindow : Window, VMRefreshableView
{
    public MainWindow()
    {
        InitializeComponent();
        ProjectVM.VMRefreshableView = this;
        var x = this.DataContext as ProjectViewModel;
        var b = ProjectVM == x;
        //SequenceEdit.IRefresh = this;
        //_ = LoadLocalSettingsAsync();
        //SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += OnWindowClose;
    }

    private ProjectViewModel ProjectVM => this.DataContext as ProjectViewModel;

    public void RefreshViewModel()
    {
        PianoRoll.RefreshViewModel();
        PianoOverview.RefreshViewModel();
    }

    //public MuzUProject _project;
    //public MuzUProject Project
    //{
    //    get => _project;
    //    set
    //    {
    //        _project = value;
    //        MainVM = new ProjectVM(Project);
    //        SweetPotato.MainVM = MainVM;
    //        Visualizer.MainVM = MainVM;
    //        Visualizer.MusicPosShareData = SweetPotato;
    //        //SequenceEdit.BeatLengthShareData = this;

    //        SweetPotato.SequenceVM = MainVM.SelectedSequence;
    //        SequenceEdit.SequenceVM = MainVM.SelectedSequence;
    //        Bindings.Update();
    //    }
    //}
    //private string WindowTitle => projectFile?.Name ?? "";
    //private void SequenceSelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    SweetPotato.SequenceVM = MainVM.SelectedSequence;
    //    SequenceEdit.SequenceVM = MainVM.SelectedSequence;
    //    Bindings.Update();
    //}

    private async void NewEmpty_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectVM.ExistProject) if (!(await SaveWorkDialog())) return;
        ProjectVM.NewEmptyProject();
    }

    private async void NewMidi_Click(object sender, RoutedEventArgs e)
    {
        await PickMidiAndImport();
        //if (existProject) if (!(await SaveWorkDialog())) return;
        //await PickMidiAndImport(true, true);
        //projectFile = null;
    }

    private async void Open_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectVM.ExistProject) if (!(await SaveWorkDialog())) return;
        await LoadWithFilePicker();
    } 

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        if (ProjectVM.ExistProjectPath) await ProjectVM.SaveProject();
        else await SaveWithFilePicker();
    }

    private async void SaveAs_Click(object sender, RoutedEventArgs e)
    {
        await SaveWithFilePicker();
    }

    //private void ListViewModelAddNew_Click(object sender, RoutedEventArgs e)
    //{
    //    if(MainVM!=null) MainVM.AddNewSequence();
    //}

    private async Task<bool> SaveWorkDialog()
    {
        var resp = MessageBox.Show("Do you want to save changes?", "MuzU Studio",  MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
        if (resp == MessageBoxResult.Yes)
        {
            if (ProjectVM.ExistProjectPath) if (await ProjectVM.SaveProject() == false)
                {
                    MessageBox.Show("Unable to save Project");
                    return false;
                }
                else if (await SaveWithFilePicker() == false) return false;
        }
        else if(resp == MessageBoxResult.Cancel) return false;
        return true;
    }

    private async Task<bool> SaveWithFilePicker()
    {
        var picker = new SaveFileDialog();
        picker.DefaultExt = ".muzu";
        picker.Filter = "MuzU file (*.muzu)|*.muzu";
        if (picker.ShowDialog() ?? false)
        {
            if(await ProjectVM.SaveToFile(picker.FileName)) return true;
            else MessageBox.Show("Couldn't save the project");
        } 
        return false;
    }

    private ProjectProperties projectProperties;
    private void ProjectProperties_Click(object sender, RoutedEventArgs e)
    {
        if (projectProperties == null)
        {
            projectProperties = new ProjectProperties(ProjectVM.MuzUProject.MuzUData);
            projectProperties.Show();
            projectProperties.Closed += (x, y) => { projectProperties = null; ProjectVM.NotifyBindings(); };
        }
        else projectProperties.Focus();
    }

    private async Task<bool> LoadWithFilePicker()
    {
        var picker = new OpenFileDialog();
        picker.DefaultExt = ".muzu";
        picker.Filter = "MuzU file (*.muzu)|*.muzu";
        if (picker.ShowDialog() ?? false)
        {
            if (await ProjectVM.LoadFromFile(picker.FileName)) return true;
            else MessageBox.Show("Couldn't save the project");
        }
        return false;
    }

    private async Task<bool> PickMidiAndImport()
    {
        var picker = new OpenFileDialog();
#if DEBUG
        picker.InitialDirectory = "D:\\DosU\\Documents\\midi";
#endif
        picker.DefaultExt = ".mid";
        picker.Filter = "Midi file (*.mid)|*.mid";
        if (picker.ShowDialog() ?? false)
        {
            MuzUProject res;
            using (var stream = File.OpenRead(picker.FileName))
            {
                res = MidiImporter.Import(stream, picker.SafeFileName);
            }
            if (res != null) ProjectVM.MuzUProject = res;
        }
        return false;
    }

    /** Save to Local Settings
    //private async void OnWindowClose(object sender, SystemNavigationCloseRequestedPreviewEventArgs args)
    //{
    //    args.Handled = true;
    //    if (existProject) if (!(await SaveWorkDialog())) return;
    //    SaveLocalSettings();
    //    App.Current.Exit();
    //}

    //private void SaveLocalSettings()
    //{
    //    if (projectFile == null) ApplicationData.Current.LocalSettings.Values["ProjectFileFutureAccessToken"] = null;
    //    else
    //    {
    //        string faToken = StorageApplicationPermissions.FutureAccessList.Add(projectFile);
    //        ApplicationData.Current.LocalSettings.Values["ProjectFileFutureAccessToken"] = faToken;
    //    }
    //}

    //private async Task LoadLocalSettingsAsync()
    //{
    //    if (ApplicationData.Current.LocalSettings.Values.ContainsKey("ProjectFileFutureAccessToken"))
    //    {
    //        string faToken = ApplicationData.Current.LocalSettings.Values["ProjectFileFutureAccessToken"].ToString();
    //        IStorageFile _projectFile = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(faToken);
    //        await LoadFromFile(_projectFile);
    //    }
    //}
    */
}
