using System.ComponentModel.DataAnnotations;

namespace CineCheck.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        // Relationship with Movie
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        // Relationship with Cinema
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
    }
}
