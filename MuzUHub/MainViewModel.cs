using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MuzUHub;

class MainViewModel : BindableBase
{
    private ObservableCollection<string> _projectUrls;
    private ICommand _deleteItemCommand;
    private ICommand _openProjectCommand;

    public ObservableCollection<string> ProjectUrls
    {
        get
        {
            if (_projectUrls == null)
            {
                _projectUrls = new ObservableCollection<string>();
                foreach (var x in Properties.Settings.Default.ProjectsURLs)
                    if (x != null) _projectUrls.Add(x);
                _projectUrls.CollectionChanged += _projectUrls_CollectionChanged;
            }
            return _projectUrls;
        }
    }

    private void _projectUrls_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Properties.Settings.Default.ProjectsURLs = new StringCollection();
        Properties.Settings.Default.ProjectsURLs.AddRange(_projectUrls.ToArray());
        Properties.Settings.Default.Save();
    }

    internal void OpenExistingProject(string URL)
    {
        ProjectUrls.Add(URL);
        OpenProject(URL);
    }

    public ICommand DeleteItemCommand
    {
        get
        {
            _deleteItemCommand ??= new RelayCommand(x => DeleteItem(x));
            return _deleteItemCommand;
        }
    }

    private void DeleteItem(object item)
    {
        if (item is string url) ProjectUrls.Remove(url);
    }

    public ICommand OpenProjectCommand
    {
        get
        {
            _openProjectCommand ??= new RelayCommand(x => OpenProject(x));
            return _openProjectCommand;
        }
    }

    private void OpenProject(object item)
    {
        if (item is string url)
        {
            Application.Current.Shutdown();
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            appPath = appPath.Substring(0, appPath.LastIndexOf('\\')+1) + "MuzU Studio.exe";
            Process.Start(appPath, new string[] { url });
        }
    }

    internal void OpenEmtpyProject()
    {
        throw new NotImplementedException();
    }

    internal void NewProjectFromMidi(string fileName)
    {
        throw new NotImplementedException();
    }
}
