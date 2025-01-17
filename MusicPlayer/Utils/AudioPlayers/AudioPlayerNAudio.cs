﻿using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Media;
using TagLib;
using NAudio.Dsp;


namespace MusicPlayer.Utils;

public class AudioPlayerNAudio : IDisposable, IAudioPlayer
{
    private IWavePlayer _player;
    private WaveStream? _audioFileReader;
    private VolumeSampleProvider _volumeProvider;
    public float VolumeLevel { get; set; }

    public AudioPlayerNAudio()
    {
        _player = new WaveOutEvent();
        VolumeLevel = 0.5f;
    }

    public void Play(string filePath)
    {
        try
        {
            _audioFileReader = CreateAudioFileReader(filePath);
            _volumeProvider = new VolumeSampleProvider(_audioFileReader.ToSampleProvider());
            Volume(VolumeLevel);
            _player.Init(_volumeProvider);
            _player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while playing audio. \n" + ex.ToString());
            throw;
        }
    }

    public void Stop()
    {
        if (_player != null)
        {
            _player.Stop();
        }

        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }
    }

    public void Pause()
    {
        if (_player != null && _player.PlaybackState == PlaybackState.Playing)
        {
            _player.Pause();
        }
    }

    public void Resume()
    {
        if (_player != null && _player.PlaybackState == PlaybackState.Paused)
        {
            _player.Play();
        }
    }

    public void Dispose()
    {
        Stop();
        if (_player != null)
        {
            _player.Dispose();
            _player = null;
        }
    }

    private WaveStream CreateAudioFileReader(string filePath)
    {
        string extension = System.IO.Path.GetExtension(filePath).ToLower();

        return extension switch
        {
            ".mp3" => new Mp3FileReader(filePath),
            ".wav" => new WaveFileReader(filePath),
            ".aiff" => new AiffFileReader(filePath),
            _ => throw new NotSupportedException($"File format {extension} is not supported")
        };
    }

    public void Volume(float volume)
    {
        if (volume >= 0 && volume <= 1)
        {
            if(_volumeProvider != null)
                _volumeProvider.Volume = volume;
            VolumeLevel = volume;
        }
    }

    public string GetTotalSongTime(string filePath)
    {
        using (var audioFile = new AudioFileReader(filePath))
        {
            TimeSpan duration = audioFile.TotalTime;
            return duration.ToString(@"mm\:ss");
        }
    }
    
    public double GetSongPlaybackPercentage()
    {
        if (_audioFileReader == null || _audioFileReader.TotalTime == TimeSpan.Zero)
        {
            return 0;
        }

        double percentage = _audioFileReader.CurrentTime.TotalSeconds / _audioFileReader.TotalTime.TotalSeconds;
        return percentage;
    }

    public string GetSongArtist(string filePath)
    {
        var file = File.Create(filePath);
        string artist = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : "Unknown artist";
        return artist;
    }

}