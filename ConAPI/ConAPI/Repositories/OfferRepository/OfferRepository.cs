using ConAPI.Models;
using ConAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public OfferRepository(AppDbContext context) : base(context) { }

        public async Task<List<Offer>> GetAcceptedOffersByUser(User user)
        {
            return await _context.Offers.Include(off => off.User).Include(off => off.Project)
                .Where(off => off.Project.User == user && off.Status == 1)
                .OrderBy(off => off.ConstructorId).ToListAsync();
        }

        public async Task<List<Offer>> GetAllOffersByProject(int id)
        {
            return await _context.Offers.Include(off => off.User).Include(off => off.User.Rating)
                .Where(off => off.ProjectId == id
                && off.Status == 0).OrderByDescending(off => off.SendDate).ToListAsync();
        }

        public async Task<Offer> GetByMultipleKey(int consId, int projId)
        {
            return await _context.Offers.Where(off => off.ConstructorId == consId
                && off.ProjectId == projId).FirstOrDefaultAsync();
        }
    }
}
