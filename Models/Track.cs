namespace MusicLibDB
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}