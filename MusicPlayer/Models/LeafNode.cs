using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;
using MusicPlayer.Utils;

namespace MusicPlayer.Model;

public class LeafNode
{
    public string PlaylistName { get; set; }
    public ICommand ButtonCommand { get; set; }
    public delegate void PlaylistSetter(string playlistName);
    public event PlaylistSetter PlaylistSetEvent;

    public void OnPlaylistSetEvent(string playlistName)
    {
        PlaylistSetEvent?.Invoke(playlistName);
    }

    public LeafNode(Playlist playlist, ObservableCollection<MusicFile> musicFilesList)
    {
        PlaylistName = playlist.Name;
        ButtonCommand = new RelayCommand(_ => ExecuteCommand(musicFilesList));
    }

    public void RefreshMusicList(ObservableCollection<MusicFile> musicFilesList)
    {
        ExecuteCommand(musicFilesList);
    }

    private void ExecuteCommand(ObservableCollection<MusicFile> musicFilesList)
    {
        musicFilesList.Clear();

        foreach (var audioFile in Data.FetchAudioFiles(Metadata.absolutePath + "\\" + PlaylistName))
        {
            string filePath =
                Metadata.absolutePath
                + "\\"
                + PlaylistName
                + "\\"
                + audioFile.Name
                + audioFile.Extension;

            musicFilesList.Add(
                new MusicFile
                {
                    FilePath = filePath,
                    Title = ShortenTitle(audioFile.Name),
                    Playlist = PlaylistName,
                    Duration = AudioPlayerNAudio.GetTotalSongTime(filePath),
                    Artist = AudioPlayerNAudio.GetSongArtist(filePath),
                }
            );
            
        }
        OnPlaylistSetEvent(PlaylistName);
    }
    
    private string ShortenTitle(string text)
    {
        const int SIZE = 30;
        if (text.Length <= SIZE)
            return text;
        else
        {
            string beg = text.Substring(0, SIZE - 3);
            return beg + "...";
        }
    }
}
