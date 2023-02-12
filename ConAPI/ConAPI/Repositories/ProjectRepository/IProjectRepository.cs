using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetAllProjects(); // get all implementat in generic repository nu extrage
                                             // si proprietatile de navigare pentru useri
        Task<Project> GetByIdWithUser(int id);
        void UpdateProject(Project p, NewPostDTO pDTO);
    }
}
