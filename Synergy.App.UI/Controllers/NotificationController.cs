using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.UI.Controllers
{
    public class NotificationController(ApplicationDbContext context, INotificationBusiness notificationBusiness)
        : Controller
    {
        // GET: Application
        public async Task<IActionResult> Index()
        {
            return View(await notificationBusiness.GetList());
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var notifications = await notificationBusiness.GetSingleById(id);
            return View(notifications);
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
        public async Task<IActionResult> Create([Bind("Message,IsRead")] NotificationViewModel notificationViewModel)
        {
            if (ModelState.IsValid)
            {
                context.Add(notificationViewModel);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(notificationViewModel);
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var notifications = await notificationBusiness.GetSingleById(id);

            return View(notifications);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Message,IsRead")] NotificationViewModel notificationViewModel)
        {
            if (id != notificationViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(notificationViewModel);
            try
            {
                context.Update(notificationViewModel);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationsViewModelExists(notificationViewModel.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var notifications = await notificationBusiness.GetSingleById(id);
            return View(notifications);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var notifications = await context.Notification.FindAsync(id);
            if (notifications != null)
            {
                context.Notification.Remove(notifications);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationsViewModelExists(Guid id)
        {
            return context.Notification.Any(e => e.Id == id);
        }
    }
}