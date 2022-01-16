using eProtokoll.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IApplicationConfigurationService
    {
      string GetApplicationAproveButtonByRole(string role, int appId);
      string GetApplicationRefuseButtonByRole(string role, int appId);
    }
}
