using System;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.CQRS.Commands;
using BookingSystem.Core.Interfaces;
using MediatR;

namespace BookingSystem.Application.CQRS.Handlers.Commands
{
    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(request.Id);

            if (reservation == null)
            {
                return false; // ❌ Reservation not found, return false instead of throwing an exception
            }

            await _unitOfWork.Reservations.DeleteAsync(reservation.Id);
            await _unitOfWork.SaveChangesAsync();

            return true; // ✅ Successfully deleted
        }

    }
}
