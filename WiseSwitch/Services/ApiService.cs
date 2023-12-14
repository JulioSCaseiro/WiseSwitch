using System.Net.Http.Headers;
using Newtonsoft.Json;
using WiseSwitch.Exceptions.ApiServiceExceptions;

namespace WiseSwitch.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseAddress = "https://localhost:7179/";

        public ApiService()
        {
            _httpClient = new HttpClient();
            ApiServiceConfiguration();
        }

        private void ApiServiceConfiguration()
        {
            _httpClient.BaseAddress = new Uri(_apiBaseAddress);

            // Headers.
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set timeout for the request.
            _httpClient.Timeout = TimeSpan.FromSeconds(30);

        }

        public async Task<T> GetDataFromApiAsync<T>(string apiEndpoint)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {
                throw new CouldNotGetDataException();
            }

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(jsonstring);

                    // Success.
                    return result;
                }
                catch (Exception)
                {
                    throw new CouldNotDeserializeJsonObjectException();
                }
            }

            throw new ApiResponseNotSuccessfulException();
        }

        public async Task<object> PostDataToApiAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PostAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }

        public async Task<object> PutDataToApiAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.PutAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }

        public async Task<object> DeleteDataFromApiAsync(string apiEndpoint)
        {
            HttpResponseMessage response;

            try
            {
                response = await _httpClient.DeleteAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }
    }
}
