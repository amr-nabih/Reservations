
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task.Infrastructure.Entities;

namespace Task.Infrastructure.DataContext
{
    public class TaskContext :DbContext
    {
        #region Methods Context
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>().HasData(new[]{
      new User {Id=1,Email ="amrn@amr.com", PassWord="123456"},
      new User{ Id=2,Email = "ahmed@amr.com", PassWord = "987654" },
       new User{ Id=3,Email = "ahmed2@amr.com", PassWord = "987654" },
       new User{ Id=4,Email = "ahmed3@amr.com", PassWord = "741852" }
            });
            modelBuilder.Entity<Trip>().HasData(new[]{
            new Trip { Id=1, Name ="رحلة مدينة نصر", CityName ="القاهره", Price = 100.0M, Content = "رحله تلف حول مدينة نصر بمعالمها السياحيه",CreationDate=System.DateTime.Now },
            new Trip { Id=2, Name = "رحلة الجيزه", CityName = "الجيزه", Price = 100.0M, Content = "رحله تلف حول الجيزه بمعالمها السياحيه",CreationDate=System.DateTime.Now },
          new Trip {Id=3, Name = "رحلة الرياض", CityName = "الرياض", Price = 100.0M, Content = "رحله تلف حول الرياض بمعالمها السياحيه",CreationDate=System.DateTime.Now }
            });

           


            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Properties DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

       


        #endregion


    }
}


