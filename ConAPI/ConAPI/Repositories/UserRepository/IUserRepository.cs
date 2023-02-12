using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);

        Task<User> GetByIdWithRoles(int id);
    }
}
