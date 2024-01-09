using System.Net.Http.Headers;
using Newtonsoft.Json;
using WiseSwitch.Exceptions.ApiServiceExceptions;

namespace WiseSwitch.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseAddress;

        public ApiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiBaseAddress = configuration["Api:BaseAddress"];
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

        public async Task<T> GetDataAsync<T>(string apiEndpoint)
        {
            HttpResponseMessage httpResponseMessage;

            // Try get data from api.
            try
            {
                httpResponseMessage = await _httpClient.GetAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {
                throw new CouldNotGetDataException();
            }

            // If response is successful, try deserialize content.
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    var jsonstring = await httpResponseMessage.Content.ReadAsStringAsync();
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

        public async Task<object> PostDataAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                httpResponseMessage = await _httpClient.PostAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }

        public async Task<object> PutDataAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                httpResponseMessage = await _httpClient.PutAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }

        public async Task<object> DeleteDataAsync(string apiEndpoint)
        {
            HttpResponseMessage httpResponseMessage;

            try
            {
                httpResponseMessage = await _httpClient.DeleteAsync(_apiBaseAddress + apiEndpoint);
            }
            catch (Exception)
            {
                throw new ApiResponseNotSuccessfulException();
            }

            return null;
        }
    }
}
