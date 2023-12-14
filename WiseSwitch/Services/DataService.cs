using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Dtos;
using WiseSwitch.ViewModels.Entities.Brand;
using WiseSwitch.ViewModels.Entities.FirmwareVersion;
using WiseSwitch.ViewModels.Entities.Manufacturer;
using WiseSwitch.ViewModels.Entities.ProductLine;
using WiseSwitch.ViewModels.Entities.ProductSeries;
using WiseSwitch.ViewModels.Entities.SwitchModel;

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
        public async Task<object> GetDataAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                // Brand.
                DataOperations.GetAllBrandsOrderByName => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowBrandViewModel>>("api/Brands/All"),
                DataOperations.GetComboBrands => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/Brands/Combo"),
                DataOperations.GetDisplayBrand => await _apiService.GetDataFromApiAsync<DisplayBrandViewModel>($"api/Brands/Display/{(int)value}"),
                DataOperations.GetExistsBrand => await _apiService.GetDataFromApiAsync<bool>($"api/Brands/Exists/{(int)value}"),
                DataOperations.GetModelBrand => await _apiService.GetDataFromApiAsync<EditBrandViewModel>($"api/Brands/Model/{(int)value}"),
                // Firmware Version.
                DataOperations.GetAllFirmwareVersionsOrderByVersion => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowFirmwareVersionViewModel>>("api/FirmwareVersions/All"),
                DataOperations.GetComboFirmwareVersions => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/FirmwareVersions/Combo"),
                DataOperations.GetDisplayFirmwareVersion => await _apiService.GetDataFromApiAsync<DisplayFirmwareVersionViewModel>($"api/FirmwareVersions/Display/{(int)value}"),
                DataOperations.GetExistsFirmwareVersion => await _apiService.GetDataFromApiAsync<bool>($"api/FirmwareVersions/Exists/{(int)value}"),
                DataOperations.GetModelFirmwareVersion => await _apiService.GetDataFromApiAsync<EditFirmwareVersionViewModel>($"api/FirmwareVersions/Model/{(int)value}"),
                // Manufacturer.
                DataOperations.GetAllManufacturersOrderByName => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowManufacturerViewModel>>("api/Manufacturers/All"),
                DataOperations.GetComboManufacturers => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/Manufacturers/Combo"),
                DataOperations.GetDisplayManufacturer => await _apiService.GetDataFromApiAsync<DisplayManufacturerViewModel>($"api/Manufacturers/Display/{(int)value}"),
                DataOperations.GetExistsManufacturer => await _apiService.GetDataFromApiAsync<bool>($"api/Manufacturers/Exists/{(int)value}"),
                DataOperations.GetModelManufacturer => await _apiService.GetDataFromApiAsync<EditManufacturerViewModel>($"api/Manufacturers/Model/{(int)value}"),
                // Product Line.
                DataOperations.GetAllProductLinesOrderByName => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowProductLineViewModel>>("api/ProductLines/All"),
                DataOperations.GetComboProductLines => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/ProductLines/Combo"),
                DataOperations.GetDisplayProductLine => await _apiService.GetDataFromApiAsync<DisplayProductLineViewModel>($"api/ProductLines/Display/{(int)value}"),
                DataOperations.GetExistsProductLine => await _apiService.GetDataFromApiAsync<bool>($"api/ProductLines/Exists/{(int)value}"),
                DataOperations.GetModelProductLine => await _apiService.GetDataFromApiAsync<EditProductLineViewModel>($"api/ProductLines/Model/{(int)value}"),
                DataOperations.GetBrandIdOfProductLine => await _apiService.GetDataFromApiAsync<int>($"api/ProductLines/GetBrandIdOfProductline/{(int)value}"),
                DataOperations.GetComboProductLinesOfBrand => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>($"api/ProductLines/ComboProductLinesOfBrand/{(int)value}"),
                // Product Series.
                DataOperations.GetAllProductSeriesOrderByName => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowProductSeriesViewModel>>("api/ProductSeries/All"),
                DataOperations.GetComboProductSeries => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/ProductSeries/Combo"),
                DataOperations.GetDependencyChainIdsOfProductSeries => await _apiService.GetDataFromApiAsync<ProductSeriesDependencyChainIds>($"api/ProductSeries/IdsOfDependencyChain/{(int)value}"),
                DataOperations.GetDisplayProductSeries => await _apiService.GetDataFromApiAsync<DisplayProductSeriesViewModel>($"api/ProductSeries/Display/{(int)value}"),
                DataOperations.GetExistsProductSeries => await _apiService.GetDataFromApiAsync<bool>($"api/ProductSeries/Exists/{(int)value}"),
                DataOperations.GetModelProductSeries => await _apiService.GetDataFromApiAsync<EditProductSeriesViewModel>($"api/ProductSeries/Model/{(int)value}"),
                DataOperations.GetComboProductSeriesOfProductLine => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>($"api/ProductSeries/ComboProductSeriesOfProductLine/{(int)value}"),
                // Switch Model.
                DataOperations.GetAllSwitchModelsOrderByModelName => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowSwitchModelViewModel>>("api/SwitchModels/All"),
                DataOperations.GetComboSwitchModels => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/SwitchModels/Combo"),
                DataOperations.GetDisplaySwitchModel => await _apiService.GetDataFromApiAsync<DisplaySwitchModelViewModel>($"api/SwitchModels/Display/{(int)value}"),
                DataOperations.GetEditModelSwitchModel => await _apiService.GetDataFromApiAsync<EditSwitchModelViewModel>($"api/SwitchModels/EditModel/{(int)value}"),
                DataOperations.GetExistsSwitchModel => await _apiService.GetDataFromApiAsync<bool>($"api/SwitchModels/Exists/{(int)value}"),
                DataOperations.GetModelSwitchModel => await _apiService.GetDataFromApiAsync<EditSwitchModelViewModel>($"api/SwitchModels/Model/{(int)value}"),

                _ => throw new InvalidOperationException(dataOperation),
            };
        }


        // Post data.
        public async Task PostDataAsync(string dataOperation, object value)
        {
            var posted = dataOperation switch
            {
                DataOperations.CreateBrand => await _apiService.PostDataToApiAsync("api/Brands/Create", value),
                DataOperations.CreateFirmwareVersion => await _apiService.PostDataToApiAsync("api/FirmwareVersions/Create", value),
                DataOperations.CreateManufacturer => await _apiService.PostDataToApiAsync("api/Manufacturers/Create", value),
                DataOperations.CreateProductLine => await _apiService.PostDataToApiAsync("api/ProductLines/Create", value),
                DataOperations.CreateProductSeries => await _apiService.PostDataToApiAsync("api/ProductSeries/Create", value),
                DataOperations.CreateSwitchModel => await _apiService.PostDataToApiAsync("api/SwitchModelsCreate", value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Put data.
        public async Task PutDataAsync(string dataOperation, object value)
        {
            var putted = dataOperation switch
            {
                DataOperations.UpdateBrand => await _apiService.PutDataToApiAsync("api/Brands/Update", value),
                DataOperations.UpdateFirmwareVersion => await _apiService.PutDataToApiAsync("api/FirmwareVersions/Update", value),
                DataOperations.UpdateManufacturer => await _apiService.PutDataToApiAsync("api/Manufacturers/Update", value),
                DataOperations.UpdateProductLine => await _apiService.PutDataToApiAsync("api/ProductLines/Update", value),
                DataOperations.UpdateProductSeries => await _apiService.PutDataToApiAsync("api/ProductSeries/Update", value),
                DataOperations.UpdateSwitchModel => await _apiService.PutDataToApiAsync("api/SwitchModels/Update", value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Delete data.
        public async Task<object> DeleteDataAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                DataOperations.DeleteBrand => await _apiService.DeleteDataFromApiAsync($"api/Brands/Delete/{(int)value}"),
                DataOperations.DeleteFirmwareVersion => await _apiService.DeleteDataFromApiAsync($"api/FirmwareVersions/Delete/{(int)value}"),
                DataOperations.DeleteManufacturer => await _apiService.DeleteDataFromApiAsync($"api/Manufacturers/Delete/{(int)value}"),
                DataOperations.DeleteProductLine => await _apiService.DeleteDataFromApiAsync($"api/ProductLines/Delete/{(int)value}"),
                DataOperations.DeleteProductSeries => await _apiService.DeleteDataFromApiAsync($"api/ProductSeries/Delete/{(int)value}"),
                DataOperations.DeleteSwitchModel => await _apiService.DeleteDataFromApiAsync($"api/SwitchModels/Delete/{(int)value}"),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }
    }
}
