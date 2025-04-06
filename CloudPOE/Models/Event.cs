using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string? EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string? Description { get; set; }
    }
}
