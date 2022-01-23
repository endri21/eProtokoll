using System;
using System.Collections.Generic;

namespace eProtokoll.Dto
{

    public class ApplicationRequestDto //:Response
    {
        public int appId { get; set; }
        public string appName { get; set; }
        public int userAppId { get; set; }
        public DateTime appDate { get; set; } = DateTime.Now;
        public int createdBy { get; set; }
        public InstitutionDto institution { get; set; }
        public TypeDto type { get; set; }
        public int to { get; set; }
        public string status { get; set; }
        public List<DocumentDto> documentDtos { get; set; }
        public DateTime appReviewDate { get; set; } = DateTime.Now;
    }
}
