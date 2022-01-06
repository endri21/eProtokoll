using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class LogInResponse
    {
        public string token { get; set; }
        public string username { get; set; }
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
    }
}
