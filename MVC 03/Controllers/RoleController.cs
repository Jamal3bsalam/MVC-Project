﻿using Company.G05.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_03.ViewModels;

namespace MVC_03.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        // Get, GetAll , Add , Update , Delete
        // Index , Create , Details , Edit , Delete

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager) 
		{
            _roleManager = roleManager;
            _userManager = userManager;
        }
		public async Task<IActionResult> Index(string searchInput)
		{
			var roles = Enumerable.Empty<RoleViewModel>();
			if(string.IsNullOrEmpty(searchInput))
			{
				roles = await _roleManager.Roles.Select(R => new RoleViewModel()
				{
					Id = R.Id,
					Name = R.Name
				}).ToListAsync();
			}
			else
			{
				roles = await _roleManager.Roles.Where(R => R.Name
										  .ToLower()
										  .Contains(searchInput.ToLower()))
										  .Select(R => new RoleViewModel()
										  {
                                              Id = R.Id,
                                              Name = R.Name
                                          }).ToListAsync();
										  
			}
			return View(roles);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
			if (ModelState.IsValid)
			{
				var role = new IdentityRole()
				{
					Name = model.Name,
				};

				var Result = await _roleManager.CreateAsync(role);
				if(Result.Succeeded)
				{
					return RedirectToAction(nameof(Index));	
				}
				ModelState.AddModelError(string.Empty, "Invalid Operation");
			}
            return View();
        }

        [HttpGet]
		public async Task<IActionResult> Details(string? id , string ViewName = "Details")
		{
			if (id is null)
				return BadRequest();

			var roleFromDb = await _roleManager.FindByIdAsync(id);
			if (roleFromDb is null)
				return NotFound();
			var role = new RoleViewModel()
			{
				Id = roleFromDb.Id,
				Name = roleFromDb.Name
			};

			return View(ViewName, role);

		}


		[HttpGet]
		public async Task<IActionResult> Edit(string? id)
		{
			return await Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string? id,RoleViewModel model)
		{
			if (id != model.Id)
				return BadRequest();
			if(ModelState.IsValid)
			{
				var roleFromDb = await _roleManager.FindByIdAsync(id);
				if (roleFromDb is null)
					return NotFound();

				// Manual Map
				roleFromDb.Name = model.Name;

				await _roleManager.UpdateAsync(roleFromDb);
				return RedirectToAction(nameof(Index));
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
		public async Task<IActionResult> Delete([FromRoute]string? id,RoleViewModel model)
		{
			if(id != model.Id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				var roleFromDb = await _roleManager.FindByIdAsync(id);
				if (roleFromDb is null)
					return NotFound();

				await _roleManager.DeleteAsync(roleFromDb);

				return RedirectToAction(nameof(Index));

			}
			return View(model);
		}


		[HttpGet]
		public async Task<IActionResult> AddOrRemoveUsers(string roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId);
			if (role is null)
				return NotFound();

			ViewData["RoleId"] = roleId;

			var UsersInRole = new List<UserInRoleViewModel>();
			var users = await _userManager.Users.ToListAsync();


			foreach (var user in users)
			{
				var userInRole = new UserInRoleViewModel()
				{
					UserId = user.Id,
					UserName = user.UserName
				};
				if (await _userManager.IsInRoleAsync(user, role.Name))
				{
					userInRole.IsSelected = true;
				}
				else
				{
					userInRole.IsSelected = false;
				}
				UsersInRole.Add(userInRole); 
			}
			return View(UsersInRole);
		}

		[HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel> users)
		{
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
			if(ModelState.IsValid)
			{
				foreach (var user in users)
				{
					var appUser = await _userManager.FindByIdAsync(user.UserId);
					if(appUser is not null)
					{
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser,role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser,role.Name);
                        }
                    }
				}

				return RedirectToAction(nameof(Edit), new {id = roleId});

			}
			return View(users);
        }

    }
}
