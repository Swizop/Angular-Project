using ConAPI.Models;
using ConAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(AppDbContext context) : base(context) { }

        public async Task<Rating> GetByConsId(int id)
        {
            return await _context.Ratings.Where(r => r.UserId == id).FirstOrDefaultAsync();
        }
    }
}
