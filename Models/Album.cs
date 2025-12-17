using System.Collections.Generic;

namespace MusicLibDB
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}