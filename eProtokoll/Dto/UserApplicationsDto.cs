using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class UserApplicationsRequestDto
    {
        public int id { get; set; }
        public ApplicationRequestDto applicationDto { get; set; }
        public string comment { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public List<DocumentDto> documentDtos { get; set; }
        public bool finished { get; set; }
    }
}
