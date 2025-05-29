using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Core.Controllers
{
    public class ColumnController(IContextBase<ColumnViewModel, ColumnModel> context) : Controller
    {
        // GET: Column
        public async Task<IActionResult> Index()
        {
            var columns = await context.GetList();
            return View(columns);
        }


        // GET: Column/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var columnViewModel = await context.GetSingleById(id);
            if (columnViewModel == null)
            {
                return NotFound();
            }

            return View(columnViewModel);
        }

        // GET: Column/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Column/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Alias,IsNullable,DataType,IsForeignKey,IsPrimaryKey,IsSystemColumn,IsUniqueColumn,IsVisible,Type,Id")] ColumnViewModel columnViewModel)
        {
            if (ModelState.IsValid)
            {
                await context.Create(columnViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(columnViewModel);
        }

        // GET: Column/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var columnViewModel = await context.GetSingleById(id);
            if (columnViewModel == null)
            {
                return NotFound();
            }
            return View(columnViewModel);
        }

        // POST: Column/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Alias,IsNullable,DataType,IsForeignKey,IsPrimaryKey,IsSystemColumn,IsUniqueColumn,IsVisible,Type,Id")] ColumnViewModel columnViewModel)
        {
            if (id != columnViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await context.Edit(columnViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(columnViewModel);
        }

        // GET: Column/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var columnViewModel = await context.GetSingleById(id);
            if (columnViewModel == null)
            {
                return NotFound();
            }

            return View(columnViewModel);
        }

        // POST: Column/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
