using WiseSwitch.Utils;

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
        public async Task<T> GetAsync<T>(string dataOperation, object value)
        {
            return dataOperation switch
            {
                // Brand.
                DataOperations.GetBrandDisplay => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Display/" + value),
                DataOperations.GetBrandEditModel => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/EditModel/" + value),
                DataOperations.GetBrandExists => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Exists/" + value),
                DataOperations.GetBrandModel => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Model/" + value),
                DataOperations.GetBrandsCombo => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/Combo"),
                DataOperations.GetBrandsOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiBrands + "/All"),
                // Firmware Version.
                DataOperations.GetFirmwareVersionDisplay => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Display/" + value),
                DataOperations.GetFirmwareVersionEditModel => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/EditModel/" + value),
                DataOperations.GetFirmwareVersionExists => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Exists/" + value),
                DataOperations.GetFirmwareVersionModel => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Model/" + value),
                DataOperations.GetFirmwareVersionsCombo => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/Combo"),
                DataOperations.GetFirmwareVersionsOrderByVersion => await _apiService.GetDataFromApiAsync<T>(_apiFirmwareVersions + "/All"),
                // Manufacturer.
                DataOperations.GetManufacturerDisplay => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Display/" + value),
                DataOperations.GetManufacturerEditModel => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/EditModel/" + value),
                DataOperations.GetManufacturerExists => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Exists/" + value),
                DataOperations.GetManufacturerModel => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Model/" + value),
                DataOperations.GetManufacturersCombo => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/Combo"),
                DataOperations.GetManufacturersOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiManufacturers + "/All"),
                // Product Line.
                DataOperations.GetProductLineBrandId => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/GetBrandIdOfProductline/" + value),
                DataOperations.GetProductLineDisplay => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Display/" + value),
                DataOperations.GetProductLineEditModel => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/EditModel/" + value),
                DataOperations.GetProductLineExists => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Exists/" + value),
                DataOperations.GetProductLineModel => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Model/" + value),
                DataOperations.GetProductLinesCombo => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/Combo"),
                DataOperations.GetProductLinesComboOfBrand => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/ComboProductLinesOfBrand/" + value),
                DataOperations.GetProductLinesOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiProductLines + "/All"),
                // Product Series.
                DataOperations.GetProductSeriesCombo => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Combo"),
                DataOperations.GetProductSeriesComboOfProductLine => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/ComboProductSeriesOfProductLine/" + value),
                DataOperations.GetProductSeriesDependencyChainIds => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/IdsOfDependencyChain/" + value),
                DataOperations.GetProductSeriesDisplay => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Display/" + value),
                DataOperations.GetProductSeriesEditModel => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/EditModel/" + value),
                DataOperations.GetProductSeriesExists => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Exists/" + value),
                DataOperations.GetProductSeriesModel => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/Model/" + value),
                DataOperations.GetProductSeriesOrderByName => await _apiService.GetDataFromApiAsync<T>(_apiProductSeries + "/All"),
                // Switch Model.
                DataOperations.GetSwitchModelDisplay => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Display/" + value),
                DataOperations.GetSwitchModelEditModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/EditModel/" + value),
                DataOperations.GetSwitchModelExists => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Exists/" + value),
                DataOperations.GetSwitchModelModel => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Model/" + value),
                DataOperations.GetSwitchModelsCombo => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/Combo"),
                DataOperations.GetSwitchModelsOrderByModelName => await _apiService.GetDataFromApiAsync<T>(_apiSwitchModels + "/All"),

                _ => throw new InvalidOperationException(dataOperation),
            };
        }


        // Post data.
        public async Task CreateAsync(string dataOperation, object value)
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
        public async Task UpdateAsync(string dataOperation, object value)
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
        public async Task<object> DeleteAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                DataOperations.DeleteBrand => await _apiService.DeleteDataFromApiAsync(_apiBrands + "/Delete/" + value),
                DataOperations.DeleteFirmwareVersion => await _apiService.DeleteDataFromApiAsync(_apiFirmwareVersions + "/Delete/" + value),
                DataOperations.DeleteManufacturer => await _apiService.DeleteDataFromApiAsync(_apiManufacturers + "/Delete/" + value),
                DataOperations.DeleteProductLine => await _apiService.DeleteDataFromApiAsync(_apiProductLines + "/Delete/" + value),
                DataOperations.DeleteProductSeries => await _apiService.DeleteDataFromApiAsync(_apiProductSeries + "/Delete/" + value),
                DataOperations.DeleteSwitchModel => await _apiService.DeleteDataFromApiAsync(_apiSwitchModels + "/Delete/" + value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }
    }
}
