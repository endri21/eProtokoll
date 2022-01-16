using eProtokoll.Dto;
using eProtokoll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        public string GetApplicationRefuseButtonByRole(string role, int appId)
        {
            string refuse = "";
            switch (role)
            {
                case "Menaxher":
                    refuse = $"< input type = \"button\" @onclick = \"@(() => ShowRefuzeApplication({appId}))\" class=\"btn btn-primary\" value=\"Kthe per shqyrtim\" />";
                    break;
                case "Drejtor":
                    refuse = $"< input type = \"button\" @onclick = \"@(() => ShowRefuzeApplication({appId}))\" class=\"btn btn-primary\" value=\"Kthe per shqyrtim\" />";
                    break;
                case "Punojes":
                    refuse = $"";
                    break;
            }
            return refuse;
        }

        public string GetApplicationAproveButtonByRole(string role, int appId)
        {
            string aprove = "";
            switch (role)
            {
                case "Menaxher":
                    aprove = $"< input type = \"button\" @onclick = \"@(() => ShowAproveMovie({appId}))\" class=\"btn btn-primary\" value=\"Kalo ne hapin tjeter\" />";
                    break;
                case "Drejtor":
                    aprove = $"< input type = \"button\" @onclick = \"@(() => ShowAproveMovie({appId}))\" class=\"btn btn-primary\" value=\"Aprovo aplikimin\" />";
                    break;
                case "Punojes":
                    aprove = $"< input type = \"button\" @onclick = \"@(() => ShowAproveMovie({appId}))\" class=\"btn btn-primary\" value=\"Ridergo aplikimin\" />";
                    break;
            }

            return aprove;
        }

    }
}
