using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class RegisterConstructorDTO
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
    }
}
