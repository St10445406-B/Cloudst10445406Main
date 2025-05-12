using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        
        [StringLength(60, MinimumLength = 3)]
        public string? EventName { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
        
        [StringLength(500, MinimumLength = 3)]
        
        public string? Description { get; set; }
    }
}
