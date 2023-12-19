﻿namespace WiseSwitch.ViewModels.Entities.SwitchModel
{
    public class CreateSwitchModelViewModel : IInputViewModel
    {
        public string ModelName { get; set; }

        public string ModelYear { get; set; }


        public int DefaultFirmwareVersionId { get; set; }


        public int ProductSeriesId { get; set; }

        public int ProductLineId { get; set; }

        public int BrandId { get; set; }
    }
}
