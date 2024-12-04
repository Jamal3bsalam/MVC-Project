using System.ComponentModel.DataAnnotations;

namespace MVC_03.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage = "Invalid Email !!")]
        public string Email { get; set; }
    }
}
