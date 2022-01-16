using Blazored.LocalStorage;
using eProtokoll.Dto;
using eProtokoll.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _authentication;
        private readonly IHttpClientRepository _clientRepository;

        public AuthStateProvider(ILocalStorageService localStorage, IHttpClientRepository clientRepository)
        {
            _localStorage = localStorage;
            _authentication = new AuthenticationState(
                                    new ClaimsPrincipal(
                                        new ClaimsIdentity()));
            _clientRepository = clientRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
            {
                return _authentication;
            }
            _clientRepository.AuthorizationAsync(token);

            var authUser = new ClaimsPrincipal(
              new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")
              );

            var user = new ClaimsPrincipal(authUser);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(UsersDto user)
        {
            var authUser = new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(user.token), "jwtAuthType")
                );

            var clone = authUser.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;
            var roleClaim = new Claim(newIdentity.RoleClaimType, user.role);
            var nameClaim = new Claim(newIdentity.NameClaimType, user.name);

            newIdentity.AddClaim(roleClaim);
            newIdentity.AddClaim(nameClaim);
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_authentication);
            NotifyAuthenticationStateChanged(authState);
        }
        public async void RemoveAuthenticationToken()
        {
            await _localStorage.RemoveItemAsync("authToken");
        }
    }
}
