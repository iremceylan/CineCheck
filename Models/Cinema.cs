using System.ComponentModel.DataAnnotations;

namespace CineCheck.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(150)]
        public string? Location { get; set; }

        public ICollection<Showtime>? Showtimes { get; set; }
    }
}
