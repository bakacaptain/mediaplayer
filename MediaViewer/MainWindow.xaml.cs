using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MediaLibrary;

namespace MediaViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<MultiMedia> _collection;
        private TimeSpan TotalTime;
        private DispatcherTimer videoTime;
        private Boolean timerAdded;

        public MainWindow()
        {
            InitializeComponent();
            timerAdded = false;
            _collection = Resources["myMediaList"] as ObservableCollection<MultiMedia>;
            mediaPlayer.LoadedBehavior = MediaState.Manual;
        }

        private void MediaPlayerOnMediaOpened(object sender, RoutedEventArgs routedEventArgs)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TotalTime = mediaPlayer.NaturalDuration.TimeSpan;
                seek_bar.Maximum = TotalTime.TotalSeconds;
            }
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            playing_Label.Content = string.Empty;
        }

        private void VideoTimerTick(object o, EventArgs e)
        {
            if(mediaPlayer.NaturalDuration.HasTimeSpan && mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds > 0)
            {
                if(TotalTime.TotalSeconds > 0)
                {
                    seek_bar.Value = mediaPlayer.Position.TotalSeconds;
                }
            }
        }

        private void BtnOpenClick(object sender, RoutedEventArgs e)
        {
            if(mediaList.SelectedIndex <= -1) return;
            try
            {
                Uri source = _collection[mediaList.SelectedIndex].Media;
                if(source != null)
                {
                    mediaPlayer.Stop();
                    mediaPlayer.Source = source;
                    playing_Label.Content = string.Format("Loaded: {0}", _collection[mediaList.SelectedIndex].MediaName);
                    videoTime = null;
                    timerAdded = false;
                }     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry the picture felt into the void.\n"+ex.StackTrace,ex.Source);
            }
        }

        private void BtnPlayClick(object sender, RoutedEventArgs e)
        {
            if(!timerAdded && (mediaPlayer.HasAudio || mediaPlayer.HasVideo))
            {
                videoTime = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
                videoTime.Tick += VideoTimerTick;
                videoTime.Start();
                timerAdded = true;
            }

            mediaPlayer.Play();
        }

        private void BtnPauseClick(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void BtnStopClick(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddWindow(_collection);
            addWindow.ShowDialog();
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if(mediaList.SelectedIndex <= -1) return;

            var editWindow = new EditWindow(_collection[mediaList.SelectedIndex]);

            editWindow.ShowDialog();
        }

        private void BtnRemoveClick(object sender, RoutedEventArgs e)
        {
            if (mediaList.SelectedIndex > -1)
            {
                _collection.RemoveAt(mediaList.SelectedIndex);
            }
        }

        // Call it a hack - but that is how I push and pull up the grid for the information for the media is expanded and collapsed. 

        private void MediaInfoCollapsed(object sender, RoutedEventArgs e)
        {
            meatGrid.RowDefinitions[1].Height = new GridLength(meatGrid.RowDefinitions[1].MinHeight);
        }

        private void MediaInfoExpanded(object sender, RoutedEventArgs e)
        {
            meatGrid.RowDefinitions[1].Height = new GridLength(meatGrid.RowDefinitions[1].MaxHeight);
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by Sam Chieng (klogesam@gmail.com)\nSelect a media on the list to your left and press the open button to load the media. You can thereafter play, pause and stop the media. You can change the media using through Media>Edit. The cover is a picture, where the Media can be any media as long as your Windows Media Player supports it. Yes there are some minor bugs, but those are out of scope of this assignment. Have fun :D", "About");
        }
    }


}
