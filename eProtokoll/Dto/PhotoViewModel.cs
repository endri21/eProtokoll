using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class PhotoViewModel
    {
        public byte[] Photo { get; set; }
        public string PhotoName { get; set; }
        public string PhotoExtension { get; set; }
    }
}
