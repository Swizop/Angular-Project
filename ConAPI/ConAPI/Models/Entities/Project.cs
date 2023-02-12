using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime AddDate { get; set; }
        public Boolean Done { get; set; }
        public User User { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }
}
