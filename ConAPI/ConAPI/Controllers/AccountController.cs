using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using ConAPI.Repositories;
using ConAPI.Services.UserServices;
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
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IRepositoryWrapper _repository;

        public AccountController(UserManager<User> userManager, IUserService userService,
            IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _userService = userService;
            _repository = repository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.User.GetAllUsers();
            return Ok(new { users });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if(exists != null)
            {
                return BadRequest("User already registered");
            }

            var result = await _userService.RegisterUserAsync(dto);
            if(result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("register/constructor")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConstructor([FromBody] RegisterConstructorDTO dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if (exists != null)
            {
                return BadRequest("User already registered");
            }

            var result = await _userService.RegisterConstructorAsync(dto);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var token = await _userService.LoginUser(dto);

            ReturnLoginDTO checkIfNull = new ReturnLoginDTO();
            if(token.Id == checkIfNull.Id)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteAccount()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Ok();
            return BadRequest();
        }

        [HttpPut("edit/phone")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> EditPhone([FromBody] PhoneDTO dto)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentId);

            user.PhoneNumber = dto.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok();
            return BadRequest();
        }
    }
}
