using MediaPlayer = System.Windows.Media.MediaPlayer;

namespace MuzU_Studio.model;

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
        try
        {
            mediaPlayer.Stop();
            mediaPlayer.Open(new Uri(audioFilePath));
            isPlaying = false;
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
}
