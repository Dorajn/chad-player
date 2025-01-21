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
        foreach (var audioFile in Data.FetchAudioFiles(Metadata.absolutePath + "\\" + PlaylistName))
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
            mf.Duration = AudioPlayerNAudio.GetTotalSongTime(mf.FilePath);
            mf.Artist = AudioPlayerNAudio.GetSongArtist(mf.FilePath);
            musicFilesList.Add(mf);
        }
    }
}
