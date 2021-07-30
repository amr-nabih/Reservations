using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Infrastructure.Entities
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; }
        public string CityName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
