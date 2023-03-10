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
    internal const double HOR_SCALE = 96.0 * 8.0 / 500_000.0; // 96 = x * 500_000 / 8
    internal const int VER_SCALE = 64;

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
    private double contentWidth = 128 * HOR_SCALE;

    ///
    /// The heigth of the content (in content coordinates).
    /// 
    private double contentHeight = 128 * VER_SCALE;

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
}
