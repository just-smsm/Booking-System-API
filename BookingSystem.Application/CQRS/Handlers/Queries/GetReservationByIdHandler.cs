using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookingSystem.Application.CQRS.Queries;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.CQRS.Handlers.Queries
{
    public class GetReservationByIdHandler : IRequestHandler<GetReservationByIdQuery, GetReservationResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetReservationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GetReservationResponseDTO> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.Reservations
    .GetByIdAsync(request.Id, include: r => r.Include(r => r.Trip).Include(r => r.ReservedBy));

            return _mapper.Map<GetReservationResponseDTO>(reservation);

        }
    }
}
