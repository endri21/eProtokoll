using eProtokoll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<LogInResponse> Login(LoginRequest dto);
        void Logout();
        Task<string> GetAuthenticationToken();

        Task<UsersDto> GetLoginUserAsync();
        void StoreLocalUser(UsersDto users);
        Task<UsersDto> GetLocalUser();
    }
}
