using System;
using System.Collections.ObjectModel;

namespace MediaLibrary
{
    public class MediaCollection : ObservableCollection<MultiMedia>
    {
        public MediaCollection()
        {
            this.Add(new MultiMedia()
            {
                Artist = "Suzy",
                Album = "",
                Genre = "Varity Show",
                Title = "Strong Heart episode 302",
                Media = new Uri(@"http://bakacaptain.net/secrets/sample.mp3"),
                Type = MultiMedia.MediaType.Stream
            });
            this.Add(new MultiMedia()
            {
                Artist = "Alex",
                Album = "Smiling Goodbye",
                Genre = "Music",
                Title = "Just like me",
                Cover_Uri = new Uri(@"http://img198.imageshack.us/img198/3625/295823.jpg"),
                Type = MultiMedia.MediaType.Digital
            });
            this.Add(new MultiMedia()
            {
                Artist = "Scott Hanselman",
                Album = "Best of .NET",
                Genre = "Tutorial",
                Title = "Code ninja",
                Media = new Uri(@"http://bakacaptain.net/secrets/ma_spa.mp3"),
                Cover_Uri = new Uri(@"http://www.hanselman.com/blog/images/tinyheadshot2.jpg"),
                Type = MultiMedia.MediaType.CD
            });
        }
    }
}