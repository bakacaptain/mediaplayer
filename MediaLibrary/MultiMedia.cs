using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace MediaLibrary
{
    public class MultiMedia : INotifyPropertyChanged
    {
        #region MediaType enum

        public enum MediaType
        {
            UNKNOWN,
            CD,
            VCD,
            DVD,
            BD,
            Digital,
            Stream,
        }

        #endregion

        private string _album;
        private string _artist;
        private Uri _cover_uri;
        private Uri _media_uri;
        private BitmapImage _cover;
        private string _genre;
        private string _title;

        private MediaType _type;

        public MultiMedia()
        {
            _title = "No title";
            _artist = "Unknown artist";
            _album = "Unknown album";
            _genre = "Undefined genre";
            _media_uri = null;
            Cover_Uri = new Uri("pack://application:,,,/MediaLibrary;component/default.png");
            _type = MediaType.UNKNOWN;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if(!StringIsEmptyOrWhitespace(value))
                {
                    _title = value;
                    OnPropertyChanged("Title");
                    OnPropertyChanged("MediaName");   
                }
            }
        }

        public string Album
        {
            get { return _album; }
            set
            {
                if (!StringIsEmptyOrWhitespace(value))
                {
                    _album = value;
                    OnPropertyChanged("Album");
                }
            }
        }

        public string Artist
        {
            get { return _artist; }
            set
            {
                if (!StringIsEmptyOrWhitespace(value))
                {
                    _artist = value;
                    OnPropertyChanged("Artist");
                    OnPropertyChanged("MediaName");
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                if (!StringIsEmptyOrWhitespace(value))
                {
                    _genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        public Uri Media
        {
            get { return _media_uri; }
            set
            {
                if(value != null)
                {
                    _media_uri = value;
                    OnPropertyChanged("Media");
                }
            }
        }

        public Uri Cover_Uri
        {
            get { return _cover_uri; }
            set
            {
                if(value != null)
                {
                    _cover_uri = value;
                    Cover = new BitmapImage(_cover_uri);
                    OnPropertyChanged("Cover_Uri");
                }
            }
        }

        public BitmapImage Cover
        {
            get { return _cover; }
            set
            {
                if(value != null)
                {
                    _cover = value;
                    OnPropertyChanged("Cover");
                }
            }
        }


        public MediaType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public string MediaName
        {
            get { return ToString(); }
            // Solves the problem of not updating ToString for the listbox when a property has changed.
        }

        private bool StringIsEmptyOrWhitespace(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged(string propertyName) //TODO: Virtual or not?
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Title, this.Artist);
        }
    }
}