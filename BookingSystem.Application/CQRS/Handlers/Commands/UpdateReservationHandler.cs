using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.CQRS.Commands;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.CQRS.Handlers.Commands
{
    public class UpdateReservationHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public UpdateReservationHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing reservation
            var existingReservation = await _unitOfWork.Reservations.GetByIdAsync(request.reservationDto.Id);
            if (existingReservation == null)
            {
                return false; // ❌ Reservation not found
            }

            // ✅ Check if TripId exists
            var tripExists = await _unitOfWork.Trip.GetByIdAsync(request.reservationDto.TripId);
            if (tripExists==null)
            {
                return false; // ❌ Trip does not exist
            }

            // ✅ Check if User exists
            var userExists = await _userManager.FindByIdAsync(request.reservationDto.ReservedById.ToString());
            if (userExists == null)
            {
                return false; // ❌ User does not exist
            }

            // ✅ Update fields
            existingReservation.CustomerName = request.reservationDto.CustomerName;
            existingReservation.TripId = request.reservationDto.TripId;
            existingReservation.ReservedById = request.reservationDto.ReservedById;
            existingReservation.ReservationDate = request.reservationDto.ReservationDate;
            existingReservation.Notes = request.reservationDto.Notes;

            // ✅ Save changes
            await _unitOfWork.Reservations.UpdateAsync(existingReservation);
            await _unitOfWork.SaveChangesAsync();

            return true; // ✅ Successfully updated
        }

    }
}
