using ConAPI.Models.Constants;
using ConAPI.Models.DTOs;
using ConAPI.Models.Entities;
using ConAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ConAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryWrapper _repository;
        public UserService (UserManager<User> userManager, IRepositoryWrapper repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        private SecurityToken GenerateJwtToken(SymmetricSecurityKey signinKey, User user,
            List<string> roles, JwtSecurityTokenHandler tokenHandler, string jti)
        {
            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            });

            foreach(var role in roles)      //pt fiecare rol pe care-l are utilizatorul, adaugam un claim in subject
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }


        public async Task<ReturnLoginDTO> LoginUser(LoginUserDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email);
            var check = await _userManager.CheckPasswordAsync(user, dto.Password);

            ReturnLoginDTO rLDTO= new ReturnLoginDTO();
            if(user != null && check == true)
            {
                user = await _repository.User.GetByIdWithRoles(user.Id);
                List<string> roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

                var newJti = Guid.NewGuid().ToString();
                var tokenHandler = new JwtSecurityTokenHandler();
                var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    ("Parola asta trebuie sa fie lunga lfc1905"));

                var token = GenerateJwtToken(signinkey, user, roles, tokenHandler, newJti);
                _repository.SessionToken.Create(new SessionToken(newJti, user.Id, token.ValidTo));

                await _repository.SaveAsync();
                rLDTO.Id = user.Id;
                rLDTO.Roles = roles;
                rLDTO.Token = tokenHandler.WriteToken(token);
                return rLDTO;
            }

            return rLDTO;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDTO dto)
        {
            var registerUser = new User();

            registerUser.Email = dto.Email;
            registerUser.UserName = dto.Email;
            registerUser.FirstName = dto.FirstName;
            registerUser.LastName = dto.LastName;
            registerUser.PhoneNumber = dto.PhoneNumber;

            var result = await _userManager.CreateAsync(registerUser, dto.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.User);
                return true;
            }
            return false;
        }


        public async Task<bool> RegisterConstructorAsync(RegisterConstructorDTO dto)
        {
            var registerUser = new User();

            registerUser.Email = dto.Email;
            registerUser.UserName = dto.Email;
            registerUser.FirstName = dto.FirstName;
            registerUser.PhoneNumber = dto.PhoneNumber;
            registerUser.About = dto.About;

            var result = await _userManager.CreateAsync(registerUser, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, UserRoleType.Constructor);
                return true;
            }
            return false;
        }
    }
}
