using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.Devices;
using MusicPlayer.Model;
using MusicPlayer.Utils;

public class AudioFile
{
    public string Name { get; internal set; }
    public string Extension { get; internal set; }
}

public class Playlist
{
    public string Name { get; internal set; }
}

public class Data
{
    private static readonly string[] _extensions = [".mp3", ".wav", ".aiff"];

    public static List<Playlist> FetchPlaylists(string rootDirectoryPath)
    {
        return
        [
            .. Directory
                .GetDirectories(rootDirectoryPath)
                .Select(playlist => new Playlist { Name = Path.GetFileName(playlist) }),
        ];
    }

    public static List<AudioFile> FetchAudioFiles(string palylistPath)
    {
        return
        [
            .. Directory
                .GetFiles(palylistPath)
                .Where(audioFileName =>
                    _extensions.Any(extension => Path.GetExtension(audioFileName) == extension)
                )
                .Select(audioFileName => new AudioFile
                {
                    Name = Path.GetFileNameWithoutExtension(audioFileName),
                    Extension = Path.GetExtension(audioFileName),
                }),
        ];
    }

    public static string? FetchLyrics(MusicFile song)
    {
        string filePath = Metadata.absolutePath + "\\" + song.Playlist + "\\" + song.Title + ".txt";

        if (!File.Exists(filePath))
        {
            return null;
        }

        return File.ReadAllText(filePath);
    }

    public static void AddAudioFiles(string playlistName, string[] audioFilePaths)
    {
        string destinationPlaylist = Metadata.absolutePath + "\\" + playlistName;
        foreach (var audioFile in audioFilePaths)
        {
            string audioFileExtension = Path.GetExtension(audioFile);

            if (!_extensions.Any(extension => audioFileExtension == extension))
            {
                continue;
            }

            string audioFileName = Path.GetFileNameWithoutExtension(audioFile);
            string destinationAudioFile =
                destinationPlaylist + "\\" + audioFileName + audioFileExtension;

            if (File.Exists(destinationAudioFile))
            {
                continue;
            }

            File.Copy(audioFile, destinationAudioFile);
        }
    }

    public static void CreateAndOpenLyricsFile(MusicFile song)
    {
        string filePath = Metadata.absolutePath + "\\" + song.Playlist + "\\" + song.Title + ".txt";

        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath))
                ;
        }

        Process.Start("notepad.exe", filePath);
    }
}
