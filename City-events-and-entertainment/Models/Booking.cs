using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_events_and_entertainment.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Visitor Name")]
        public string VisitorName { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int MuseumId { get; set; }

        [ForeignKey("MuseumId")]
        public Museum? Museum { get; set; }
    }
}
