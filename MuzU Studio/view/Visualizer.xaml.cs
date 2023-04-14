using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Modeling.Diagrams;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.viewmodel;
using System.Diagnostics;
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
        double backFlashFactor = 0;
        foreach (var note in sequenceList.Notes.Where(x => x.Parent.Visible && 
                                x.X <= musicPos && musicPos <= x.X + x.Width))
        {
            double width = 50 + 50 * (((musicPos % 10 / 10.0) + note.Node.Note!.Value/128.0) % 1.0);
            double tempFlashFactor = Math.Max(100_000D - PanAndZoomModel.ToMicroseconds(musicPos - note.X), 0) / 100_000;
            if (backFlashFactor < tempFlashFactor) backFlashFactor = tempFlashFactor;
            Rectangle rect = new()
            {
                Width = width,
                Height = MainCanvas.ActualHeight,
                Fill = LaserBrushFrom(note.Parent.Hue)
            };
            MainCanvas.Children.Add(rect);
            Canvas.SetLeft(rect, (MainCanvas.ActualWidth - 100) * 
                (note.Node.Note!.Value/128.0) + 50 - width / 2);
        }
        MainCanvas.Background = BackBrushFrom(backFlashFactor);
    }

    private static Brush BackBrushFrom(double factor)
    {
        var defaultColor = System.Drawing.Color.FromArgb(0xFF, 0x22, 0x15, 0x31);
        var hslColor = HslColor.FromRgbColor(defaultColor);

        hslColor.Luminosity += (int)(13 * factor);

        var lightColor = hslColor.ToRgbColor();

        return new SolidColorBrush(Color.FromRgb(lightColor.R, lightColor.G, lightColor.B));
    }


    private static LinearGradientBrush LaserBrushFrom(int hue)
    {
        HslColor lightness45 = new(hue, 255, 114);
        var drawing45 = lightness45.ToRgbColor();
        Color lightness45Color = Color.FromRgb(drawing45.R, drawing45.G, drawing45.B);
        HslColor lightness63 = new(hue, 255, 160);
        var drawing63 = lightness63.ToRgbColor();
        Color lightness63Color = Color.FromRgb(drawing63.R, drawing63.G, drawing63.B);
        Color transparent = Color.FromArgb(0x00, drawing63.R, drawing63.G, drawing63.B);
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
