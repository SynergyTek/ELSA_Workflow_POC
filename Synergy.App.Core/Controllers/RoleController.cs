using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Core.Controllers
{
    public class RoleController(
        RoleManager<Role> roleManager
    ) : Controller
    {
        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var roles = await roleManager.FindByIdAsync(id.ToString());
            return View(roles);
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel role)
        {
            if (!ModelState.IsValid) return View(role);
            await roleManager.CreateAsync(role);
            return RedirectToAction(nameof(Index));
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var role = await roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel role)
        {
            if (!ModelState.IsValid) return View(role);
            await roleManager.UpdateNormalizedRoleNameAsync(role);
            return RedirectToAction(nameof(Index));
        }

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