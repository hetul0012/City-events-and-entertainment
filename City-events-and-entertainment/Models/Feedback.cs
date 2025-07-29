using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_events_and_entertainment.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int MuseumId { get; set; }

        [ForeignKey("MuseumId")]
        public Museum? Museum { get; set; }
    }
}
