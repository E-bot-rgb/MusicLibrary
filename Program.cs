using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicLibDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== MusicLibDB - LINQ Exempel ===\n");

            using (var context = new MusicLibraryContext())
            {
                context.Database.EnsureCreated();

                Console.WriteLine("1. Artister från United Kingdom:");
                Console.WriteLine("SQL: SELECT * FROM Artists WHERE Country = 'United Kingdom';\n");

                var artistsUK = context.Artists
                    .Where(a => a.Country == "United Kingdom")
                    .ToList();

                foreach (var artist in artistsUK)
                {
                    Console.WriteLine($"   - {artist.Name} ({artist.Country})");
                }

                Console.WriteLine("\n2. Låtar längre än 5 minuter (300 sek):");
                Console.WriteLine("SQL: SELECT Title, Duration FROM Tracks WHERE Duration > 300 ORDER BY Duration DESC;\n");

                var longTracks = context.Tracks
                    .Where(t => t.Duration > 300)
                    .OrderByDescending(t => t.Duration)
                    .Select(t => new { t.Title, t.Duration })
                    .ToList();

                foreach (var track in longTracks)
                {
                    Console.WriteLine($"   - {track.Title} ({track.Duration} sek)");
                }

                Console.WriteLine("\n3. Låtar med album- och artistinformation (JOIN):");
                Console.WriteLine("SQL: SELECT t.Title, al.Title, ar.Name FROM Tracks t JOIN Albums al ON t.AlbumId = al.AlbumId JOIN Artists ar ON al.ArtistId = ar.ArtistId;\n");

                var trackDetails = context.Tracks
                    .Include(t => t.Album)
                        .ThenInclude(a => a!.Artist)
                    .Where(t => t.Album != null && t.Album.Artist != null)
                    .Select(t => new
                    {
                        TrackTitle = t.Title,
                        AlbumTitle = t.Album!.Title,
                        ArtistName = t.Album.Artist!.Name
                    })
                    .Take(5)
                    .ToList();

                foreach (var detail in trackDetails)
                {
                    Console.WriteLine($"   - {detail.TrackTitle} | Album: {detail.AlbumTitle} | Artist: {detail.ArtistName}");
                }

                Console.WriteLine("\n4. Antal album per artist (GROUP BY):");
                Console.WriteLine("SQL: SELECT ArtistId, COUNT(*) AS AlbumCount FROM Albums GROUP BY ArtistId;\n");

                var albumCounts = context.Albums
                    .GroupBy(a => a.ArtistId)
                    .Select(g => new
                    {
                        ArtistId = g.Key,
                        AlbumCount = g.Count()
                    })
                    .ToList();

                foreach (var count in albumCounts)
                {
                    var artist = context.Artists.Find(count.ArtistId);
                    Console.WriteLine($"   - {artist?.Name ?? "Unknown"}: {count.AlbumCount} album");
                }

                Console.WriteLine("\n5. Låtar med 'Love' i titeln (LIKE):");
                Console.WriteLine("SQL: SELECT * FROM Tracks WHERE Title LIKE '%Love%';\n");

                var loveSongs = context.Tracks
                    .Where(t => t.Title.Contains("Love"))
                    .ToList();

                if (loveSongs.Any())
                {
                    foreach (var track in loveSongs)
                    {
                        Console.WriteLine($"   - {track.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("   Inga låtar hittades med 'Love' i titeln.");
                }

                Console.WriteLine("\n6. UPDATE exempel:");
                Console.WriteLine("SQL: UPDATE Artists SET Name = 'The Beatles (Remastered)' WHERE ArtistId = 1;\n");

                var beatles = context.Artists.FirstOrDefault(a => a.ArtistId == 1);
                if (beatles != null)
                {
                    Console.WriteLine($"   Före: {beatles.Name}");
                    beatles.Name = "The Beatles (Remastered Edition)";
                    Console.WriteLine($"   Efter: {beatles.Name}");
                    Console.WriteLine("   (Ej sparat - använd SaveChanges() för att spara)");
                }
            }

            Console.WriteLine("\n=== Klart! Tryck på valfri tangent för att avsluta ===");
            Console.ReadKey();
        }
    }
}