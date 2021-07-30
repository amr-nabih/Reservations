using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Infrastructure.Entities;

namespace Task.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Properties GenericRepository
        GenericRepository<Trip> TripRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Reservation> ReservationRepository { get; }
       
        
        #endregion

        void Commit();
        void Rollback();
    }
}
