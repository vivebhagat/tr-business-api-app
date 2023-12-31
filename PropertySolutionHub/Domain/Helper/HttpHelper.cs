﻿using Newtonsoft.Json;
using PropertySolutionHub.Domain.Entities.Shared;
using System.Text;

namespace PropertySolutionHub.Domain.Helper
{
    public interface IHttpHelper
    {
        Task<T> GetAsync<T>(string apiUrl, string domainKey);
        Task<T> PostAsync<T>(string jsonData, string domainKey, string apiUrl);
    }

    public class HttpHelper : IHttpHelper
    {
        private readonly string BaseAddress = string.Empty;
        private readonly IConfiguration _configuration;

        public HttpHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
            BaseAddress = _configuration["CustomerStoreUrl"];// "https://localhost:7281"; //
        }

        public async Task<T> GetAsync<T>(string apiUrl, string domainKey)
        {
            using (var client = new HttpClient())
            {
                var context = new HttpContextAccessor();
                string auth = context.HttpContext.Request.Headers["Authorization"];
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.3");
                client.DefaultRequestHeaders.Add("AuthKey", _configuration["AUthKey"]);
                client.DefaultRequestHeaders.Add("DomainKey", domainKey);


                HttpResponseMessage response = await client.GetAsync(BaseAddress + apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }

                return default; 
            }
        }

        public async Task<T> PostAsync<T>(string jsonData, string domainKey, string apiUrl)
        {
            using (var client = new HttpClient())
            {
                var context = new HttpContextAccessor();
                string auth = context.HttpContext.Request.Headers["Authorization"];
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.3");
                client.DefaultRequestHeaders.Add("AuthKey", _configuration["AUthKey"]);
                client.DefaultRequestHeaders.Add("DomainKey", domainKey);


                HttpResponseMessage response = await client.PostAsync(BaseAddress + apiUrl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }

                return default; 
            }
        }
    }
}
