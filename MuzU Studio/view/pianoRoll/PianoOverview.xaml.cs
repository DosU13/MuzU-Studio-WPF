using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZoomAndPan;

namespace MuzU_Studio.view;

/// <summary>
/// Interaction logic for PianoOverview.xaml
/// </summary>
public partial class PianoOverview : UserControl, VMRefreshableView
{
    public PianoOverview()
    {
        this.InitializeComponent();
    }

    private PianoRollViewModel pianoRollViewModel => (PianoRollViewModel)DataContext;

    /// <summary>
    /// Event raised when the size of the ZoomAndPanControl changes.
    /// </summary>
    private void pianoRoll_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        //
        // Update the scale so that the entire content fits in the window.
        //
        overview.ScaleToFit();
    }

    /// <summary>
    /// Event raised when the user drags the overview zoom rect.
    /// </summary>
    private void overviewZoomRectThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        //
        // Update the position of the overview rect as the user drags it around.
        //
        double newContentOffsetX = Math.Min(Math.Max(0.0, Canvas.GetLeft(overviewZoomRectThumb) + e.HorizontalChange), 
            pianoRollViewModel.PianoRollModel.ContentWidth - pianoRollViewModel.PianoRollModel.ContentViewportWidth);
        Canvas.SetLeft(overviewZoomRectThumb, newContentOffsetX);

        double newContentOffsetY = Math.Min(Math.Max(0.0, Canvas.GetTop(overviewZoomRectThumb) + e.VerticalChange), 
            pianoRollViewModel.PianoRollModel.ContentHeight - pianoRollViewModel.PianoRollModel.ContentViewportHeight);
        Canvas.SetTop(overviewZoomRectThumb, newContentOffsetY);
    }

    /// <summary>
    /// Event raised on mouse down.
    /// </summary>
    private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        //
        // Update the position of the overview rect to the point that was clicked.
        //
        Point clickedPoint = e.GetPosition(content);
        double newX = clickedPoint.X - (overviewZoomRectThumb.Width / 2);
        double newY = clickedPoint.Y - (overviewZoomRectThumb.Height / 2);
        Canvas.SetLeft(overviewZoomRectThumb, newX);
        Canvas.SetTop(overviewZoomRectThumb, newY);
    }

    public void RefreshViewModel()
    {
        DataContext = App.Current.Services.GetService<PianoRollViewModel>();
    }
}
