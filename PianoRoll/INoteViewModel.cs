﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoRoll;

public interface INoteViewModel
{
    public IVisibilityViewModel Parent { get; }
    public double Width { get; set; }
    public double Height { get; set; }
}
