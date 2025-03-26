using BookingSystem.Application.CQRS.Queries;
using BookingSystem.Application.CQRS.Commands;
using BookingSystem.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //  Get all reservations
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _mediator.Send(new GetAllReservationsQuery());
            return Ok(reservations);
        }

        //  Get reservation by ID 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery(id));

            if (reservation == null)
                return NotFound(new { message = $"Reservation with ID {id} not found." }); //  404 Not Found

            return Ok(reservation); //  200 OK
        }

        //  Add a new reservation with input validation
        [HttpPost]
        public async Task<IActionResult> AddNewReservation([FromBody] AddReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new CreateReservationCommand(reservationDto));

            if (!result.Success)
            {
                return BadRequest(new { message = result.ErrorMessage }); // ✅ Return specific error message
            }

            return CreatedAtAction(nameof(GetReservationById), new { id = result.ReservationId },
                new { message = "Reservation added successfully", reservationId = result.ReservationId });
        }

        //  Update a reservation with 404 handling
        [HttpPatch]
        public async Task<IActionResult> EditReservation([FromBody] ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //  400 Bad Request (Invalid input)
            }

            var reservationUpdated = await _mediator.Send(new UpdateReservationCommand(reservationDto));

            if (!reservationUpdated)
            {
                return NotFound(new { message = "Invalid Trip ID or ReservationDate" }); //  404 Not Found
            }

            return Ok(new { message = "Reservation updated successfully" }); //  200 OK
        }


        //  Delete a reservation with 404 handling
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var success = await _mediator.Send(new DeleteReservationCommand(id));

            if (!success)
            {
                return NotFound(new { message = $"Reservation with ID {id} not found." }); //  404 Not Found
            }

            return NoContent(); //  204 No Content (successful deletion)
        }

    }
}
