namespace WiseSwitch.Services
{
    public class DataService
    {
        private readonly ApiService _apiService;

        private const string _apiBrands = "api/Brands";
        private const string _apiFirmwareVersions = "api/FirmwareVersions";
        private const string _apiManufacturers = "api/Manufacturers";
        private const string _apiProductLines = "api/ProductLines";
        private const string _apiProductSeries = "api/ProductSeries";
        private const string _apiSwitchModels = "api/SwitchModels";

        public DataService(ApiService apiService)
        {
            _apiService = apiService;
        }


        // Get data.
        public async Task<T> GetDataAsync<T>(string dataOperation, object value)
        {
            return dataOperation switch
            {
                // Brand.
                DataOperations.GetAllBrandsOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/All"),
                DataOperations.GetComboBrands => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Combo"),
                DataOperations.GetDisplayBrand => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Display/" + (int)value),
                DataOperations.GetEditModelBrand => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/EditModel/" + (int)value),
                DataOperations.GetExistsBrand => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Exists/" + (int)value),
                DataOperations.GetModelBrand => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Model/" + (int)value),
                // Firmware Version.
                DataOperations.GetAllFirmwareVersionsOrderByVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/All"),
                DataOperations.GetComboFirmwareVersions => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Combo"),
                DataOperations.GetDisplayFirmwareVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Display/" + (int)value),
                DataOperations.GetEditModelFirmwareVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/EditModel/" + (int)value),
                DataOperations.GetExistsFirmwareVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Exists/" + (int)value),
                DataOperations.GetModelFirmwareVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Model/" + (int)value),
                // Manufacturer.
                DataOperations.GetAllManufacturersOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/All"),
                DataOperations.GetComboManufacturers => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Combo"),
                DataOperations.GetDisplayManufacturer => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Display/" + (int)value),
                DataOperations.GetEditModelManufacturer => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/EditModel/" + (int)value),
                DataOperations.GetExistsManufacturer => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Exists/" + (int)value),
                DataOperations.GetModelManufacturer => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Model/" + (int)value),
                // Product Line.
                DataOperations.GetAllProductLinesOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/All"),
                DataOperations.GetBrandIdOfProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/GetBrandIdOfProductline/" + (int)value),
                DataOperations.GetComboProductLines => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Combo"),
                DataOperations.GetComboProductLinesOfBrand => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/ComboProductLinesOfBrand/" + (int)value),
                DataOperations.GetDisplayProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Display/" + (int)value),
                DataOperations.GetEditModelProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/EditModel/" + (int)value),
                DataOperations.GetExistsProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Exists/" + (int)value),
                DataOperations.GetModelProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Model/" + (int)value),
                // Product Series.
                DataOperations.GetAllProductSeriesOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/All"),
                DataOperations.GetComboProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Combo"),
                DataOperations.GetComboProductSeriesOfProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/ComboProductSeriesOfProductLine/" + (int)value),
                DataOperations.GetDependencyChainIdsOfProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/IdsOfDependencyChain/" + (int)value),
                DataOperations.GetDisplayProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Display/" + (int)value),
                DataOperations.GetEditModelProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/EditModel/" + (int)value),
                DataOperations.GetExistsProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Exists/" + (int)value),
                DataOperations.GetModelProductSeries => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Model/" + (int)value),
                // Switch Model.
                DataOperations.GetAllSwitchModelsOrderByModelName => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/All"),
                DataOperations.GetComboSwitchModels => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Combo"),
                DataOperations.GetDisplaySwitchModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Display/" + (int)value),
                DataOperations.GetEditModelSwitchModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/EditModel/" + (int)value),
                DataOperations.GetExistsSwitchModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Exists/" + (int)value),
                DataOperations.GetModelSwitchModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Model/" + (int)value),

                _ => throw new InvalidOperationException(dataOperation),
            };
        }


        // Post data.
        public async Task PostDataAsync(string dataOperation, object value)
        {
            var posted = dataOperation switch
            {
                DataOperations.CreateBrand => await _apiService.PostDataToApiAsync(_apiBrands + "/Create", value),
                DataOperations.CreateFirmwareVersion => await _apiService.PostDataToApiAsync(_apiFirmwareVersions + "/Create", value),
                DataOperations.CreateManufacturer => await _apiService.PostDataToApiAsync(_apiManufacturers + "/Create", value),
                DataOperations.CreateProductLine => await _apiService.PostDataToApiAsync(_apiProductLines + "/Create", value),
                DataOperations.CreateProductSeries => await _apiService.PostDataToApiAsync(_apiProductSeries + "/Create", value),
                DataOperations.CreateSwitchModel => await _apiService.PostDataToApiAsync(_apiSwitchModels + "/Create", value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Put data.
        public async Task PutDataAsync(string dataOperation, object value)
        {
            var putted = dataOperation switch
            {
                DataOperations.UpdateBrand => await _apiService.PutDataToApiAsync(_apiBrands + "/Update", value),
                DataOperations.UpdateFirmwareVersion => await _apiService.PutDataToApiAsync(_apiFirmwareVersions + "/Update", value),
                DataOperations.UpdateManufacturer => await _apiService.PutDataToApiAsync(_apiManufacturers + "/Update", value),
                DataOperations.UpdateProductLine => await _apiService.PutDataToApiAsync(_apiProductLines + "/Update", value),
                DataOperations.UpdateProductSeries => await _apiService.PutDataToApiAsync(_apiProductSeries + "/Update", value),
                DataOperations.UpdateSwitchModel => await _apiService.PutDataToApiAsync(_apiSwitchModels + "/Update", value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Delete data.
        public async Task<object> DeleteDataAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                DataOperations.DeleteBrand => await _apiService.DeleteDataFromApiAsync(_apiBrands + "/Delete/" + (int)value),
                DataOperations.DeleteFirmwareVersion => await _apiService.DeleteDataFromApiAsync(_apiFirmwareVersions + "/Delete/" + (int)value),
                DataOperations.DeleteManufacturer => await _apiService.DeleteDataFromApiAsync(_apiManufacturers + "/Delete/" + (int)value),
                DataOperations.DeleteProductLine => await _apiService.DeleteDataFromApiAsync(_apiProductLines + "/Delete/" + (int)value),
                DataOperations.DeleteProductSeries => await _apiService.DeleteDataFromApiAsync(_apiProductSeries + "/Delete/" + (int)value),
                DataOperations.DeleteSwitchModel => await _apiService.DeleteDataFromApiAsync(_apiSwitchModels + "/Delete/" + (int)value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }
    }
}
