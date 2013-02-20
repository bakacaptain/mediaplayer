using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using MediaLibrary;
using Microsoft.Win32;

namespace MediaViewer
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private readonly ObservableCollection<MultiMedia> collection;

        public AddWindow(ObservableCollection<MultiMedia> collection)
        {
            InitializeComponent();
            this.collection = collection;
            combo_type.ItemsSource = Enum.GetValues(typeof(MultiMedia.MediaType));
            combo_type.SelectedIndex = combo_type.Items.IndexOf(MultiMedia.MediaType.UNKNOWN);
        }

        private void open_dialog(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                "JEPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|Bitmap files (*.bmp)|(*.bmp)|Gif files (*.gif)|*.gif";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {
                this.field_Cover_uri.Text = dialog.FileName;
            }
        }

        private void open_dialog_media(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                "MP3 files (*.mp3)|*.mp3|Whatever files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {
                this.field_media_uri.Text = dialog.FileName;
            }
        }

        private void CompletedMediaForm(object sender, RoutedEventArgs e)
        {
            var media = new MultiMedia
            {
                Artist = this.field_Artist.Text,
                Title = this.field_Title.Text,
                Album = this.field_Album.Text,
                Genre = this.field_Genre.Text,
                Media = (this.field_media_uri.Text == string.Empty) ? null : new Uri(this.field_media_uri.Text),
                Cover_Uri = (this.field_Cover_uri.Text == string.Empty) ? null : new Uri(this.field_Cover_uri.Text),
                Type = (MultiMedia.MediaType)this.combo_type.SelectedItem
            };
            this.collection.Add(media);
            DialogResult = true;
        }
    }
}
