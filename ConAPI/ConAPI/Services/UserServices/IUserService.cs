using ConAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDTO dto);
        Task<bool> RegisterConstructorAsync(RegisterConstructorDTO dto);
        Task<ReturnLoginDTO> LoginUser(LoginUserDTO dto);
    }
}
