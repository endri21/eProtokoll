using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class DocumentDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string base64 { get; set; }
        public string extention { get; set; }
        public byte[] bytes { get; set; }
    }
}
