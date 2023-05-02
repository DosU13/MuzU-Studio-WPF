using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.service;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MuzU_Studio.model;

public class PanAndZoomModel : BindableBase
{
    /// <summary>
    /// Microseconds * HOR_SCALE = Proper measurement on the piano board
    /// </summary>
    private const double HOR_SCALE = 96.0 * 8.0 / 500_000.0; // 96 = x * 500_000 / 8

    internal static double FromMicroseconds(long microseconds) => microseconds * HOR_SCALE;
    internal static long ToMicroseconds(double pixels) => (long)(pixels / HOR_SCALE);
    
    /// <summary>
    /// Microseconds * VER_SCALE = Proper measurement on the piano board
    /// </summary>
    internal const int NOTE_HEIGHT = 64;

    #region Data Members  

    ///
    /// The current scale at which the content is being viewed.
    /// 
    private double contentScaleX = 1;
    private double contentScaleY = 1;

    ///
    /// The X coordinate of the offset of the viewport onto the content (in content coordinates).
    /// 
    private double contentOffsetX = 0;

    ///
    /// The Y coordinate of the offset of the viewport onto the content (in content coordinates).
    /// 
    private double contentOffsetY = 0;

    ///
    /// The width of the content (in content coordinates).
    /// 
    private double contentWidth = 1 << 10;

    ///
    /// The heigth of the content (in content coordinates).
    /// 
    private double contentHeight = 128 * NOTE_HEIGHT;

    ///
    /// The width of the viewport onto the content (in content coordinates).
    /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
    /// data model so that the value can be shared with the overview window.
    /// 
    private double contentViewportWidth = 0;

    ///
    /// The heigth of the viewport onto the content (in content coordinates).
    /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
    /// data model so that the value can be shared with the overview window.
    /// 
    private double contentViewportHeight = 0;

    #endregion Data Members

    ///
    /// The current scale at which the content is being viewed.
    /// 
    public double ContentScaleX
    {
        get
        {
            return contentScaleX;
        }
        set
        {
            SetProperty(ref contentScaleX, value);
        }
    }

    public double ContentScaleY
    {
        get
        {
            return contentScaleY;
        }
        set
        {
            SetProperty(ref contentScaleY, value);
        }
    }

    ///
    /// The X coordinate of the offset of the viewport onto the content (in content coordinates).
    /// 
    public double ContentOffsetX
    {
        get
        {
            return contentOffsetX;
        }
        set
        {
            SetProperty(ref contentOffsetX, value);
        }
    }

    ///
    /// The Y coordinate of the offset of the viewport onto the content (in content coordinates).
    /// 
    public double ContentOffsetY
    {
        get
        {
            return contentOffsetY;
        }
        set
        {
            SetProperty(ref contentOffsetY, value);
        }
    }

    ///
    /// The width of the content (in content coordinates).
    /// 
    public double ContentWidth
    {
        get
        {
            return contentWidth;
        }
        set
        {
            SetProperty(ref contentWidth, value);
        }
    }

    /// <summary>
    /// Width of a pixel inside PanAndZoom with width equal to ContentWidth
    /// </summary>
    public double ContentWidthUnit => ContentWidth / (1 << 13);

    ///
    /// The heigth of the content (in content coordinates).
    /// 
    public double ContentHeight
    {
        get
        {
            return contentHeight;
        }
        set
        {
            SetProperty(ref contentHeight, value);
        }
    }

    ///
    /// The width of the viewport onto the content (in content coordinates).
    /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
    /// data model so that the value can be shared with the overview window.
    /// 
    public double ContentViewportWidth
    {
        get
        {
            return contentViewportWidth;
        }
        set
        {
            SetProperty(ref contentViewportWidth, value);
        }
    }

    /// <summary>
    /// Width of a pixel inside PanAndZoom with width equal to ContentViewportWidth
    /// </summary>
    public double ContentViewportWidthUnit => ContentViewportWidth / (1 << 13);

    ///
    /// The heigth of the viewport onto the content (in content coordinates).
    /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
    /// data model so that the value can be shared with the overview window.
    /// 
    public double ContentViewportHeight
    {
        get
        {
            return contentViewportHeight;
        }
        set
        {
            SetProperty(ref contentViewportHeight, value);
        }
    }
    public const string Nameof_ContentWidth = nameof(ContentWidth);
    public const string Nameof_ContentViewportWidth = nameof(ContentViewportWidth);

    public void UpdateWidth()
    {
        var audioService = App.Current.Services.GetService<AudioService>()!;
        double max = FromMicroseconds((long)audioService.AudioDurationMicroseconds);
        var notes = App.Current.Services.GetService<SequenceListModel>()!.Notes;
        foreach (var n in notes) if (max < n.Width + n.X) max = n.Width + n.X;
        ContentWidth = max;
    }
}
