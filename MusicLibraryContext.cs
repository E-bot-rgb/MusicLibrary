using Microsoft.EntityFrameworkCore;

namespace MusicLibDB
{
    public class MusicLibraryContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // DIN RÄTTA CONNECTION STRING baserad på SSMS
            optionsBuilder.UseSqlServer(
                @"Server=WINDOWS-B4B6BDP\SQLEXPRESS;Database=MusicLibraryDB;Integrated Security=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.ArtistId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Country).HasMaxLength(50);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.AlbumId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Artist)
                    .WithMany(a => a.Albums)
                    .HasForeignKey(e => e.ArtistId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasKey(e => e.TrackId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.Album)
                    .WithMany(a => a.Tracks)
                    .HasForeignKey(e => e.AlbumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}