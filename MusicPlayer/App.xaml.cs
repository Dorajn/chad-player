using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using MusicPlayer.Utils;

namespace MusicPlayer;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        string musicFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        
        Metadata.SaveMusicFolderPath(musicFolderPath);
        Metadata.absolutePath = Metadata.GetMusicFolderPath();
    }
}
