namespace WiseSwitch.Services
{
    public class DataService
    {
        private readonly ApiService _apiService;

        public DataService(ApiService apiService)
        {
            _apiService = apiService;
        }


        // Get data.
        public async Task<T> GetAsync<T>(string url, object value)
        {
            return await _apiService.GetDataFromApiAsync<T>(url + value);
        }


        // Post data.
        public async Task CreateAsync(string url, object value)
        {
            await _apiService.PostDataToApiAsync(url, value);
        }


        // Put data.
        public async Task UpdateAsync(string url, object value)
        {
            await _apiService.PutDataToApiAsync(url, value);
        }


        // Delete data.
        public async Task<object> DeleteAsync(string url, object value)
        {
            return await _apiService.DeleteDataFromApiAsync(url + value);
        }
    }
}
