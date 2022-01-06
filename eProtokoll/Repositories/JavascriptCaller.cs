using eProtokoll.Interfaces;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class JavascriptCaller : IJavascriptCaller
    {
        private readonly IJSRuntime  _jSRuntime;
        public JavascriptCaller (IJSRuntime jS)
        {
            _jSRuntime = jS;
        }

        public async Task Alert()
        {
            await _jSRuntime.InvokeVoidAsync("alet");
        }
    }
}
