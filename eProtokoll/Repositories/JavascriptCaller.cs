using eProtokoll.Interfaces;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class JavascriptCaller : IJavascriptCaller
    {
        private readonly IJSRuntime _jSRuntime;
        public JavascriptCaller(IJSRuntime jS)
        {
            _jSRuntime = jS;
        }

        public async Task Alert()
        {
            await _jSRuntime.InvokeVoidAsync("alet");
        }

        public async Task Collapse()
        {
            await _jSRuntime.InvokeVoidAsync("collapse");
        }

        public async Task Hide(string id)
        {
            await _jSRuntime.InvokeVoidAsync("hide", id);
        }

        public async Task Print(int c)
        {
            c++;
            await _jSRuntime.InvokeVoidAsync("logUser", c);
        }

        public async Task Show(string id)
        {
            await _jSRuntime.InvokeVoidAsync("show", id);
        }

        public async Task DownloadFile(string name, MemoryStream vs)
        {
            await _jSRuntime.InvokeVoidAsync("download", name, vs);
        }
    }
}
