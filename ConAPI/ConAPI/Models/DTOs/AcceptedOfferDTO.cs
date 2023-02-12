using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class AcceptedOfferDTO
    {
        public int ConstructorId { get; set; }
        public string FirstName { get; set; }
        public string ProjectName { get; set; }
        public AcceptedOfferDTO(Offer offer)
        {
            this.ConstructorId = offer.ConstructorId;

            this.FirstName = offer.User.FirstName;
            this.ProjectName = offer.Project.Title;
        }
    }
}
