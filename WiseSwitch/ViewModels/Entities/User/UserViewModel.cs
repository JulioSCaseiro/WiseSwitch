using System.ComponentModel.DataAnnotations;

namespace WiseSwitch.ViewModels.Entities.User
{
    public class UserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "Username")]
        [Required(ErrorMessage = "The Username is required.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "The Password is required.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "The Role is required.")]
        public string Role { get; set; }
    }
}
