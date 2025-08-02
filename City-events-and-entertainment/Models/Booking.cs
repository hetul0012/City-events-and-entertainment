using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_events_and_entertainment.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string VisitorName { get; set; } = string.Empty;

        [Required]
        public int NumberOfPersons { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required]
        public int MuseumId { get; set; }

        [ForeignKey("MuseumId")]
        public Museum Museum { get; set; } = null!;

        public string? UserId { get; set; }
    }
}
