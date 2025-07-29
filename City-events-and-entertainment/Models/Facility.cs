using System.ComponentModel.DataAnnotations;

namespace City_events_and_entertainment.Models
{
    public class Facility
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<MuseumFacility>? MuseumFacilities { get; set; }
    }
}
