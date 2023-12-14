namespace WiseSwitch.ViewModels.Entities.FirmwareVersion
{
    public class DisplayFirmwareVersionViewModel
    {
        public int Id { get; set; }

        public string Version { get; set; }


        public IEnumerable<string> SwitchModelsNames { get; set; }
    }
}
