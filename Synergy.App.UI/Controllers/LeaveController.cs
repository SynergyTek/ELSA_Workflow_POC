using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.UI.Controllers
{
    public class LeaveController(ApplicationDbContext context, ILeaveBusiness leaveBusiness, IUserContext userContext)
        : Controller
    {
        // GET: Application
        public async Task<IActionResult> Index()
        {
            return View(await leaveBusiness.GetList());
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var leaves = await leaveBusiness.GetSingleById(id);
            return View(leaves);
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
        public async Task<IActionResult> Create(LeaveViewModel leaveViewModel)
        {
            //if (!ModelState.IsValid) return View(leaveViewModel);
            leaveViewModel.StartDate = DateTime.SpecifyKind(leaveViewModel.StartDate, DateTimeKind.Utc);
            leaveViewModel.EndDate = DateTime.SpecifyKind(leaveViewModel.EndDate, DateTimeKind.Utc);
            leaveViewModel.AppliedById = userContext.Id;

            await leaveBusiness.Create(leaveViewModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var leaves = await leaveBusiness.GetSingleById(id);

            return View(leaves);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LeaveViewModel leaveViewModel)
        {
            if (id != leaveViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(leaveViewModel);
            await leaveBusiness.Edit(leaveViewModel);
            return RedirectToAction(nameof(Index));
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var leaves = await leaveBusiness.GetSingleById(id);
            return View(leaves);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leaves = await context.Leave.FindAsync(id);
            if (leaves != null)
            {
                context.Leave.Remove(leaves);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}