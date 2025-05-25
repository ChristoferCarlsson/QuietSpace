namespace Application.DTOs;

public class QuietPlaceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }       // double
    public double Longitude { get; set; }      // double
    public string Category { get; set; }       // string
    public float AverageRating { get; set; }   // float
    public string Tags { get; set; }
}
