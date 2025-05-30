
using System.ComponentModel.DataAnnotations;

namespace CineCheck.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(100)]
        public string? Genre { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [Display(Name = "Poster URL")]
        public string? PosterUrl { get; set; }

        public string? Description { get; set; }

        public string? Director { get; set; }

        public string? Actors { get; set; }

        public ICollection<Showtime>? Showtimes { get; set; }

        public ICollection<Rating>? Ratings { get; set; }

        
    }
}

