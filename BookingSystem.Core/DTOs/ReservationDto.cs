using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;

namespace BookingSystem.Core.DTOs
{
    public class ReservationDto
    {
        [Required(ErrorMessage = "Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        public int Id { get; set; }

        // [Required(ErrorMessage = "Customer name is required.")] [http Patch] can edit on one property only
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        
        public string? CustomerName { get; set; }
                
        // [Required(ErrorMessage = "Reservation date is required.")] [http Patch] can edit on one property only
        [DataType(DataType.Date)]
        public DateTime? ReservationDate { get; set; }

        // [Required(ErrorMessage = "Notes is required.")] [http Patch] can edit on one property only
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string? Notes { get; set; }  
    }
}
