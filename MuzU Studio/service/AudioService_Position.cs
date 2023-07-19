using Microsoft.Extensions.DependencyInjection;
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
    public partial class AudioService: BindableBase
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

        private long MusicOffset => _projectRepository.ProjectModel.MuzUProject.
                                        MuzUData.MusicLocal.MusicOffsetMicroseconds;
                                    
        public double PlayheadPosition {
            get => PanAndZoomModel.FromMicroseconds(
                (long)mediaPlayer.Position.TotalMicroseconds - MusicOffset);
            set {
                double newMicroseconds = PanAndZoomModel.ToMicroseconds(value) + MusicOffset;
                mediaPlayer.Position = TimeSpan.FromMicroseconds(newMicroseconds);
                OnPropertyChanged();
            }
        }
        public const string Nameof_PlayheadPosition = nameof(PlayheadPosition);
    }
}
