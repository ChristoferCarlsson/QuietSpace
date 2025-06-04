namespace Application.DTOs;

public class QuietPlaceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public float? AverageRating { get; set; }

    public string? LatestReviewComment { get; set; }
    public int? LatestReviewRating { get; set; }
    public string? LatestReviewerName { get; set; }
}


