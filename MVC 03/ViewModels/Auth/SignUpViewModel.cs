﻿using System.ComponentModel.DataAnnotations;

namespace MVC_03.ViewModels.Auth
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }

		[Required(ErrorMessage = "FirstName Is Required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName Is Required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		[MaxLength(16)]
		[MinLength(4)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Comfirmed Password Dose Not Match Password")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
