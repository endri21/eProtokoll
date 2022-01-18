using eProtokoll.Dto;
using eProtokoll.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IHttpClientRepository _clientRepository;

        public UserRepository(IHttpClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        private readonly string REGISTER_URL = "auth/register";
        private readonly string GET_USERS_BY_ROLE = "usersmanage/GetUsersByRole";
        private readonly string GET_USER = "usersmanage/GetUserById";
        private readonly string ACTIVATE_USER = "usersmanage/ActivateUser";
        private readonly string GET_ROLES = "UsersManage/GetRoles";
        private readonly string GET_ALL_USERS = "UsersManage/GetUsers";
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest register)
        {
            try
            {
                var response = await _clientRepository.PostAsync(register, REGISTER_URL);
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<RegisterResponse>(
                      content
                      //,
                      //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                      );
                return result;

            }
            catch (Exception)
            {

                return new RegisterResponse { };
            }

        }

        public async Task<List<UsersDto>> GetUsersByRoleAsync(string role)
        {
            var response = await _clientRepository.GetAsync($"{GET_USERS_BY_ROLE}?role={role}");
            var content = response.Content.ReadAsStringAsync().Result;


            var result = JsonConvert.DeserializeObject<List<UsersDto>>(content);
            return result;
        }
        public async Task<List<UsersDto>> GetUsersAsync()
        {
            var responseAsync = await _clientRepository.GetAsync(GET_ALL_USERS);
            try
            {
                var response = responseAsync;
                var content = response.Content.ReadAsStringAsync().Result;


                var result = Task.Run(() => JsonConvert.DeserializeObject<List<UsersDto>>(content).ToList()).GetAwaiter().GetResult();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<UsersDto> GetUserAsyncById(int id)
        {
            var response = await _clientRepository.GetAsync($"{GET_USER}?id={id}");
            try
            {

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true,
                        WriteIndented = true
                    };
                    var result = JsonConvert.DeserializeObject<UsersDto>(
                                  content
                                  //,
                                  //new JsonSerializerOptions { PropertyNameCaseInsensitive = true}
                                  //options
                                  );
                    result.success = true;
                    return result;
                }
                else
                {
                    return new UsersDto
                    {
                        success = false,
                        errorMessage = "Nuk u mundesua lidhja me serverin qendror"
                    };
                }

            }
            catch (Exception ex)
            {

                return new UsersDto
                {
                    success = false,
                    errorMessage = "Ka ndodhur nje gabim"

                };
            }
        }

        public Task<bool> ActivateUsersAsync(List<int> id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ActivateUserAsync(int id)
        {
            var response = await _clientRepository.PostAsync(id, $"{ACTIVATE_USER}?id={id}");
            try
            {

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        IgnoreReadOnlyProperties = true,
                        WriteIndented = true
                    };
                    var result = JsonConvert.DeserializeObject<bool>(content);

                    return result;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            var response = await _clientRepository.GetAsync(GET_ROLES);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<RoleDto>>(content);
                return result;
            }
            else
            {
                return new List<RoleDto>();
            }

        }
    }
}
