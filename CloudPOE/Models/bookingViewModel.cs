using System.ComponentModel.DataAnnotations;

namespace CloudPOE.Models
{
    public class bookingViewModel
    {
        public int BookingId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public string? EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string? Description { get; set; }
        public string? EventDescription { get; set; }
        public string? VenueName { get; set; }
        public string? Location { get; set; }
        public string? Capacity { get; set; }

        public string? SearchString { get; set; }
        public IEnumerable<Bookings> bookings { get; set; } = new List<Bookings>();

    }
}
