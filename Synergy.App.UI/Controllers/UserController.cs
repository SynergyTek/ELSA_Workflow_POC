using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.UI.Controllers
{
    [Authorize]
    public class UserController(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IUserContext userContext
    ) : Controller
    {
        public IActionResult Index()
        {
            return View(userManager.Users);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            IdentityResult result = await userManager.CreateAsync(user, model.PasswordHash);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Users");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            return View(model);
        }


        public async Task<IActionResult> Manage(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);
            ViewBag.Roles = roleManager.Roles.ToList();
            return View(new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles
            });
        }

        public async Task<IActionResult> AddUserToRole(string userId, string roleCode)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleCode))
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(userId);
            await userManager.AddToRoleAsync(user, roleCode);
            return RedirectToAction("Manage", new { id = userId });
        }

        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleCode)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleCode))
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(userId);
            await userManager.RemoveFromRoleAsync(user, roleCode);
            return RedirectToAction("Manage", new { id = userId });
        }

        //
        // [HttpPost]
        // public async Task<IActionResult> Edit(string id, UserViewModel model)
        // {
        //     if (string.IsNullOrEmpty(id) || id != model.Id || !ModelState.IsValid)
        //     {
        //         return NotFound();
        //     }
        //
        //     var user = await userManager.FindByIdAsync(model.Id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     user.Name = model.Name;
        //     user.UserName = model.UserName;
        //     user.Email = model.Email;
        //
        //     var result = await userManager.UpdateAsync(user);
        //     if (result.Succeeded)
        //     {
        //         return RedirectToAction("Index", "Users");
        //     }
        //
        //     foreach (IdentityError err in result.Errors)
        //     {
        //         ModelState.AddModelError("", err.Description);
        //     }
        //
        //     return View(model);
        // }
        //
        //
        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id.ToString());
            return View(role as RoleViewModel);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null) return View();
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
    }
}