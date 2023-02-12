using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.Entities
{
    public class Offer
    {
        public int ConstructorId { get; set; }
        public int ProjectId { get; set; }
        public int MinBound { get; set; } 
        public int MaxBound { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime SendDate { get; set; } 
        public int Status { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
    }
}
