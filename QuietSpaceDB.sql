-- Create the database
CREATE DATABASE QuietSpaceDB;
GO

-- Use the new database
USE QuietSpaceDB;
GO

-- Create the User table
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Preferences TEXT
);
GO

-- Create the QuietPlace table
CREATE TABLE QuietPlace (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    AverageRating REAL NOT NULL,
    Tags TEXT NOT NULL
);
GO

-- Create the Review table
CREATE TABLE Review (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    PlaceId INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(1000) NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id),
    FOREIGN KEY (PlaceId) REFERENCES QuietPlace(Id)
);
GO

-- Create the Bookmark table
CREATE TABLE Bookmark (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    PlaceId INT NOT NULL,
    DateAdded DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](Id),
    FOREIGN KEY (PlaceId) REFERENCES QuietPlace(Id)
);
GO

-- Insert Users
INSERT INTO [User] (Name, Email, Password, Preferences)
VALUES
('Alice Smith', 'alice@example.com', 'password123', 'Nature, Silence'),
('Bob Johnson', 'bob@example.com', 'securePass!', 'Water sounds, Dim light'),
('Carol Lee', 'carol@example.com', 'carolPW!', 'Minimalism, Indoor plants');
GO

-- Insert QuietPlaces
INSERT INTO QuietPlace (Name, Address, Latitude, Longitude, Category, AverageRating, Tags)
VALUES
('Zen Garden', '123 Peace St, Tranquil Town', 37.7749, -122.4194, 'Garden', 4.8, 'zen, green, peaceful'),
('Silent Library', '456 Calm Ave, Still City', 40.7128, -74.0060, 'Library', 4.5, 'books, study, silence'),
('Mountain Retreat', '789 Solitude Rd, Remote Hills', 39.7392, -104.9903, 'Retreat', 4.9, 'mountains, isolation, fresh air');
GO

-- Insert Reviews
INSERT INTO Review (UserId, PlaceId, Rating, Comment, Date)
VALUES
(1, 1, 5, 'Absolutely serene and beautiful.', '2024-11-01'),
(2, 2, 4, 'Great place to read in peace.', '2024-11-05'),
(3, 3, 5, 'Perfect getaway from the city.', '2024-12-15'),
(1, 2, 4, 'Loved the atmosphere, but a bit crowded.', '2025-01-10');
GO

-- Insert Bookmarks
INSERT INTO Bookmark (UserId, PlaceId, DateAdded)
VALUES
(1, 3, '2025-01-12'),
(2, 1, '2025-01-15'),
(3, 2, '2025-01-20');
GO
