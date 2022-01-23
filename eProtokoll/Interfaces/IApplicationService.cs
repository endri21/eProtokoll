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
        Task<List<InstitutionDto>> GetInstitutionDtosAsync();
        Task<List<AppStatus>> GetAppStatusesAsync();
        Task<List<TypeDto>> GetTypeDtosAsync();
        Task<bool> RefuseApplicationAsync(UserApplicationsRequestDto requestDto);
        Task<Response> PassInNextStepAsync(UserApplicationsRequestDto dto);
        Task<List<ApplicationHistory>> GetApplicationHistoryAsync(int appId);

    }
}
