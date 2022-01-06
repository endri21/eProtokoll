using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class ListOfApplicationConfigsDto
    {
        public List<InstitutionDto> ListOfInstitution { get; set; }
        public List<TypeDto> ListOfTypes { get; set; }
        public List<AppStatus> ListOfStatus { get; set; }
    }
}
