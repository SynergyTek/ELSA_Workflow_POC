using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Data;
using Synergy.App.Data.Models;

namespace Synergy.App.UI.Controllers
{
    public class TableMetadataController(ApplicationDbContext context) : Controller
    {
        // GET: TableMetadata
        public async Task<IActionResult> Index()
        {
            return View(await context.Set<TableModel>().ToListAsync());
        }

        // GET: TableMetadata/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableMetadataModel = await context.Set<TableModel>()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableMetadataModel == null)
            {
                return NotFound();
            }

            return View(tableMetadataModel);
        }

        // GET: TableMetadata/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TableMetadata/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Description,Alias,Schema,CreateTable,Query,Id,CreatedDate,CreatedBy,LastUpdatedDate,LastUpdatedBy,IsDeleted,Status")] TableModel tableModel)
        {
            if (ModelState.IsValid)
            {
                tableModel.Id = Guid.NewGuid();
                context.Add(tableModel);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tableModel);
        }

        // GET: TableMetadata/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableMetadataModel = await context.Set<TableModel>().FindAsync(id);
            if (tableMetadataModel == null)
            {
                return NotFound();
            }
            return View(tableMetadataModel);
        }

        // POST: TableMetadata/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Code,Name,Description,Alias,Schema,CreateTable,Query,Id,CreatedDate,CreatedBy,LastUpdatedDate,LastUpdatedBy,IsDeleted,Status")] TableModel tableModel)
        {
            if (id != tableModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tableModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableMetadataModelExists(tableModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tableModel);
        }

        // GET: TableMetadata/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableMetadataModel = await context.Set<TableModel>()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableMetadataModel == null)
            {
                return NotFound();
            }

            return View(tableMetadataModel);
        }

        // POST: TableMetadata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tableMetadataModel = await context.Set<TableModel>().FindAsync(id);
            if (tableMetadataModel != null)
            {
                context.Set<TableModel>().Remove(tableMetadataModel);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableMetadataModelExists(Guid id)
        {
            return context.Set<TableModel>().Any(e => e.Id == id);
        }
    }
} 