using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace empClientApp.Services
{
    public class TokenManager : ITokenManager
    {
        public async Task<string> GenerateToken(string Email)
        {
            string result = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Settings.RestApiUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync($"api/Token/Auhenticate/?Email={Settings.RestApiUrl}");
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {

            }
            return result;

        }
    }
}
