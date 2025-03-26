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
                return false; // Reservation not found
            }

            // Validate reservation date (Ensure it’s not in the past)
            if (request.reservationDto.ReservationDate < DateTime.UtcNow)
            {
                return false; // Invalid date
            }

            // Update fields
            existingReservation.CustomerName = request.reservationDto.CustomerName;
            existingReservation.ReservationDate = request.reservationDto.ReservationDate;
            existingReservation.Notes = request.reservationDto.Notes;

            // Save changes
            await _unitOfWork.Reservations.UpdateAsync(existingReservation);
            await _unitOfWork.SaveChangesAsync();

            return true; // Successfully updated
        }
    }
}
