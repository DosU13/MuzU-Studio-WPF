using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MuzU_Studio.model;

public partial class PianoRollModel
{
    public PianoRollModel() {
        
    }

    private List<Rectangle> lines = new List<Rectangle>();
    public List<Rectangle> Lines => lines;

    private SolidColorBrush lineClr = Brushes.Gray;


    public void UpdateLines()
    {
        return;
        //lines.Clear();
        //for (int i = 0; i < ContentHeight; i += VER_SCALE)
        //{
        //    var l = new Rectangle() { Fill = lineClr, Width = ContentWidth, Height = 5 };
        //    Canvas.SetTop(l, i);
        //    Lines.Add(l);
        //}
        //for (int i = 0; i < ContentWidth; i += HOR_SCALE)
        //{
        //    var l = new Rectangle() { Fill = lineClr, Height = ContentHeight, Width = 5 };
        //    Canvas.SetLeft(l, i);
        //    lines.Add(l);  
        //}
    }

    #region Piano Keys

    private List<PianoKeyViewModel>? pianoKeys;
    public List<PianoKeyViewModel> PianoKeys => pianoKeys ??= InitPianoKeys();

    private List<PianoKeyViewModel> InitPianoKeys()
    {
        // Populate the collection with PianoKeyViewModels for all 128 MIDI keys
        pianoKeys = new();
        for (int i = 0; i < 128; i++)
        {
            pianoKeys.Add(new PianoKeyViewModel(i));
        }
        return pianoKeys;
    }
    #endregion

    #region TimeLine
    private readonly ObservableCollection<TimelineItemViewModel> timelineItems = new();
    public ObservableCollection<TimelineItemViewModel> TimelineItems => timelineItems;

    private void InitTimelineItems()
    {
        // Populate the collection with PianoKeyViewModels for all 128 MIDI keys
        timelineItems.Clear();
        var panAndZoomModel = App.Current.Services.GetService<PanAndZoomModel>()!;
        var itemWidth = PanAndZoomModel.HOR_SCALE * 1_000_000;
        int j = 0;
        for (double i = 0; i < panAndZoomModel.ContentWidth; j++, i+= itemWidth)
        {
            timelineItems.Add(new TimelineItemViewModel(j, i));
        }
    }
    #endregion

    internal void Update()
    {
        InitTimelineItems();
    }
}
