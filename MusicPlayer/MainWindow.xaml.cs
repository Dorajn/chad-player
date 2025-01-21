using System.Windows;
using MusicPlayer.Services;
using System.Windows.Controls;
using System.Windows.Media;
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
            Data.AddAudioFiles(mainViewModel.CurrentPlaylist, audioFilePaths);
        }
    }

    private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"));
        }
    }

    private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1d1d1d"));
        }
    }

    private void Button_MouseEnterPlay(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007a13"));
        }
    }

    private void Button_MouseLeavePlay(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00e022"));
        }
    }
}
