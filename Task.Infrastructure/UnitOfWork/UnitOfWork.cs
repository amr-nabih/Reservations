using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Infrastructure.DataContext;
using Task.Infrastructure.Entities;

namespace Task.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Properties GenericRepository

        private readonly TaskContext _context;
        private GenericRepository<Trip> _TripRepository;
        private GenericRepository<User> _UserRepository;
        private GenericRepository<Reservation> _ReservationRepository;
    
        #endregion

        #region Methods GenericRepository

       
        public GenericRepository<Trip> TripRepository
        {
            get { return _TripRepository ??= new GenericRepository<Trip>(_context); }
        }
        public GenericRepository<User> UserRepository
        {
            get { return _UserRepository ??= new GenericRepository<User>(_context); }
        }
        public GenericRepository<Reservation> ReservationRepository
        {
            get { return _ReservationRepository ??= new GenericRepository<Reservation>(_context); }
        }

      
        #endregion

        #region Methods And Construcor UnitOfWork
        public UnitOfWork(TaskContext context)
        { _context = context; }

        public void Commit()
        { _context.SaveChanges(); }

        public void Rollback()
        { _context.Dispose(); }
        #endregion

    }
}
