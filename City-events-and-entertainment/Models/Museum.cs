using System.ComponentModel.DataAnnotations;

namespace City_events_and_entertainment.Models
{
    public class Museum
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public List<Booking> Bookings { get; set; } = new();
        public List<Feedback> Feedbacks { get; set; } = new();
        public List<MuseumFacility> MuseumFacilities { get; set; } = new();
    }
}
