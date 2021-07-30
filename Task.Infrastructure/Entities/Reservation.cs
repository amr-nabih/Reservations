using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task.Infrastructure.Entities
{
    public class Reservation : BaseEntity
    {
        public string CustomerName { get; set; }
        public int TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string note { get; set; }

        [ForeignKey("TripId")]
        [JsonIgnore]
        public virtual Trip Trip { get; set; }

    }
}
