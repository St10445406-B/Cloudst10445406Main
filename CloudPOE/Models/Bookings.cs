using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class Bookings
    {
        [Key]
        public int bookingID { get; set; }

        [Required]
        [ForeignKey(nameof(EventID))]
        public int EventID { get; set; }

        [Required]
        [ForeignKey(nameof(VenueID))]
        public int VenueID { get; set; }
        public string? bookingDate { get; set; }

        // Navigation Properties
        public virtual Event? Events { get; set; }
        public virtual Venues? Venue { get; set; }
    }
}
