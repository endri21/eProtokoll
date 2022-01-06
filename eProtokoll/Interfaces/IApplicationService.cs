using eProtokoll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IApplicationService
    {
        Task<bool> PostApplicationAsync(ApplicationRequestDto application);
        Task<List<ApplicationRequestDto>> GetApplicationAsync();
        Task<ApplicationRequestDto> GetApplicationDetailsAsync(int id);
        Task<List<InstitutionDto>> GetInstitutionDtosAsync();
        Task<List<AppStatus>> GetAppStatusesAsync();
        Task<List<TypeDto>> GetTypeDtosAsync();
    }
}
