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
    public class ApplicationService : IApplicationService
    {
        private readonly IHttpClientRepository _httpClient;
        //private readonly AuthenticationServices _authenticationServices;
        private readonly string POST_APPLICATION_URL = "/application/postApp";
        private readonly string GET_APPLICATION_URL = "/application/GetMyApplications";
        private readonly string GET_APPLICATION_DETAILS_URL = "/application/GetApplicationDetails";
        private readonly string GET_INSTITUTION_URL = "/applicationConfig/GetInstitution";
        private readonly string GET_STATUS_URL = "/applicationConfig/GetStatuses";
        private readonly string GET_TYPES_URL = "/applicationConfig/GetTypes";
        private readonly IAuthenticationServices _authService;
        public ApplicationService(IHttpClientRepository httpClient, IAuthenticationServices authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public async Task<bool> PostApplicationAsync(ApplicationRequestDto application)
        {
            var user = await _authService.GetLocalUser();
            application.createdBy = user.id;
            var result = await _httpClient.PostAsync(application, POST_APPLICATION_URL);
            return true;
        }
        public async Task<List<ApplicationRequestDto>> GetApplicationAsync()
        {
            string token = (await _authService.GetLocalUser()).token;
            var response = await _httpClient.GetAsync($"{GET_APPLICATION_URL}?token={token}");
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ApplicationRequestDto>>(
                              content
                              //,
                              //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                              );
                return result;
            }
            catch (Exception ex)
            {

                return new List<ApplicationRequestDto>
                {


                };
            }

        }

        public async Task<ApplicationRequestDto> GetApplicationDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{GET_APPLICATION_DETAILS_URL}?id={id}");
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
                    var result = JsonConvert.DeserializeObject<ApplicationRequestDto>(
                                  content//,
                                         //new JsonSerializerOptions { PropertyNameCaseInsensitive = true}
                                         // options
                                  );
                    //result.success = true;
                    return result;
                }
                else
                {
                    return new ApplicationRequestDto
                    {
                        // success = false,
                        //errorMessage = "Nuk u mundesua lidhja me serverin qendror"
                    };
                }

            }
            catch (Exception ex)
            {

                return new ApplicationRequestDto
                {
                    /// success = false,
                    //errorMessage = "Ka ndodhur nje gabim"

                };
            }
        }

        public async Task<List<InstitutionDto>> GetInstitutionDtosAsync()
        {
            var response = await _httpClient.GetAsync(GET_INSTITUTION_URL);
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var result =  JsonConvert.DeserializeObject<List<InstitutionDto>>(content);
                return result.ToList();
            }
            catch (Exception)
            {

                return new List<InstitutionDto>();
            }
        }

        public async Task<List<AppStatus>> GetAppStatusesAsync()
        {
            var response = await _httpClient.GetAsync(GET_STATUS_URL);
            try
            {

                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<AppStatus>>(content);
                //var result = JsonSerializer.Deserialize<List<AppStatus>>(
                //              content,
                //              new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );
                return result;
            }
            catch (Exception)
            {
                return new List<AppStatus>();
            }
        }

        public async Task<List<TypeDto>> GetTypeDtosAsync()
        {
            var response = await _httpClient.GetAsync(GET_TYPES_URL);
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<List<TypeDto>>(content);
                //var result = JsonSerializer.Deserialize<List<TypeDto>>(
                //             content,
                //              new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                //              );

                return result;
            }
            catch (Exception)
            {

                return new List<TypeDto>();
            }
        }


    }
}
