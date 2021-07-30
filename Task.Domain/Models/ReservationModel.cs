using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Domain.Models
{
    public class ReservationModel 
    {
        [Required(ErrorMessage = "Please Select Customer Name ")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please Select Your Trip ")]
        public int TripId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string note { get; set; }
        public string createUserID { get; set; }
        public DateTime? createDate { get; set; }
        public string updateUserID { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
