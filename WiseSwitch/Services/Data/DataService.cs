using Newtonsoft.Json;
using WiseSwitch.Services.Api;

namespace WiseSwitch.Services.Data
{
    public class DataService
    {
        private readonly ApiService _apiService;

        public DataService(ApiService apiService)
        {
            _apiService = apiService;
        }


        // Get data.
        public async Task<DataResponse> GetAsync<T>(string url, object value = null)
        {
            var apiResponse = await _apiService.GetDataAsync(url + value);

            if (apiResponse.IsSuccess)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<T>(apiResponse.Content);

                    return DataResponse.Success(result);
                }
                catch
                {
                    return DataResponse.Error("Could not deserialize JSON object from the API response.");
                }
            }

            var message = apiResponse.Content;
            var isClientError = IsClientError(apiResponse.StatusCode);

            return DataResponse.Error(message, isClientError);
        }


        // Create data.
        public async Task<DataResponse> CreateAsync(string url, object value)
        {
            var apiResponse = await _apiService.PostDataAsync(url, value);

            return new DataResponse
            {
                IsSuccess = apiResponse.IsSuccess,
                IsClientError = IsClientError(apiResponse.StatusCode),
                Message = apiResponse.Content,
            };
        }


        // Update data.
        public async Task<DataResponse> UpdateAsync(string url, object value)
        {
            var apiResponse = await _apiService.PutDataAsync(url, value);

            return new DataResponse
            {
                IsSuccess = apiResponse.IsSuccess,
                IsClientError = IsClientError(apiResponse.StatusCode),
                Message = apiResponse.Content,
            };
        }


        // Delete data.
        public async Task<DataResponse> DeleteAsync(string url, object value)
        {
            var apiResponse = await _apiService.DeleteDataAsync(url + value);

            return new DataResponse
            {
                IsSuccess = apiResponse.IsSuccess,
                IsClientError = IsClientError(apiResponse.StatusCode),
                Message = apiResponse.Content,
            };
        }


        private bool IsClientError(int statusCode)
        {
            return statusCode >= 400 && statusCode <= 499;
        }
    }
}
