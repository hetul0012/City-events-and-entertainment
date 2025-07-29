using System.ComponentModel.DataAnnotations.Schema;

namespace City_events_and_entertainment.Models
{
    public class MuseumFacility
    {
        public int MuseumId { get; set; }
        public Museum Museum { get; set; } = null!;

        public int FacilityId { get; set; }
        public Facility Facility { get; set; } = null!;
    }
}
