using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class Venues
    {
        [Key]
        public int VenueID { get; set; }
        
        [StringLength(60, MinimumLength = 3)]
        public string? VenueName { get; set; }
        
        [StringLength(60, MinimumLength = 3)]
        public string? location { get; set; }
       
        [StringLength(5)]
        public string? Capacity { get; set; }
       
        public string? URLImage { get; set; }
    }
}
