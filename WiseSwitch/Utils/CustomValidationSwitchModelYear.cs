using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.Utils
{
    public class CustomValidationSwitchModelYear : ValidationAttribute
    {
        public CustomValidationSwitchModelYear()
        {
            ErrorMessage = "The Model Year is not valid.";
        }

        public override bool IsValid(object value)
        {
            return int.TryParse(value as string, out int result) && result >= 0 && result < 10000;
        }
    }
}
