using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime DateAdded { get; set; }

        public User User { get; set; } = null!;
        public QuietPlace Place { get; set; } = null!;
    }
}
