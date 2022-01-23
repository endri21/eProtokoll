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
        private readonly string POST_APPLICATION_URL = "/application/postApp";
        private readonly string GET_APPLICATION_URL = "/application/GetMyApplications";
        private readonly string GET_APPLICATION_DETAILS_URL = "/application/GetApplicationDetails";
        private readonly string GET_INSTITUTION_URL = "/applicationConfig/GetInstitution";
        private readonly string GET_STATUS_URL = "/applicationConfig/GetStatuses";
        private readonly string GET_TYPES_URL = "/applicationConfig/GetTypes";
        private readonly string REFUSE_APP_URL = "/application/RefuseApplication";
        private readonly string NEXT_STEP_URL = "/application/NextStep";
        private readonly string GET_APP_HISTORY = "/application/GetApplicationHistory";

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
            string token = (await _authService.GetLocalUser())?.token;
            var response = await _httpClient.GetAsync($"{GET_APPLICATION_URL}?token={token}");
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ApplicationRequestDto>>(content) ?? new List<ApplicationRequestDto>();
            }
            catch (Exception)
            {
                return new List<ApplicationRequestDto>
                {
                };
            }

        }

        public async Task<UserApplicationsRequestDto> GetApplicationDetailsAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{GET_APPLICATION_DETAILS_URL}?id={id}");
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<UserApplicationsRequestDto>(content);
                    return result;
                }
                else
                {
                    return new UserApplicationsRequestDto
                    {
                        // success = false,
                        //errorMessage = "Nuk u mundesua lidhja me serverin qendror"
                    };
                }

            }
            catch (Exception ex)
            {

                return new UserApplicationsRequestDto
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
                //var result = JsonConvert.DeserializeObject<List<InstitutionDto>>(content);
                var result = Task.Run(() =>
                JsonConvert.DeserializeObject<List<InstitutionDto>>(content).ToList()).GetAwaiter().GetResult();
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
                var result = Task.Run(() =>
               JsonConvert.DeserializeObject<List<AppStatus>>(content).ToList()).GetAwaiter().GetResult();
                // JsonConvert.DeserializeObject<List<AppStatus>>(content);
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
                var result = Task.Run(() =>
             JsonConvert.DeserializeObject<List<TypeDto>>(content).ToList()).GetAwaiter().GetResult();
                return result;//JsonConvert.DeserializeObject<List<TypeDto>>(content);
            }
            catch (Exception)
            {
                return new List<TypeDto>();
            }
        }

        public async Task<bool> RefuseApplicationAsync(UserApplicationsRequestDto requestDto)
        {
            var response = await _httpClient.PostAsync(requestDto, REFUSE_APP_URL);
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<bool>(content);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<Response> PassInNextStepAsync(UserApplicationsRequestDto dto)
        {

            var response = await _httpClient.PostAsync(dto, NEXT_STEP_URL);
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Response>(content);
            }
            catch (Exception)
            {

                return new Response()
                {
                    success = false,
                    errorMessage = "Ka ndodhur nje gabim"
                };
            }
        }

        public async Task<List<ApplicationHistory>> GetApplicationHistoryAsync(int appId)
        {
            var response = await _httpClient.GetAsync($"{GET_APP_HISTORY}?appId={appId}");
            try
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<ApplicationHistory>>(content);
            }
            catch (Exception)
            {

                return new List<ApplicationHistory>();
            }
        }
    }
}
