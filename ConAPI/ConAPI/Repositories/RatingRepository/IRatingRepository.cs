using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<Rating> GetByConsId(int id);
    }
}
