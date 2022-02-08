using eProtokoll.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IApplicationService
    {
        Task<bool> PostApplicationAsync(ApplicationRequestDto application);
        Task<List<ApplicationRequestDto>> GetApplicationAsync();
        Task<UserApplicationsRequestDto> GetApplicationDetailsAsync(int id);
        Task<bool> OpenNotification(int id);
        Task<List<InstitutionDto>> GetInstitutionDtosAsync();
        Task<List<AppStatus>> GetAppStatusesAsync();
        Task<List<TypeDto>> GetTypeDtosAsync();
        Task<Response> RefuseApplicationAsync(UserApplicationsRequestDto requestDto);
        Task<Response> PassInNextStepAsync(UserApplicationsRequestDto dto);
        Task<List<ApplicationHistory>> GetApplicationHistoryAsync(int appId);
        Task<List<ApplicationRequestDto>> GetFinishedApplicationAsync();

    }
}
