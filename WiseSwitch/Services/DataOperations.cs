namespace WiseSwitch.Services
{
    public static class DataOperations
    {
        // -- Create --
        public const string CreateBrand = "CreateBrand";
        public const string CreateFirmwareVersion = "CreateFirmwareVersion";
        public const string CreateManufacturer = "CreateManufacturer";
        public const string CreateProductLine = "CreateProductLine";
        public const string CreateProductSeries = "CreateProductSeries";
        public const string CreateSwitchModel = "CreateSwitchModel";


        // -- Delete --
        public const string DeleteBrand = "DeleteBrand";
        public const string DeleteFirmwareVersion = "DeleteFirmwareVersion";
        public const string DeleteManufacturer = "DeleteManufacturer";
        public const string DeleteProductLine = "DeleteProductLine";
        public const string DeleteProductSeries = "DeleteProductSeries";
        public const string DeleteSwitchModel = "DeleteSwitchModel";


        // -- Get --

        // Brand.
        public const string GetAllBrandsOrderByName = "GetAllBrandsOrderByName";
        public const string GetComboBrands = "GetComboBrands";
        public const string GetDisplayBrand = "GetDisplayBrand";
        public const string GetEditModelBrand = "GetEditModelBrand";
        public const string GetExistsBrand = "GetExistsBrand";
        public const string GetModelBrand = "GetModelBrand";
        // Firmware Version.
        public const string GetAllFirmwareVersionsOrderByVersion = "GetAllFirmwareVersionsOrderByVersion";
        public const string GetComboFirmwareVersions = "GetComboFirmwareVersions";
        public const string GetDisplayFirmwareVersion = "GetDisplayFirmwareVersion";
        public const string GetEditModelFirmwareVersion = "GetEditModelFirmwareVersion";
        public const string GetExistsFirmwareVersion = "GetExistsFirmwareVersion";
        public const string GetModelFirmwareVersion = "GetModelFirmwareVersion";
        // Manufacturer.
        public const string GetAllManufacturersOrderByName = "GetAllManufacturersOrderByName";
        public const string GetComboManufacturers = "GetComboManufacturers";
        public const string GetDisplayManufacturer = "GetDisplayManufacturer";
        public const string GetEditModelManufacturer = "GetEditModelManufacturer";
        public const string GetExistsManufacturer = "GetExistsManufacturer";
        public const string GetModelManufacturer = "GetModelManufacturer";
        // Product Line.
        public const string GetAllProductLinesOrderByName = "GetAllProductLinesOrderByName";
        public const string GetBrandIdOfProductLine = "GetBrandIdOfProductLine";
        public const string GetComboProductLines = "GetComboProductLines";
        public const string GetDisplayProductLine = "GetDisplayProductLine";
        public const string GetEditModelProductLine = "GetEditModelProductLine";
        public const string GetExistsProductLine = "GetExistsProductLine";
        public const string GetModelProductLine = "GetModelProductLine";
        public const string GetComboProductLinesOfBrand = "GetComboProductLinesOfBrand";
        // Product Series.
        public const string GetAllProductSeriesOrderByName = "GetAllProductSeriesOrderByName";
        public const string GetComboProductSeries = "GetComboProductSeries";
        public const string GetDisplayProductSeries = "GetDisplayProductSeries";
        public const string GetEditModelProductSeries = "GetEditModelProductSeries";
        public const string GetExistsProductSeries = "GetExistsProductSeries";
        public const string GetModelProductSeries = "GetModelProductSeries";
        public const string GetDependencyChainIdsOfProductSeries = "GetDependencyChainIdsOfProductSeries";
        public const string GetComboProductSeriesOfProductLine = "GetComboProductSeriesOfProductLine";
        // Switch Model.
        public const string GetAllSwitchModelsOrderByModelName = "GetAllSwitchModelsOrderByModelName";
        public const string GetComboSwitchModels = "GetComboSwitchModel";
        public const string GetDisplaySwitchModel = "GetDisplaySwitchModel";
        public const string GetEditModelSwitchModel = "GetEditModelSwitchModel";
        public const string GetExistsSwitchModel = "GetExistsSwitchModel";
        public const string GetModelSwitchModel = "GetModelSwitchModel";


        // -- Update --
        public const string UpdateBrand = "UpdateBrand";
        public const string UpdateFirmwareVersion = "UpdateFirmwareVersion";
        public const string UpdateManufacturer = "UpdateManufacturer";
        public const string UpdateProductLine = "UpdateProductLine";
        public const string UpdateProductSeries = "UpdateProductSeries";
        public const string UpdateSwitchModel = "UpdateSwitchModel";
    }
}
