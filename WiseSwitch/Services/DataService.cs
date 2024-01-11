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
            return await _apiService.GetDataAsync<T>(url + value);
        }


        // Post data.
        public async Task CreateAsync(string url, object value)
        {
            await _apiService.PostDataAsync(url, value);
        }


        // Put data.
        public async Task UpdateAsync(string url, object value)
        {
            await _apiService.PutDataAsync(url, value);
        }


        // Delete data.
        public async Task<object> DeleteAsync(string url, object value)
        {
            return await _apiService.DeleteDataAsync(url + value);
        }
    }
}
