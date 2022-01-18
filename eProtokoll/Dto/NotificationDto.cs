using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Dto
{
    public class NotificationDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public int idOfAction { get; set; }
    }
}
