using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class Response
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
    }
}
