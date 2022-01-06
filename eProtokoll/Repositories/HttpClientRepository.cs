using eProtokoll.Dto;
using eProtokoll.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace eProtokoll.Repositories
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient client;
       

        public HttpClientRepository(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri(Urls.BASE_URL);
            client.DefaultRequestHeaders
                .Accept.Clear();
            client.DefaultRequestHeaders
                .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         
        }

        public void AuthorizationAsync(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

    

        public  void EmptyDefaultRequestHeadersAsync()
        {
            client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                return await client.GetAsync(url);
            }
            catch (Exception)
            {

                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
        }
       
        public async Task<HttpResponseMessage> PostAsync(object obj, string url)
        {
            try
            {
                return await client.PostAsJsonAsync(url, obj);
            }
            catch (Exception)
            {
                return new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent
                };
            }


        }
    }
}
