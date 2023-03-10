using MuzU_Studio.model;
using MuzU_Studio.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MuzU_Studio.service
{
    internal partial class AudioService: BindableBase
    {
        private readonly DispatcherTimer _timer = new();

        public AudioService()
        {
            _timer.Interval = TimeSpan.FromSeconds(0.01);
            _timer.Tick += new EventHandler(Timer_tick);
            _timer.Start();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(UIPosition));
        }

        public double UIPosition {
            get => mediaPlayer.Position.TotalMicroseconds * PanAndZoomModel.HOR_SCALE;
            set {
                double newValue = value / PanAndZoomModel.HOR_SCALE;
                mediaPlayer.Position = TimeSpan.FromMicroseconds(newValue);
            }
        }
    }
}
