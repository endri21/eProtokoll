using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class RegisterRequest
    {
        public string username { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public PhotoViewModel photo { get; set; }
    }
}
