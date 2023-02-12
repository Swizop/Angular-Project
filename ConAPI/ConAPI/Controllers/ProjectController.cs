using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using ConAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        public ProjectController(IRepositoryWrapper repository,
            UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProjects()
        {
            var pr = await _repository.Project.GetAllProjects();
            var prToReturn = new List<ReturnProjectDTO>();
            foreach (var p in pr)
            {
                prToReturn.Add(new ReturnProjectDTO(p));
            }

            return Ok(prToReturn);
        }


        [HttpPost("new")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> CreateProject([FromBody] NewPostDTO dto)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            Project newProject = new Project();
            newProject.Title = dto.Title;
            newProject.Description = dto.Description;
            newProject.Location = dto.Location;
            newProject.AddDate = DateTime.Now;
            newProject.Done = false;
            newProject.User = user;

            _repository.Project.Create(newProject);
            await _repository.SaveAsync();
            return Ok(new ReturnProjectDTO(newProject));
        }


        [HttpDelete("delete/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var proj = await _repository.Project.GetByIdWithUser(id);
            if (proj == null || proj.User.Id.ToString() != currentId)
                return NotFound("No such project!");

            _repository.Project.Delete(proj);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpPut("edit/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> EditProject(int id, [FromBody] NewPostDTO dto)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var proj = await _repository.Project.GetByIdWithUser(id);
            if (proj == null || proj.User.Id.ToString() != currentId)
                return NotFound("No such project!");

            _repository.Project.UpdateProject(proj, dto);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
