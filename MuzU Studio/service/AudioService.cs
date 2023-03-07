﻿using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using System.IO;
using MediaPlayer = System.Windows.Media.MediaPlayer;

namespace MuzU_Studio.service;

internal class AudioService
{
    private MediaPlayer mediaPlayer;

    public AudioService()
    {
        mediaPlayer = new MediaPlayer();
    }

    public MediaPlayer MediaPlayer => mediaPlayer;

    public void UpdateAudio(string audioFilePath)
    {
        mediaPlayer.Stop();
        isPlaying = false;
        if (File.Exists(audioFilePath) == false) return;
        try
        {
            mediaPlayer.Open(new Uri(audioFilePath));
        }
        catch (Exception) { }
    }

    private bool isPlaying;
    public bool IsPlaying => isPlaying;

    public void Stop()
    {
        mediaPlayer.Stop();
    }

    internal void PlayPause()
    {
        if (mediaPlayer.Source == null) return;
        if (isPlaying) 
            mediaPlayer.Pause();
        else mediaPlayer.Play();
        isPlaying = !isPlaying;
    }

    internal void Update(ProjectRepository projectRepository)
    {
        if (!projectRepository.ProjectExists) return;
        UpdateAudio(projectRepository.ProjectModel.MuzUProject.MuzUData.MusicLocal.MusicPath);
    }
}
