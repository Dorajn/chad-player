using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MusicPlayer.Services;
using MusicPlayer.ViewModels;

namespace MusicPlayer;

public partial class MainWindow : Window
{
    public MainViewModel mainViewModel { get; set; }
    private bool isPanelVisible = false;

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

    private void AudioFiles_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] audioFilePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            Data.AddAudioFiles(mainViewModel.CurrentPlaylist, audioFilePaths);
        }
        mainViewModel.RefreshCurrentPlaylist();
    }

    private void Playlists_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] playlistPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            Data.AddPlaylists(playlistPaths);
        }
        mainViewModel.RefreshGatherPaths();
    }

    private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#3d3d3d")
            );
        }
    }

    private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#1d1d1d")
            );
        }
    }

    private void Button_MouseEnterPlay(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#007a13")
            );
        }
    }

    private void Button_MouseLeavePlay(object sender, System.Windows.Input.MouseEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            button.Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString("#00e022")
            );
        }
    }

    private void ToggleSidePanel_Click(object sender, RoutedEventArgs e)
    {
        if (isPanelVisible)
        {
            SidePanelColumn.Width = new GridLength(0);
            this.Width = this.Width - 350;
        }
        else
        {
            SidePanelColumn.Width = new GridLength(350);
            this.Width = this.Width + 350;
        }

        isPanelVisible = !isPanelVisible;
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scrollViewer = sender as ScrollViewer;
        if (scrollViewer != null)
        {
            if (e.Delta > 0)
                scrollViewer.LineUp();
            else
                scrollViewer.LineDown();

            e.Handled = true;
        }
    }
}
