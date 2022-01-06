using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{

    public class ApplicationRequestDto //:Response
    {
        public int appId { get; set; }
        public string appName { get; set; }
        public DateTime appDate { get; set; }
        public int createdBy { get; set; }
        public InstitutionDto institution { get; set; }
        public TypeDto type { get; set; }
        public int to { get; set; }
        public AppStatus status { get; set; }
        public DocumentDto document { get; set; }
     

    }
}
