using eProtokoll.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetNotifications();
        Task<bool> ReadNotificationAsync(int id);
    }

}
