using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineCheck.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;  // Non-nullable

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        [Required]
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!;

        [Required]
        [ForeignKey("Showtime")]
        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; } = null!;

        [Range(1, 10)]
        public int NumberOfTickets { get; set; } = 1;
    }
}
