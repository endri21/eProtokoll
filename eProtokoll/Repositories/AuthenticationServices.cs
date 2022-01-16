using Blazored.LocalStorage;
using eProtokoll.Dto;
using eProtokoll.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class AuthenticationServices : IAuthenticationServices
    {

        private readonly IHttpClientRepository _clientRepository;



        private readonly string AUTH_URL = "auth/login";
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        //private readonly HttpClient _httpClient;

        string token = "";
        private readonly string AUTH_USER_BY_TOKEN = "auth/GetAuthUserByToken";
        public AuthenticationServices(HttpClient httpClient,
                                     AuthenticationStateProvider authStateProvider,
                                     ILocalStorageService localStorage,
                                     IHttpClientRepository clientRepository)
        {
            //_httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _clientRepository = clientRepository;
        }

        public async Task<LogInResponse> Login(LoginRequest dto)
        {

            var response = await _clientRepository.PostAsync(dto, AUTH_URL);
            string content = string.Empty;
            try
            {

                content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return new LogInResponse
                {
                    success = false,
                    errorMessage = ex.Message,
                    errorCode = response.StatusCode.ToString(),
                    token = string.Empty
                };
            }

            var result = JsonSerializer.Deserialize<LogInResponse>(
                          content,
                          new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                          );
            if (!response.IsSuccessStatusCode)
            {
                return new LogInResponse
                {
                    success = false,
                    errorMessage = result.errorMessage,
                    errorCode = response.StatusCode.ToString(),
                    token = string.Empty
                };
            }
            await _localStorage.SetItemAsync("authToken", result.token);

            var user = await GetLoginUserAsync();

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(user);

            _clientRepository.AuthorizationAsync(token);
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.token);
            return result;

        }

        public async void Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("local_user");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
          
            _clientRepository.EmptyDefaultRequestHeadersAsync();
        }
        public async Task<string> GetAuthenticationToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }

        public async Task<UsersDto> GetLoginUserAsync()
        {

            token = await GetAuthenticationToken();

            if (!string.IsNullOrEmpty(token))
            {
                var response = await _clientRepository.GetAsync($"{AUTH_USER_BY_TOKEN}?token={token}");
                try
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<UsersDto>(
                                  content
                                  );
                    StoreLocalUser(result);
                    return result;
                }
                catch (Exception)
                {
                    return new UsersDto() { };
                }

            }
            return new UsersDto()
            {
            };
        }

        public async void StoreLocalUser(UsersDto users)
        {
            await _localStorage.SetItemAsync("local_user", users);
        }

        public async Task<UsersDto> GetLocalUser()
        {
            return await _localStorage.GetItemAsync<UsersDto>("local_user");
        }
    }
}
