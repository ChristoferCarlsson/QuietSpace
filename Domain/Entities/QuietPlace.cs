using Domain.Entities;

public class QuietPlace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }        // Fixed typo here
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Category { get; set; }       // Change to string to match DB
    public float AverageRating { get; set; }   // Add this property if missing

    public string Tags { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}
