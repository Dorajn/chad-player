using System.Windows;
using MusicPlayer.ViewModels;

namespace MusicPlayer;

public partial class MainWindow : Window
{
    public MainViewModel mainViewModel { get; set; }

    public MainWindow()
    {
        mainViewModel = new MainViewModel();
        DataContext = mainViewModel;
        InitializeComponent();
    }

    private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        mainViewModel.VolumeSlider_ValueChanged(sender, e, (float)slVolume.Value);
    }

    private void ImagePanel_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] audioFilePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            mainViewModel.Data.AddAudioFiles("Nightcore", audioFilePaths);
        }
    }
}
