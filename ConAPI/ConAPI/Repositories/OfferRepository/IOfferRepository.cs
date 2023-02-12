using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<List<Offer>> GetAllOffersByProject(int id);
        Task<List<Offer>> GetAcceptedOffersByUser(User user);
        Task<Offer> GetByMultipleKey(int consId, int projId);
    }
}
