using eProtokoll.Dto;
using eProtokoll.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class NotificationService : INotificationService
    {
        private readonly IHttpClientRepository _httpClient;

        public NotificationService(IHttpClientRepository clientRepository)
        {
            _httpClient = clientRepository;
        }
        private readonly string GET_NOTIFICATION = "notifications/GetNotifications";
        private readonly string READ_NOTIFICATION = "notifications/ReadNotification";
        public async Task<List<NotificationDto>> GetNotifications()
        {
            var response = await _httpClient.GetAsync($"{GET_NOTIFICATION}");
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<NotificationDto>>(content) ?? new List<NotificationDto>();
            }
            catch (Exception)
            {
                return new List<NotificationDto>
                {
                };
            }
        }

        public async Task<bool> ReadNotificationAsync(int id)
        {
            var response = await _httpClient.PostAsync(id,$"{READ_NOTIFICATION}?id={id}");
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
