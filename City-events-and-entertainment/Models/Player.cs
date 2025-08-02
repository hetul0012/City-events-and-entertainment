using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace City_events_and_entertainment.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }
    }
}
