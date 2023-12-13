using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.Brand;
using WiseSwitch.ViewModels.Entities.FirmwareVersion;

namespace WiseSwitch.Services
{
    public class DataService
    {
        ApiService _apiService;

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
                DataOperations.GetModelBrand => await _apiService.GetDataFromApiAsync<Brand>($"api/Brands/Model/{(int)value}"),
                // Firmware Version.
                DataOperations.GetAllFirmwareVersionsOrderByVersion => await _apiService.GetDataFromApiAsync<IEnumerable<IndexRowFirmwareVersionViewModel>>("api/FirmwareVersion/All"),
                DataOperations.GetComboFirmwareVersions => await _apiService.GetDataFromApiAsync<IEnumerable<SelectListItem>>("api/FirmwareVersion/Combo"),
                DataOperations.GetDisplayFirmwareVersion => await _apiService.GetDataFromApiAsync<DisplayFirmwareVersionViewModel>($"api/FirmwareVersion/Display/{(int)value}"),
                DataOperations.GetExistsFirmwareVersion => await _apiService.GetDataFromApiAsync<bool>($"api/FirmwareVersion/Exists/{(int)value}"),
                DataOperations.GetModelFirmwareVersion => await _apiService.GetDataFromApiAsync<EditFirmwareVersionViewModel>($"api/FirmwareVersion/Model/{(int)value}"),
                //// Manufacturer.
                //DataOperations.GetAllManufacturersOrderByName => await _dataUnit.Manufacturers.GetAllOrderByName(),
                //DataOperations.GetComboManufacturers => await _dataUnit.Manufacturers.GetComboManufacturersAsync(),
                //DataOperations.GetDisplayManufacturer => await _dataUnit.Manufacturers.GetDisplayDtoAsync((int)value),
                //DataOperations.GetExistsManufacturer => await _dataUnit.Manufacturers.ExistsAsync((int)value),
                //DataOperations.GetModelManufacturer => await _dataUnit.Manufacturers.GetAsNoTrackingByIdAsync((int)value),
                //// Product Line.
                //DataOperations.GetAllProductLinesOrderByName => await _dataUnit.ProductLines.GetAllOrderByName(),
                //DataOperations.GetComboProductLines => await _dataUnit.ProductLines.GetComboProductLinesAsync(),
                //DataOperations.GetDisplayProductLine => await _dataUnit.ProductLines.GetDisplayDtoAsync((int)value),
                //DataOperations.GetExistsProductLine => await _dataUnit.ProductLines.ExistsAsync((int)value),
                //DataOperations.GetModelProductLine => await _dataUnit.ProductLines.GetAsNoTrackingByIdAsync((int)value),
                //// Product Series.
                //DataOperations.GetAllProductSeriesOrderByName => await _dataUnit.ProductSeries.GetAllOrderByName(),
                //DataOperations.GetComboProductSeries => await _dataUnit.ProductSeries.GetComboProductSeriesAsync(),
                //DataOperations.GetDisplayProductSeries => await _dataUnit.ProductSeries.GetDisplayDtoAsync((int)value),
                //DataOperations.GetExistsProductSeries => await _dataUnit.ProductSeries.ExistsAsync((int)value),
                //DataOperations.GetModelProductSeries => await _dataUnit.ProductSeries.GetAsNoTrackingByIdAsync((int)value),
                //// Switch Model.
                //DataOperations.GetAllSwitchModelsOrderByModelName => await _dataUnit.SwitchModels.GetAllOrderByModelNameAsync(),
                //DataOperations.GetComboSwitchModels => await _dataUnit.SwitchModels.GetComboSwitchModelsAsync(),
                //DataOperations.GetDisplaySwitchModel => await _dataUnit.SwitchModels.GetDisplayDtoAsync((int)value),
                //DataOperations.GetExistsSwitchModel => await _dataUnit.SwitchModels.ExistsAsync((int)value),
                //DataOperations.GetModelSwitchModel => await _dataUnit.SwitchModels.GetAsNoTrackingByIdAsync((int)value),

                _ => throw new InvalidOperationException(dataOperation),
            };


        }
        // Post data.
        public async Task PostDataAsync(string dataOperation, object value)
        {
            IEntity posted = dataOperation switch
            {
                DataOperations.CreateBrand => await _apiService.PostDataToApiAsync("api/Brands/Create", value) as Brand,
                DataOperations.CreateFirmwareVersion => await _apiService.PostDataToApiAsync("api/FirmwareVersion/Create", value) as FirmwareVersion,
                //DataOperations.CreateManufacturer => await _dataUnit.Manufacturers.CreateAsync(value as Manufacturer),
                //DataOperations.CreateProductLine => await _dataUnit.ProductLines.CreateAsync(value as ProductLine),
                //DataOperations.CreateProductSeries => await _dataUnit.ProductSeries.CreateAsync(value as ProductSeries),
                //DataOperations.CreateSwitchModel => await _dataUnit.SwitchModels.CreateAsync(value as SwitchModel),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Put data.
        public async Task PutDataAsync(string dataOperation, object value)
        {
            IEntity putted = dataOperation switch
            {
                DataOperations.UpdateBrand => await _apiService.PutDataToApiAsync("api/Brands/Update", value) as Brand,
                DataOperations.UpdateFirmwareVersion => await _apiService.PutDataToApiAsync("api/FirmwareVersion/Update", value) as FirmwareVersion,
                //DataOperations.UpdateManufacturer => _dataUnit.Manufacturers.Update(value as Manufacturer),
                //DataOperations.UpdateProductLine => _dataUnit.ProductLines.Update(value as ProductLine),
                //DataOperations.UpdateProductSeries => _dataUnit.ProductSeries.Update(value as ProductSeries),
                //DataOperations.UpdateSwitchModel => _dataUnit.SwitchModels.Update(value as SwitchModel),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }


        // Delete data.
        public async Task<object> DeleteDataAsync(string dataOperation, object value)
        {
            return dataOperation switch
            {
                DataOperations.DeleteBrand => await _apiService.DeleteDataFromApiAsync($"api/Brands/Delete/{(int)value}"),
                DataOperations.DeleteFirmwareVersion => await _apiService.DeleteDataFromApiAsync($"api/FirmwareVersion/Delete/{(int)value}"),
                //DataOperations.DeleteManufacturer => await _dataUnit.Manufacturers.DeleteAsync((int)value),
                //DataOperations.DeleteProductLine => await _dataUnit.ProductLines.DeleteAsync((int)value),
                //DataOperations.DeleteProductSeries => await _dataUnit.ProductSeries.DeleteAsync((int)value),
                //DataOperations.DeleteSwitchModel => await _dataUnit.SwitchModels.DeleteAsync((int)value),

                _ => throw new InvalidOperationException(dataOperation)
            };
        }
    }
}
