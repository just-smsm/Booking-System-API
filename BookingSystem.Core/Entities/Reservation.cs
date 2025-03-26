using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Entities
{
    public class Reservation : ModelBase
    {
        public string CustomerName { get; set; } // Manually entered

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public string ReservedById { get; set; }  // Foreign Key to AppUser
        public AppUser ReservedBy { get; set; }

        public DateTime ReservationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; }
    }
}
