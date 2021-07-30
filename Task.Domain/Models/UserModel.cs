using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task.Domain.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please enter Your Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Your PassWord")]
        public string PassWord { get; set; }
        public string createUserID { get; set; }
        public DateTime? createDate { get; set; }
        public string updateUserID { get; set; }
        public DateTime? updateDate { get; set; }
    }
}
