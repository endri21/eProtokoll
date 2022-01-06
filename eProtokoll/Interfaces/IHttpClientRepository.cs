using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eProtokoll.Interfaces
{
    public interface IHttpClientRepository
    {
        Task<HttpResponseMessage> PostAsync(object obj, string url);
        void AuthorizationAsync(string token);
        Task<HttpResponseMessage> GetAsync(string url);

        void EmptyDefaultRequestHeadersAsync();


    }
}
