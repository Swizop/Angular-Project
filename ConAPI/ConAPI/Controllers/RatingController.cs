using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using ConAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public RatingController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetReviewByCons(int id)
        {
            var rating = await _repository.Rating.GetByConsId(id);
            var rDTO = new ReturnRatingDTO();
            if(rating == null)
            {
                rDTO.Average = 0;
                rDTO.ConstructorId = 0;
            }
            else
            {
                rDTO.Average = rating.Average;
                rDTO.ConstructorId = rating.UserId;
            }
            return Ok(rDTO);
        }

        [HttpPut("{id}/{nr}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<IActionResult> UpdateReview(int id, int nr)
        {
            var rating = await _repository.Rating.GetByConsId(id);
            var constr = await _repository.User.GetByIdAsync(id);
            if (constr == null)
                return BadRequest();

            if (rating == null)
            {
                var newRating = new Rating();
                newRating.HowMany = 1;
                newRating.Average = nr;
                newRating.User = constr;
                _repository.Rating.Create(newRating);
            }
            else
            {
                rating.Average = (rating.Average * rating.HowMany + nr) / (rating.HowMany + 1);
                rating.HowMany++;
                _repository.Rating.Update(rating);
            }
            await _repository.SaveAsync();
            return NoContent();
        }

    }
}
