using AutoMapper;
using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_03.ViewModels;

namespace MVC_03.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        //Get , GetAll , Add , Update , Delete
        // Index , Details , Create : SignUp depend , Edit , Delete
        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper) 
		{
			_userManager = userManager;
            _mapper = mapper;
        }
		public async Task<IActionResult> Index(string searchInput)
		{
			var users = Enumerable.Empty<UserViewModel>();
			if (string.IsNullOrEmpty(searchInput))
			{
				 users =  _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToList();
			}
			else
			{
				users = await _userManager.Users.Where(U => U.Email
								   .ToLower()
								   .Contains(searchInput))
								   .Select(U => new UserViewModel()
								   {
									   Id = U.Id,
									   FirstName = U.FirstName,
									   LastName = U.LastName,
									   Email = U.Email,
									   Roles = _userManager.GetRolesAsync(U).Result
								   }).ToListAsync();
			}
			return View(users);
		}

        [HttpGet]
       public async Task<IActionResult> Details(string? id , string ViewName = "Details")
		{
			if (id is null)
				return BadRequest(); // 400

		  var userFromDb = await _userManager.FindByIdAsync(id);

			if(userFromDb is null)
				return NotFound(); //404

			var userView = _mapper.Map<UserViewModel>(userFromDb);

			//var user = new UserViewModel()
			//{
			//	Id = userFromDb.Id,
			//	FirstName = userFromDb.FirstName,
			//	LastName = userFromDb.LastName,
			//	Email = userFromDb.Email,
			//	Roles = _userManager.GetRolesAsync(userFromDb).Result
			//};

			return View(ViewName , userView);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string? id)
		{
			return await Details(id,"Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string id , UserViewModel model)
		{
			if (id != model.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				var userFromDb = await _userManager.FindByIdAsync(id);
				if (userFromDb is null)
					return NotFound();
				userFromDb.FirstName = model.FirstName;
				userFromDb.LastName = model.LastName;
				userFromDb.Email = model.Email;

				var Result =await _userManager.UpdateAsync(userFromDb);
				if (Result.Succeeded)
				{
                    return RedirectToAction(nameof(Index));
                }
            }
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete([FromRoute]string? id,UserViewModel model)
		{
			if(id != model.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound();

                var Result = await _userManager.DeleteAsync(userFromDb);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
			return View(model);
		}

    }
}
