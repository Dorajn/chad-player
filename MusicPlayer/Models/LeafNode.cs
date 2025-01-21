using System.Collections.ObjectModel;
using System.Windows.Input;
using MusicPlayer.Utils;

namespace MusicPlayer.Model;

public class LeafNode
{
    public string PlaylistName { get; set; }
    public ICommand ButtonCommand { get; set; }

    public LeafNode(Playlist playlist, ObservableCollection<MusicFile> musicFilesList)
    {
        PlaylistName = playlist.Name;
        ButtonCommand = new RelayCommand(_ => ExecuteCommand(musicFilesList));
    }

    private void ExecuteCommand(ObservableCollection<MusicFile> musicFilesList)
    {
        musicFilesList.Clear();

        musicFilesList.Add(new MusicFile { Playlist = PlaylistName });

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
                    Title = audioFile.Name,
                    Playlist = PlaylistName,
                    Duration = AudioPlayerNAudio.GetTotalSongTime(filePath),
                    Artist = AudioPlayerNAudio.GetSongArtist(filePath),
                }
            );
        }
    }
}
