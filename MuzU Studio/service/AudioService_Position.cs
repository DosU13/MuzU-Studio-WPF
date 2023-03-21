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
            _timer.Tick += _timer_Tick; ;
            _timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(PlayheadPosition));
        }

        public double PlayheadPosition {
            get => PanAndZoomModel.FromMicroseconds((long)mediaPlayer.Position.TotalMicroseconds);
            set {
                double newMicroseconds = PanAndZoomModel.ToMicroseconds(value);
                if (Math.Abs(mediaPlayer.Position.Microseconds - newMicroseconds) < 1_000_000) return;
                mediaPlayer.Position = TimeSpan.FromMicroseconds(newMicroseconds);
                OnPropertyChanged();
            }
        }
        public const string Nameof_PlayheadPosition = nameof(PlayheadPosition);
    }
}
