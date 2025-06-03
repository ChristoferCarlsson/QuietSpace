using Domain.Entities;

public class QuietPlace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public float? AverageRating { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}

