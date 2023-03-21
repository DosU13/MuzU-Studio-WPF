using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MuzU_Studio.viewmodel.shared_property;

public class ThicknessSharedProperty : BindableBase
{
    private readonly double _factor;
    private double _value = 100;
    public double Value {
        get => _value;
        set {
            if (SetProperty(ref _value, value))
                Visible = value > 0;
        }
    }
    private bool _visible;
    public bool Visible { 
        get => _visible; 
        set => SetProperty(ref _visible, value); }

    public ThicknessSharedProperty(double factor)
    {
        _factor = factor;
    }
}
