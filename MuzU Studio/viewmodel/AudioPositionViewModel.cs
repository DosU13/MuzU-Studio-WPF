using MuzU_Studio.model;
using MuzU_Studio.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MuzU_Studio.viewmodel;

internal class AudioPositionViewModel
{
    private readonly AudioService _audioService;
    private readonly DispatcherTimer _timer = new DispatcherTimer();

    public AudioPositionViewModel(AudioService audioService)
    {
        _audioService = audioService;
        _timer.Interval = TimeSpan.FromMilliseconds(300);
        _timer.Tick += new EventHandler(ticktock);
        _timer.Start();
    }

    private void ticktock(object sender, EventArgs e)
    {
        Position = _audioService.MediaPlayer.Position;
    }

    public TimeSpan Position { get; set; }

    public double PositionOnBoard => Position.TotalMilliseconds * PianoRollModel.HOR_SCALE;
}
