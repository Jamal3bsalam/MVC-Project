using System.ComponentModel.DataAnnotations;

namespace MVC_03.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		[MaxLength(16)]
		[MinLength(4)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Comfirmed Password Dose Not Match Password")]
		public string ConfirmPassword { get; set; }
	}
}
