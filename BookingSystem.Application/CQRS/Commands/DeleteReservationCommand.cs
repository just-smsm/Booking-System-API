using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookingSystem.Application.CQRS.Commands
{
    public record DeleteReservationCommand(int Id) : IRequest<bool>;
}
