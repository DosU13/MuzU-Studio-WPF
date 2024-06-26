﻿using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using System.IO;
using MediaPlayer = System.Windows.Media.MediaPlayer;

namespace MuzU_Studio.service;

public partial class AudioService
{
    private MediaPlayer mediaPlayer = new();
    private ProjectRepository _projectRepository;

    public void UpdateAudio(string audioFilePath)
    {
        mediaPlayer.Stop();
        IsPlaying = false;
        if (File.Exists(audioFilePath) == false) return;
        try
        {
            mediaPlayer.Open(new Uri(audioFilePath));
            App.Current.Services.GetService<PanAndZoomModel>()!.ResetWidth();
        }
        catch (Exception) { }
    }

    private bool isPlaying;
    public bool IsPlaying
    {
        get => isPlaying;
        set => SetProperty(ref isPlaying, value);
    }
    public const string Nameof_IsPlaying = nameof(IsPlaying);

    public double AudioDurationMicroseconds {
        get {
            return mediaPlayer.NaturalDuration.HasTimeSpan ? 
                mediaPlayer.NaturalDuration.TimeSpan.TotalMicroseconds : 0;
        }
    }

    internal void PlayPause()
    {
        if (mediaPlayer.Source == null) return;
        if (mediaPlayer.Position == mediaPlayer.NaturalDuration)
        {
            mediaPlayer.Position = TimeSpan.Zero;
            IsPlaying = false;
        }
        if (IsPlaying)
        {
            mediaPlayer.Pause();
            _timer.Stop();
        }
        else
        {
            mediaPlayer.Play();
            _timer.Start();
        }
        IsPlaying = !IsPlaying;
    }

    internal void Update(ProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        mediaPlayer.Stop();
        mediaPlayer = new MediaPlayer();
        if (!projectRepository.ProjectExists) return;
        UpdateAudio(projectRepository.ProjectModel.MuzUProject.MuzUData.MusicLocal.MusicPath);
    }
}
