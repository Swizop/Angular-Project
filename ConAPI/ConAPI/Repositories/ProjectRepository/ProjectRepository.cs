using ConAPI.Models;
using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context) { }
        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.Include(p => p.User)
                .Where(p => p.Done == false).OrderByDescending(p => p.AddDate).ToListAsync();
        }

        public async Task<Project> GetByIdWithUser(int id)
        {
            return await _context.Projects.Include(p => p.User)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateProject(Project p, NewPostDTO pDTO)
        {
            p.Title = pDTO.Title;
            p.Description = pDTO.Description;
            p.Location = pDTO.Location;
            _context.Projects.Update(p);

            return;
        }
    }
}
