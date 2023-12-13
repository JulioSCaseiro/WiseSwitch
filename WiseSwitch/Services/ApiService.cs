using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WiseSwitch.Services
{
    public class ApiService
    {
        public static HttpClient _httpClient { get; private set; }
        private readonly string _apiBaseAddress = "https://localhost:7179/";

        public ApiService()
        {
            _httpClient = new HttpClient();
            ApiServiceConfiguration();
        }

        private void ApiServiceConfiguration()
        {
            _httpClient.BaseAddress = new Uri(_apiBaseAddress);

            //Headers
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set timeout for the request 
            _httpClient.Timeout = TimeSpan.FromSeconds(30);

        }

        public async Task<object> GetDataFromApiAsync<T>(string apiEndpoint)
        {
            var response = new HttpResponseMessage();
            try
            {
                response = await _httpClient.GetAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {

                throw;
            }

            if (response.IsSuccessStatusCode)
            {
                var jsonstring = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(jsonstring);

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<object> PostDataToApiAsync(string apiEndpoint, object value)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<object> PutDataToApiAsync(string apiEndpoint, object value)
        {
            try
            {
                return await _httpClient.PutAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<object> DeleteDataFromApiAsync(string apiEndpoint)
        {
            try
            {
                return await _httpClient.DeleteAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
