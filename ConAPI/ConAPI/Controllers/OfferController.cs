using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using ConAPI.Repositories;
using ConAPI.Services.EmailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public OfferController(IRepositoryWrapper repository, UserManager<User> userManager,
            IEmailService emailService)
        {
            _repository = repository;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpGet("getAccepted")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> GetAcceptedOffers()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            var offers = await _repository.Offer.GetAcceptedOffersByUser(user);
            var offersToReturn = new List<AcceptedOfferDTO>();
            foreach (var o in offers)
            {
                offersToReturn.Add(new AcceptedOfferDTO(o));
            }

            return Ok(offersToReturn);
        }


        [HttpGet("getByProject/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> GetOffersForProject(int id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);
            Project project = await _repository.Project.GetByIdAsync(id);
            if (project == null || project.User != user)
                return BadRequest();

            var offers = await _repository.Offer.GetAllOffersByProject(id);
            var offersToReturn = new List<ReturnOffersFromConsDTO>();
            foreach (var o in offers)
            {
                offersToReturn.Add(new ReturnOffersFromConsDTO(o));
            }

            return Ok(offersToReturn);
        }

        [HttpPost("new")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateOffer([FromBody] NewOfferDTO dto)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            Project project = await _repository.Project.GetByIdAsync(dto.ProjectId);

            Offer newOffer = new Offer();
            newOffer.MinBound = dto.MinBound;
            newOffer.MaxBound = dto.MaxBound;
            newOffer.StartDate = dto.date;
            newOffer.SendDate = DateTime.Now;
            newOffer.Project = project;
            newOffer.User = user;

            _repository.Offer.Create(newOffer);
            await _repository.SaveAsync();
            return Ok();
        }

        [HttpPut("edit/{projectId}/{constrId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> AcceptOffer(int projectId, int constrId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            // verify that the current user is the one that posted the project
            var proj = await _repository.Project.GetByIdWithUser(projectId);
            if (proj == null || proj.User.Id.ToString() != currentId)
                return NotFound("No such project!");

            var off = await _repository.Offer.GetByMultipleKey(constrId, projectId);
            if (off == null)
                return NotFound("No offer.");

            off.Status = 1;
            _repository.Offer.Update(off);

            proj.Done = true;
            _repository.Project.Update(proj);

            await _repository.SaveAsync();


            // send confirmation email to constructor
            var constr = await _userManager.FindByIdAsync(constrId.ToString());
            _emailService.SendEmail(constr.Email, proj.Title, user.PhoneNumber, user.Email, user.FirstName);
            
            return NoContent();
        }


        [HttpDelete("delete/{projectId}/{constrId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> DeleteOffer(int projectId, int constrId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            // verify that the current user is the one that posted the project
            var proj = await _repository.Project.GetByIdWithUser(projectId);
            if (proj == null || proj.User.Id.ToString() != currentId)
                return NotFound("No such project!");

            var off = await _repository.Offer.GetByMultipleKey(constrId, projectId);
            if (off == null)
                return NotFound("No offer.");

            _repository.Offer.Delete(off);
            await _repository.SaveAsync();
            return NoContent();

        }
    }
}
