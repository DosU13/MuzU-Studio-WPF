﻿using MuzU_Studio.model;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel.util;
using MuzUStandard.data;
using System;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel;

/// <summary>
/// Defines the data-model for a simple displayable rectangle.
/// </summary>
public class NoteViewModel : BindableBase
{
    int HOR_SCALE => PianoRollModel.HOR_SCALE;
    int VER_SCALE => PianoRollModel.VER_SCALE;

    #region Data Members
    /// <summary>
    /// Xml Data of the note
    /// </summary>
    private readonly Node node;

    private readonly ISequenceViewModel parent;

    /// <summary>
    /// The width of the rectangle (in content coordinates).
    /// </summary>
    private double width = 1;

    /// <summary>
    /// The height of the rectangle (in content coordinates).
    /// </summary>
    private double height = 1;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    private bool isSelected = false;

    #endregion Data Members

    public NoteViewModel(Node node, ISequenceViewModel parent)
    {
        this.node = node;
        this.parent = parent;
    }

    /// <summary>
    /// Xml Data repository
    /// </summary>
    public Node Node { get { return node; } }

    /// <summary>
    /// The X coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public double X
    {
        get => (double) node.Time.Numerator.Value * HOR_SCALE / node.Time.Denominator.Value ;
        set
        {
            long newX = Convert.ToInt64(value * node.Time.Denominator.Value / HOR_SCALE);
            if (node.Time.Numerator == newX) return;
            node.Time.Numerator = newX;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The Y coordinate of the location of the rectangle (in content coordinates).
    /// </summary>
    public int Y
    {
        get => node.Note.Value * VER_SCALE;
        set {
            if (node.Note.Value == value / VER_SCALE) return;
            node.Note = value / VER_SCALE;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The width of the rectangle (in content coordinates).
    /// </summary>
    public double Width
    {
        get => (double) node.Length.Numerator.Value * HOR_SCALE / node.Length.Denominator.Value ;
        set
        {
            long newX = Convert.ToInt64(value * node.Length.Denominator.Value / HOR_SCALE);
            if (node.Length.Numerator == newX) return;
            node.Length.Numerator = newX;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The height of the rectangle (in content coordinates).
    /// </summary>
    public double Height
    {
        get => height * VER_SCALE;
    }

    public ISequenceViewModel Parent => parent;

    public Color BorderColor => IsSelected ? Color.FromRgb(250,100,0) : Parent.Color;

    /// <summary>
    /// Set to 'true' when the rectangle is selected in the ListBox.
    /// </summary>
    public bool IsSelected
    {
        get => isSelected;
        set { if (SetProperty(ref isSelected, value)) OnPropertyChanged(nameof(BorderColor)); }
    }
}
