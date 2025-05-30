﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.DTOs
{
    public class AddReservationDto
    {
        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Trip ID is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "ReservedById is required.")]
        public string ReservedById { get; set; }

        [Required(ErrorMessage = "Reservation date is required.")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Notes is required.")]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; }
    }
}
