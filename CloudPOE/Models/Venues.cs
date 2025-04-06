using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class Venues
    {
        [Key]
        public int VenueID { get; set; }
        public string? VenueName { get; set; }
        public string? location { get; set; }
        public string? Capacity { get; set; }
        public string? URLImage { get; set; }
    }
}
