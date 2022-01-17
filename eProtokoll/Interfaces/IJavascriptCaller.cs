using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IJavascriptCaller
    {
        Task Print(int c);
        Task Collapse();
        Task Show(string id);
        Task Hide(string id);
        Task DownloadFile(string name,MemoryStream vs);
    }
}
