using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Infrastructure.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string createUserID { get; set; }
        public DateTime? createDate { get; set; }
        public string updateUserID { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
