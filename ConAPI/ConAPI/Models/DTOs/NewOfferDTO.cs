using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class NewOfferDTO
    {
        public int MinBound { get; set; }
        public int MaxBound { get; set; }
        public DateTime date { get; set; }
        public int ProjectId { get; set; }
    }
}
