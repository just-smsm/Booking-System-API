using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.DTOs
{
    public class GetReservationResponseDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int TripId { get; set; }
        public string TripName { get; set; } 
        public string ReservedById { get; set; }
        public string ReservedByName { get; set; } 
        public DateTime ReservationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Notes { get; set; }
    }
}
