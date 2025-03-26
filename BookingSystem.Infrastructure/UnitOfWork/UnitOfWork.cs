using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Core.Interfaces;
using BookingSystem.Infrastructure.Data;

namespace BookingSystem.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IReservationRepository Reservations { get; }
        public ITripRepository Trip { get; }

        public UnitOfWork(AppDbContext context, IReservationRepository reservationRepository, ITripRepository trip)
        {
            _context = context;
            Reservations = reservationRepository;
            Trip = trip;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
