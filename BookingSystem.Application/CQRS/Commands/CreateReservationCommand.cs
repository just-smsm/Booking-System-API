using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using MediatR;

namespace BookingSystem.Application.CQRS.Commands
{
    public record CreateReservationCommand(
    AddReservationDto reservationDto
) : IRequest<ReservationResultDto>;

}
