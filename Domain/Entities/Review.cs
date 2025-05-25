using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int Rating { get; set; } // Rating out of 5
        public string Comment { get; set; } = null!;
        public DateTime Date { get; set; }

        public User User { get; set; } = null!;
        public QuietPlace Place { get; set; } = null!;
    }
}
