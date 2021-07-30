using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Domain.Models
{
    public class TripModel
    {
        [Required(ErrorMessage = "Please enter Trip Name")]
        public string Name { get; set; }
        public string CityName { get; set; }
        [Required(ErrorMessage = "Please enter Trip Price")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string createUserID { get; set; }
        public DateTime? createDate { get; set; }
        public string updateUserID { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
