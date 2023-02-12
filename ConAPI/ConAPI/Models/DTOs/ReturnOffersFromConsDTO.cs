using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class ReturnOffersFromConsDTO
    {
        public int ConstructorId { get; set; }
        public int MinBound { get; set; }
        public int MaxBound { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime SendDate { get; set; }
        public string About { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public float Rating { get; set; }
        public int RatingsCount { get; set; }
        public ReturnOffersFromConsDTO(Offer offer)
        {
            this.ConstructorId = offer.ConstructorId;
            this.MinBound = offer.MinBound;
            this.MaxBound = offer.MaxBound;
            this.StartDate = offer.StartDate;
            this.SendDate = offer.SendDate;

            this.About = offer.User.About;
            this.FirstName = offer.User.FirstName;
            this.Email = offer.User.Email;
            this.PhoneNumber = offer.User.PhoneNumber;
            try
            {
                this.Rating = offer.User.Rating.Average;
                this.RatingsCount = offer.User.Rating.HowMany;
            }
            catch
            {
                this.Rating = 0;
                this.RatingsCount = 0;
            }
        }
    }
}
