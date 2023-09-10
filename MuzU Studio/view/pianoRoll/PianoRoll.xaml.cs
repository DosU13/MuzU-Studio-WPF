using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.viewmodel;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        App.Current.ServiceManager.ServiceUpdated += ServiceManager_ServiceUpdated;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).KeyDown += PianoRoll_KeyDown; ;
    }

    private void ServiceManager_ServiceUpdated(IServiceProvider serviceProvider)
    {
        Application.Current.Dispatcher.Invoke(() => 
            DataContext = serviceProvider.GetService<PianoRollViewModel>());
    }

    private PianoRollViewModel PianoRollViewModel => (PianoRollViewModel)DataContext;
    private static SequenceListModel SequenceListModel => App.Current.Services.GetService<SequenceListModel>()!;

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
    private Point mouseDownPointRelativeToContent;
    private void zoomAndPanControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Middle) isPanningMode = true;
        mouseDownPointRelativeToContent = e.GetPosition(content);
        if (e.ChangedButton == MouseButton.Left)
        {
            if(!PianoRollViewModel.EditingLocked)
                PianoRollViewModel.AddNote(mouseDownPointRelativeToContent, NoteCreationWidth);
        }
    }

    private void zoomAndPanControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (isPanningMode)
        {
            Point curContentMousePoint = e.GetPosition(content);
            Vector dragOffset = curContentMousePoint - mouseDownPointRelativeToContent;
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

    private List<NoteViewModel> copiedNotes;
    private void PianoRoll_KeyDown(object sender, KeyEventArgs e)
    {
        if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
        {
            bool isHor = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

            double hor = isHor ? 1.3 : 1.0;
            double ver = isHor ? 1.0 : 1.3;
            Point zoomPoint = new Point(zoomAndPanControl.ContentZoomFocusX, zoomAndPanControl.ContentZoomFocusY);
            switch (e.Key)
            {
                case Key.OemPlus:
                    Zoom(zoomPoint, hor, ver);
                    break;
                case Key.OemMinus:
                    Zoom(zoomPoint, 1 / hor, 1 / ver);
                    break;
                case Key.X:
                    if (PianoRollViewModel.EditingLocked) break;
                    copiedNotes = SequenceListModel.Notes.Where(x => x.IsSelected).ToList();
                    foreach(var x in copiedNotes)
                    {
                        SequenceListModel.Notes.Remove(x);
                    }
                    break;
                case Key.C:
                    copiedNotes = SequenceListModel.Notes.Where(x => x.IsSelected).ToList();
                    break;
                case Key.V:
                    if (PianoRollViewModel.EditingLocked) break;
                    foreach (var x in copiedNotes)
                    {
                        PianoRollViewModel.AddNote(x);
                    }
                    break;
            }
        }
        e.Handled = true;
    }

    private void Zoom(Point curContentMousePoint, double horizontalChange, double verticalChange)
    {
        zoomAndPanControl.ZoomAboutPoint(zoomAndPanControl.ContentHorizontalScale * horizontalChange,
            zoomAndPanControl.ContentVerticalScale * verticalChange, curContentMousePoint);
    }


    private bool draggingNoteMode = false;
    private Point notePosRelativeToContentWhenPressed;
    private Point noteXYWhenPressed;
    private double? _noteCreationWidth = null;
    private double NoteCreationWidth
    {
        get
        {
            if (_noteCreationWidth == null || _noteCreationWidth <= 0) _noteCreationWidth = PianoRollViewModel.BeatLength;
            return _noteCreationWidth.Value;
        }

        set => _noteCreationWidth = value;
    }
    private void Note_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (PianoRollViewModel.EditingLocked) return;

        content.Focus();
        Keyboard.Focus(content);

        if (sender is not FrameworkElement noteFrame ||
            noteFrame.DataContext is not NoteViewModel note)
        {
            return;
        }
        
        if(e.ChangedButton == MouseButton.Right)
        {
            PianoRollViewModel.Notes.Remove(note);
        }
        else if(e.ChangedButton == MouseButton.Left)
        {
            //var noteIsSelected = note.IsSelected;
            //if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            //{
            //    foreach (var x in SequenceListModel.Notes)
            //    {
            //        x.IsSelected = false;
            //    }
            //}
            //note.IsSelected = !noteIsSelected;

            draggingNoteMode = true;
            notePosRelativeToContentWhenPressed = e.GetPosition(content);
            noteXYWhenPressed = new Point(note.X, note.Y);
            noteFrame.CaptureMouse();
        }
    }

    private void Note_MouseUp(object sender, MouseButtonEventArgs e)
    {
        draggingNoteMode = false;

        if (PianoRollViewModel.EditingLocked) return;

        FrameworkElement? noteFrame = (FrameworkElement)sender;
        noteFrame?.ReleaseMouseCapture();
    }

    private void Note_MouseMove(object sender, MouseEventArgs e)
    {
        if (PianoRollViewModel.EditingLocked) return;

        Point curContentPoint = e.GetPosition(content);
        if (sender is not FrameworkElement noteFrame ||
            noteFrame.DataContext is not NoteViewModel note)
        {
            return;
        }

        if (draggingNoteMode)
        {
            note.X = noteXYWhenPressed.X + curContentPoint.X - notePosRelativeToContentWhenPressed.X;
            note.Y = Convert.ToInt32(noteXYWhenPressed.Y + curContentPoint.Y - notePosRelativeToContentWhenPressed.Y);
            e.Handled = true;
        }
    }

    private void PlayheadThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        if (sender is not Thumb thumb) return;
        Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
    }

    private void Timeline_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var audioService = App.Current.Services.GetService<AudioService>()!;
        audioService.PlayheadPosition = e.GetPosition(sender as Rectangle).X;
    }

    private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        if (sender is not FrameworkElement noteFrame ||
            noteFrame.DataContext is not NoteViewModel note)
        {
            e.Handled = true;
            return;
        }

        note.Width += e.HorizontalChange;
        NoteCreationWidth = note.Width;
            
        e.Handled = true;
    }
}
