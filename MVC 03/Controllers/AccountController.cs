using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_03.Helper;
using MVC_03.ViewModels.Auth;

namespace MVC_03.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser>signInManager )
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		#region SignUp

		//SignUp
		[HttpGet] //Account/SignUp
		public IActionResult SignUp()
		{
			return View();
		}


		[HttpPost] //Account/SignUp
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				// SignUp
				try
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser()
							{
								// Manual Mapping
								UserName = model.UserName,
								FirstName = model.FirstName,
								LastName = model.LastName,
								Email = model.Email,
								//PasswordHash = model.Password,
								IsAgree = model.IsAgree
							};
							var result = await _userManager.CreateAsync(user, model.Password);
							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
						ModelState.AddModelError(string.Empty, "Email Is Aleardy Exist !!");
						return View();

					}
					ModelState.AddModelError(string.Empty, "UserName Is Aleardy Exist !!");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View();
		}

		#endregion

		#region SignIn

		[HttpGet] //Account/SignIn
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						//Check Password
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//SignIn

							var Result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
							if (Result.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}
					}
					ModelState.AddModelError(string.Empty, "Invalid Login !!");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}
		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
			   var user = await _userManager.FindByEmailAsync(model.Email);
			   if(user is not null)
			   {
					//Create Token
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					// Create Reset Password URL
					var url = Url.Action("ResetPassword", "Account" , new {email = model.Email , token},Request.Scheme);

					// http://localhost:5102/Account/ResetPawword?email=gamalwork81@gmail.com&token

					// Create Email
					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};

				    //Send Email
					
						EmailSettings.SendEmail(email);

						return RedirectToAction(nameof(CheckYourInbox));
					


			   }
				ModelState.AddModelError(string.Empty, "Invalid Operation, Please Try Again !!");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string email , string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var Result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (Result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid Operation, Please Try Again !!");

			return View(model);
		}


		public IActionResult AccessDenied()
		{
			return View();
		}


	}
}
