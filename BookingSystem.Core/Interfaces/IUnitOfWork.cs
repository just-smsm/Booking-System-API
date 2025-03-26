using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IReservationRepository Reservations { get; }
        ITripRepository Trip { get; }
        Task<int> SaveChangesAsync();
    }
}
