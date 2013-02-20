using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using MediaLibrary;
using Microsoft.Win32;

namespace MediaViewer
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public EditWindow(MultiMedia media)
        {
            InitializeComponent();
            combo_type.ItemsSource = Enum.GetValues(typeof(MultiMedia.MediaType));

            var titleBind = new Binding("Title") { Source = media, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            field_Title.SetBinding(TextBox.TextProperty, titleBind);
            var artistBind = new Binding("Artist") { Source = media, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            field_Artist.SetBinding(TextBox.TextProperty, artistBind);
            var albumBind = new Binding("Album") { Source = media, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            field_Album.SetBinding(TextBox.TextProperty, albumBind);
            var genreBind = new Binding("Genre") { Source = media, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            field_Genre.SetBinding(TextBox.TextProperty, genreBind);

            var mediaBind = new Binding("Media")
            {
                Source = media,
                Mode = BindingMode.TwoWay,
                Converter = new UriConverter(),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            field_media_uri.SetBinding(TextBox.TextProperty, mediaBind);

            var coverBind = new Binding("Cover_Uri")
                                    {
                                        Source = media, Mode = BindingMode.TwoWay, Converter = new UriConverter(), UpdateSourceTrigger=UpdateSourceTrigger.PropertyChanged
                                    };
            field_Cover_uri.SetBinding(TextBox.TextProperty, coverBind);


            var typeBind = new Binding("Type") { Source = media, Mode = BindingMode.TwoWay };
            combo_type.SetBinding(Selector.SelectedItemProperty, typeBind);
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                "JEPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|Bitmap files (*.bmp)|(*.bmp)|Gif files (*.gif)|*.gif";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {
                field_Cover_uri.Text = dialog.FileName;
            }
        }

        private void OpenDialogMedia(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                "MP3 files (*.mp3)|*.mp3|Whatever files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == true)
            {
                field_media_uri.Text = dialog.FileName;
            }
        }
    }
}
