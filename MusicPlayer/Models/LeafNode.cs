﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using MusicPlayer.Utils;

namespace MusicPlayer.Model;

public class LeafNode
{
    public string PlaylistName { get; set; }
    public List<AudioFile> AudioFiles { get; set; }
    public ICommand ButtonCommand { get; set; }

    public LeafNode(
        string playlistName,
        List<AudioFile> audioFile,
        ObservableCollection<MusicFile> musicFilesList
    )
    {
        PlaylistName = playlistName;
        AudioFiles = audioFile;
        ButtonCommand = new RelayCommand(_ => ExecuteCommand(musicFilesList));
    }

    private void ExecuteCommand(ObservableCollection<MusicFile> musicFilesList)
    {
        musicFilesList.Clear();
        foreach (var audioFile in AudioFiles)
        {
            MusicFile mf = new MusicFile();
            mf.FilePath =
                Metadata.absolutePath
                + "\\"
                + PlaylistName
                + "\\"
                + audioFile.Name
                + audioFile.Extension;
            mf.Title = audioFile.Name;
            mf.Playlist = PlaylistName;
            musicFilesList.Add(mf);
        }
    }
}
