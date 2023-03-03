using MuzU_Studio.util;
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
    public PianoRollModel() { }

    internal const int HOR_SCALE = 1024;
    internal const int VER_SCALE = 64;

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
}
