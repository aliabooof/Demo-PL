using System.ComponentModel.DataAnnotations;

namespace DEMO_PL.ViewModels
{
    public class RegisterViewModel
    {

        public string FName { get; set; }
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="ivalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="doesnt match the written password")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }

    }
}
