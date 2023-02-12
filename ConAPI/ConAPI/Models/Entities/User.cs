using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.Entities
{
    public class User: IdentityUser<int>
    {
        public User(): base() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public Rating Rating { get; set; }
    }
}
