using System.Net.Http.Headers;

namespace WiseSwitch.Services.Api
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
            // Headers.
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Set timeout for the request.
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<ApiResponse> GetDataAsync(string apiEndpoint)
        {
            HttpResponseMessage httpResponseMessage;

            // Try Get.
            try
            {
                httpResponseMessage = await _httpClient.GetAsync(_apiBaseAddress + apiEndpoint);
            }
            catch
            {
                return ApiErrorResponse.ApiCallFailed;
            }

            return new ApiResponse
            {
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = (int)httpResponseMessage.StatusCode,
                Content = await httpResponseMessage.Content.ReadAsStringAsync(),
            };
        }

        public async Task<ApiResponse> PostDataAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage httpResponseMessage;

            // Try Post.
            try
            {
                httpResponseMessage = await _httpClient.PostAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch
            {
                return ApiErrorResponse.ApiCallFailed;
            }

            return new ApiResponse
            {
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = (int)httpResponseMessage.StatusCode,
                Content = await httpResponseMessage.Content.ReadAsStringAsync(),
            };
        }

        public async Task<ApiResponse> PutDataAsync(string apiEndpoint, object value)
        {
            HttpResponseMessage httpResponseMessage;

            // Try Put.
            try
            {
                httpResponseMessage = await _httpClient.PutAsJsonAsync(_apiBaseAddress + apiEndpoint, value);
            }
            catch
            {
                return ApiErrorResponse.ApiCallFailed;
            }

            return new ApiResponse
            {
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = (int)httpResponseMessage.StatusCode,
                Content = await httpResponseMessage.Content.ReadAsStringAsync(),
            };
        }

        public async Task<ApiResponse> DeleteDataAsync(string apiEndpoint)
        {
            HttpResponseMessage httpResponseMessage;

            // Try Delete.
            try
            {
                httpResponseMessage = await _httpClient.DeleteAsync(_apiBaseAddress + apiEndpoint);
            }
            catch
            {
                return ApiErrorResponse.ApiCallFailed;
            }

            return new ApiResponse
            {
                IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                StatusCode = (int)httpResponseMessage.StatusCode,
                Content = await httpResponseMessage.Content.ReadAsStringAsync(),
            };
        }
    }
}
