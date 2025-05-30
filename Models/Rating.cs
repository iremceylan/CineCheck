using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineCheck.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
