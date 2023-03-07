using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel.util
{
    public interface ISequenceViewModel
    {
        public Color Color { get; set; }
        public bool Visible { get; set; }
    }
}
