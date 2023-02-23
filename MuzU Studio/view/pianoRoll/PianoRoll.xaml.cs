using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.viewmodel;
using System;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Point = System.Windows.Point;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MuzU_Studio.view;

public sealed partial class PianoRoll : UserControl
{
    public PianoRoll()
    {
        this.InitializeComponent();
    }

    private PianoRollViewModel pianoRollViewModel => (PianoRollViewModel)DataContext;

    private void zoomAndPanControl_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        e.Handled = true;
        Point curContentMousePoint = e.GetPosition(content);
        bool isHor = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
        {
            double hor = isHor ? 1.3 : 1.0;
            double ver = isHor ? 1.0 : 1.3;
            if (e.Delta > 0) Zoom(curContentMousePoint, hor, ver);
            else if (e.Delta < 0) Zoom(curContentMousePoint, 1 / hor, 1 / ver);
        }
        else
        {
            if (e.Delta > 0)
            {
                if (isHor) zoomAndPanControl.LineLeft();
                else zoomAndPanControl.LineUp();
            }
            else
            {
                if (isHor) zoomAndPanControl.LineRight();
                else zoomAndPanControl.LineDown();
            }
        }
    }

    private bool isPanningMode = false;
    private Point origContentMouseDownPoint;
    private void zoomAndPanControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Middle) isPanningMode = true;
        origContentMouseDownPoint = e.GetPosition(content);
    }

    private void zoomAndPanControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (isPanningMode)
        {
            Point curContentMousePoint = e.GetPosition(content);
            Vector dragOffset = curContentMousePoint - origContentMouseDownPoint;
            zoomAndPanControl.ContentOffsetX -= dragOffset.X;
            zoomAndPanControl.ContentOffsetY -= dragOffset.Y;
            e.Handled = true;
        }
    }

    private void zoomAndPanControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        isPanningMode = false;
    }

    private void pianoRoll_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        zoomAndPanControl.ScaleToFit();
    }

    private void zoomAndPanControl_KeyDown(object sender, KeyEventArgs e)
    {
        if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
        {
            e.Handled = true;
            bool isHor = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

            double hor = isHor ? 1.3 : 1.0;
            double ver = isHor ? 1.0 : 1.3;
            Point zoomPoint = new Point(zoomAndPanControl.ContentZoomFocusX, zoomAndPanControl.ContentZoomFocusY);
            if (e.Key == Key.OemPlus) Zoom(zoomPoint, hor, ver);
            else if (e.Key == Key.OemMinus) Zoom(zoomPoint, 1 / hor, 1 / ver);
        }
    }

    private void Zoom(Point curContentMousePoint, double horizontalChange, double verticalChange)
    {
        zoomAndPanControl.ZoomAboutPoint(zoomAndPanControl.ContentHorizontalScale * horizontalChange,
            zoomAndPanControl.ContentVerticalScale * verticalChange, curContentMousePoint);
    }


    bool draggingNoteMode = false;
    Vector origNoteMouseDownPoint;
    Point selectedNotePos;
    private void Note_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed) return;
        content.Focus();
        Keyboard.Focus(content);

        FrameworkElement noteFrame = (FrameworkElement)sender;
        NoteViewModel note = (NoteViewModel)noteFrame.DataContext;

        note.IsSelected = true;

        draggingNoteMode = true;
        origNoteMouseDownPoint = e.GetPosition(content) - e.GetPosition(noteFrame);
        selectedNotePos = new Point(note.X, note.Y);

        noteFrame.CaptureMouse();

        e.Handled = true;
    }

    private void Note_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (!draggingNoteMode) return;

        draggingNoteMode = false;

        FrameworkElement noteFrame = (FrameworkElement)sender;
        noteFrame.ReleaseMouseCapture();

        e.Handled = true;
    }

    private void Note_MouseMove(object sender, MouseEventArgs e)
    {
        if(!draggingNoteMode) return;

        Point curContentPoint = e.GetPosition(content);
        FrameworkElement noteFrame = (FrameworkElement)sender;
        NoteViewModel note = (NoteViewModel)noteFrame.DataContext;
        note.X = selectedNotePos.X + curContentPoint.X - origNoteMouseDownPoint.X;
        note.Y = Convert.ToInt32(selectedNotePos.Y + curContentPoint.Y - origNoteMouseDownPoint.Y);

        e.Handled = true;
    }

    public void RefreshViewModel()
    {
        DataContext = App.Current.Services.GetService<PianoRollViewModel>();
    }
}
