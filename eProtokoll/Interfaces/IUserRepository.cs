using eProtokoll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest register);
        Task<List<UsersDto>> GetUsersAsync();
        Task<UsersDto> GetUserAsyncById(int id);
        Task<bool> ActivateUsersAsync(List<int> id);
        Task<bool> ActivateUserAsync(int id);
    }
}
