namespace Application.DTOs
{
    public class BookmarkDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
