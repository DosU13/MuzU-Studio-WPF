using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Modeling.Diagrams;
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
        var audioService = App.Current.Services.GetService<AudioService>()!;
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
        foreach (var note in sequenceList.Notes.Where(x => x.Parent.Visible && 
                                x.X <= musicPos && musicPos <= x.X + x.Width))
        {
            double width = 50 + 50 * (((musicPos % 10 / 10.0) + note.Node.Note!.Value/128.0) % 1.0);
            Rectangle rect = new()
            {
                Width = width,
                Height = MainCanvas.ActualHeight,
                Fill = LaserBrushFrom(note.Parent.Color)
            };
            MainCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, (MainCanvas.ActualWidth - 100) * 
                (note.Node.Note!.Value/128.0) + 50 - width / 2);
        }
    }

    private static LinearGradientBrush LaserBrushFrom(Color color)
    {
        var hslColor = HslColor.FromRgbColor(System.Drawing.Color.FromArgb(0xFF, color.R,color.G,color.B));
        var hue = hslColor.Hue;
        HslColor lightness45 = new(hue, 255, 114);
        var drawing45 = lightness45.ToRgbColor();
        Color lightness45Color = Color.FromRgb(drawing45.R, drawing45.G, drawing45.B);
        HslColor lightness63 = new(hue, 255, 160);
        var drawing63 = lightness63.ToRgbColor();
        Color lightness63Color = Color.FromRgb(drawing63.R, drawing63.G, drawing63.B);
        Color transparent = Color.FromArgb(0x00, color.R, color.G, color.B);
        var laserBrush = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(transparent, 0),
                new GradientStop(lightness45Color, 0.40),
                new GradientStop(lightness63Color, 0.45),
                new GradientStop(Colors.White, 0.48),
                new GradientStop(Colors.White, 0.52),
                new GradientStop(lightness63Color, 0.55),
                new GradientStop(lightness45Color, 0.60),
                new GradientStop(transparent, 1)
            },
            new System.Windows.Point(0, 0.5),
            new System.Windows.Point(1, 0.5)
        );

        return laserBrush;
    }
}
