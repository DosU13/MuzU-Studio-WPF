using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PianoRoll
{
    public interface IVisibilityViewModel
    {
        public bool Visible { get; set; }
        public Color Color { get; set; }
    }
}
