using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel.shared_property
{
    public interface ISequenceSharedProperty
    {
        public int Hue { get; }
        public Color Color { get; }
        public bool Visible { get; }
    }
}
