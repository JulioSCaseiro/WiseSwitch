using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The Username is required.")]
        public string UserName { get; set; }

        
        [Required(ErrorMessage = "The Password is required.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
