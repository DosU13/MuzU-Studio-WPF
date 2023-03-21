using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.viewmodel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MuzU_Studio.view;

public sealed partial class Visualizer : UserControl
{
    public Visualizer()
    {
        this.InitializeComponent();
        var audioService = App.Current.Services.GetService<AudioService>();
        audioService.PropertyChanged += AudioService_PropertyChanged;
    }

    private void AudioService_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is not AudioService audioService) return;
        if(e.PropertyName == AudioService.Nameof_IsPlaying)
        {
            if(audioService.IsPlaying)
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            else
                CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }
    }

    private void CompositionTarget_Rendering(object? sender, EventArgs e)
    {
        var sequenceList = App.Current.Services.GetService<SequenceListModel>()!;
        var musicPos = App.Current.Services.GetService<AudioService>()!.PlayheadPosition;
        MainCanvas.Children.Clear();
        foreach (var note in sequenceList.Notes.Where(x => x.X >= musicPos && x.X + x.Width >= musicPos))
        {
            double width = 50 + 50 * (((musicPos % 10 / 10.0) + note.Node.Note!.Value/128.0) % 1.0);
            Rectangle rect = new()
            {
                Width = width,
                Height = MainCanvas.ActualHeight,
                Fill = Resources["Laser"] as LinearGradientBrush
            };
            MainCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, (MainCanvas.ActualWidth - 100) * 
                (note.Node.Note!.Value/128.0) + 50 - width / 2);
        }
    }

    private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
    }
}
