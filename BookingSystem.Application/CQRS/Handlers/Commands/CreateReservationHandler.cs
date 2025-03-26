using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.CQRS.Commands;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.CQRS.Handlers.Commands
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, ReservationResultDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CreateReservationHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReservationResultDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // ✅ Check if the user exists
            var user = await _userManager.FindByIdAsync(request.reservationDto.ReservedById);
            if (user == null)
            {
                return new ReservationResultDto { Success = false, ErrorMessage = "User not found." };
            }

            // ✅ Check if the trip exists
            var trip = await _unitOfWork.Trip.GetByIdAsync(request.reservationDto.TripId);
            if (trip == null)
            {
                return new ReservationResultDto { Success = false, ErrorMessage = "Trip not found." };
            }

            // ✅ Validate Reservation Date
            if (request.reservationDto.ReservationDate < DateTime.UtcNow)
            {
                return new ReservationResultDto { Success = false, ErrorMessage = "Reservation date cannot be in the past." };
            }

            // ✅ Correct mapping (use DTO, not request)
            var reservation = _mapper.Map<Reservation>(request.reservationDto);
            reservation.CreatedAt = DateTime.UtcNow; // Automatically set creation time

            await _unitOfWork.Reservations.AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            return new ReservationResultDto { Success = true, ReservationId = reservation.Id };
        }

    }
}
