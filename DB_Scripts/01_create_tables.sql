CREATE TABLE Artists (
    ArtistId INT IDENTITY PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Country VARCHAR(50)
);

CREATE TABLE Albums (
    AlbumId INT IDENTITY PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    ReleaseYear INT,
    ArtistId INT NOT NULL,
    FOREIGN KEY (ArtistId) REFERENCES Artists(ArtistId)
);

CREATE TABLE Tracks (
    TrackId INT IDENTITY PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Duration INT,
    AlbumId INT NOT NULL,
    FOREIGN KEY (AlbumId) REFERENCES Albums(AlbumId)
);
