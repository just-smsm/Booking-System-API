using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.CQRS.Queries;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.CQRS.Handlers.Queries
{
    public class GetAllReservationsHandler : IRequestHandler<GetAllReservationsQuery, IEnumerable<GetReservationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllReservationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetReservationResponseDTO>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _unitOfWork.Reservations
                .GetAllAsync(include: r => r.Include(r => r.Trip).Include(r => r.ReservedBy));

            return _mapper.Map<List<GetReservationResponseDTO>>(reservations);
        }
    }
}
