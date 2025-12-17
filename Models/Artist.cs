using System.Collections.Generic;

namespace MusicLibDB
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Country { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
