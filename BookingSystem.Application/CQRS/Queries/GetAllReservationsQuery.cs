using System.Collections.Generic;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using MediatR;

namespace BookingSystem.Application.CQRS.Queries
{
    public record GetAllReservationsQuery : IRequest<IEnumerable<GetReservationResponseDTO>>;
}
