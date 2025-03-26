using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.DTOs
{
    public class ReservationResultDto
    {
        public bool Success { get; set; }
        public int? ReservationId { get; set; }
        public string ErrorMessage { get; set; }
    }

}
