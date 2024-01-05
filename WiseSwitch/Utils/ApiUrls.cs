namespace WiseSwitch.Utils
{
    public static class ApiUrls
    {
        // API controllers.
        private const string _apiBrands = "api/Brands";
        private const string _apiFirmwareVersions = "api/FirmwareVersions";
        private const string _apiManufacturers = "api/Manufacturers";
        private const string _apiProductLines = "api/ProductLines";
        private const string _apiProductSeries = "api/ProductSeries";
        private const string _apiSwitchModels = "api/SwitchModels";

        // Actions (except Get).
        private const string _create = "/Create";
        private const string _delete = "/Delete";
        private const string _update = "/Update";


        // -- Create --
        public static string CreateBrand => _apiBrands + _create;
        public static string CreateFirmwareVersion => _apiFirmwareVersions + _create;
        public static string CreateManufacturer => _apiManufacturers + _create;
        public static string CreateProductLine => _apiProductLines + _create;
        public static string CreateProductSeries => _apiProductSeries + _create;
        public static string CreateSwitchModel => _apiSwitchModels + _create;


        // -- Delete --
        public static string DeleteBrand => _apiBrands + _delete;
        public static string DeleteFirmwareVersion => _apiFirmwareVersions + _delete;
        public static string DeleteManufacturer => _apiManufacturers + _delete;
        public static string DeleteProductLine => _apiProductLines + _delete;
        public static string DeleteProductSeries => _apiProductSeries + _delete;
        public static string DeleteSwitchModel => _apiSwitchModels + _delete;


        // -- Get --

        // Brand.
        public static string GetBrandDisplay => _apiBrands + "/Display/";
        public static string GetBrandEditModel => _apiBrands + "/EditModel/";
        public static string GetBrandExists => _apiBrands + "/Exists/";
        public static string GetBrandModel => _apiBrands + "/Model/";
        public static string GetBrandsCombo => _apiBrands + "/Combo";
        public static string GetBrandsOrderByName => _apiBrands + "/All";

        // Firmware Version.
        public static string GetFirmwareVersionDisplay => _apiFirmwareVersions + "/Display/";
        public static string GetFirmwareVersionEditModel => _apiFirmwareVersions + "/EditModel/";
        public static string GetFirmwareVersionExists => _apiFirmwareVersions + "/Exists/";
        public static string GetFirmwareVersionModel => _apiFirmwareVersions + "/Model/";
        public static string GetFirmwareVersionsCombo => _apiFirmwareVersions + "/Combo";
        public static string GetFirmwareVersionsOrderByVersion => _apiFirmwareVersions + "/All";

        // Manufacturer.
        public static string GetManufacturerDisplay => _apiManufacturers + "/Display/";
        public static string GetManufacturerEditModel => _apiManufacturers + "/EditModel/";
        public static string GetManufacturerExists => _apiManufacturers + "/Exists/";
        public static string GetManufacturerModel => _apiManufacturers + "/Model/";
        public static string GetManufacturersCombo => _apiManufacturers + "/Combo";
        public static string GetManufacturersOrderByName => _apiManufacturers + "/All";

        // Product Line.
        public static string GetProductLineBrandId => _apiProductLines + "/GetBrandIdOfProductline/";
        public static string GetProductLineDisplay => _apiProductLines + "/Display/";
        public static string GetProductLineEditModel => _apiProductLines + "/EditModel/";
        public static string GetProductLineExists => _apiProductLines + "/Exists/";
        public static string GetProductLineModel => _apiProductLines + "/Model/";
        public static string GetProductLinesCombo => _apiProductLines + "/Combo";
        public static string GetProductLinesComboOfBrand => _apiProductLines + "/ComboProductLinesOfBrand/";
        public static string GetProductLinesOrderByName => _apiProductLines + "/All";

        // Product Series.
        public static string GetProductSeriesCombo => _apiProductSeries + "/Combo";
        public static string GetProductSeriesComboOfProductLine => _apiProductSeries + "/ComboProductSeriesOfProductLine/";
        public static string GetProductSeriesDependencyChainIds => _apiProductSeries + "/IdsOfDependencyChain/";
        public static string GetProductSeriesDisplay => _apiProductSeries + "/Display/";
        public static string GetProductSeriesEditModel => _apiProductSeries + "/EditModel/";
        public static string GetProductSeriesExists => _apiProductSeries + "/Exists/";
        public static string GetProductSeriesModel => _apiProductSeries + "/Model/";
        public static string GetProductSeriesOrderByName => _apiProductSeries + "/All";

        // Switch Model.
        public static string GetSwitchModelDisplay => _apiSwitchModels + "/Display/";
        public static string GetSwitchModelEditModel => _apiSwitchModels + "/EditModel/";
        public static string GetSwitchModelExists => _apiSwitchModels + "/Exists/";
        public static string GetSwitchModelModel => _apiSwitchModels + "/Model/";
        public static string GetSwitchModelsCombo => _apiSwitchModels + "/Combo";
        public static string GetSwitchModelsOrderByModelName => _apiSwitchModels + "/All";


        // -- Update --
        public static string UpdateBrand => _apiBrands + _update;
        public static string UpdateFirmwareVersion => _apiFirmwareVersions + _update;
        public static string UpdateManufacturer => _apiManufacturers + _update;
        public static string UpdateProductLine => _apiProductLines + _update;
        public static string UpdateProductSeries => _apiProductSeries + _update;
        public static string UpdateSwitchModel => _apiSwitchModels + _update;
    }
}
