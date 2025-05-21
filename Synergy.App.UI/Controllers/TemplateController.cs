using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.UI.Controllers
{
    public class TemplateController(ApplicationDbContext context, ITemplateBusiness business) : Controller
    {
        // GET: Template
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Template.Include(t => t.TableMetadata);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Template/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templateModel = await context.Template
                .Include(t => t.TableMetadata)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateModel == null)
            {
                return NotFound();
            }

            return View(templateModel);
        }

        // GET: Template/Create
        public IActionResult Create()
        {
            ViewData["TableMetadataId"] = new SelectList(context.Set<TableMetadataModel>(), "Id", "Alias");
            return View();
        }

        // POST: Template/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await business.Create(model);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TableMetadataId"] = new SelectList(context.Set<TableMetadataModel>(), "Id", "Alias", model.TableMetadataId);
            return View(model);
        }

        // GET: Template/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templateModel = await context.Template.FindAsync(id);
            if (templateModel == null)
            {
                return NotFound();
            }
            ViewData["TableMetadataId"] = new SelectList(context.Set<TableMetadataModel>(), "Id", "Alias", templateModel.TableMetadataId);
            return View(templateModel);
        }

        // POST: Template/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Code,Description,TableMetadataId,Json,Id,CreatedDate,CreatedBy,LastUpdatedDate,LastUpdatedBy,IsDeleted,Status")] TemplateModel templateModel)
        {
            if (id != templateModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(templateModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateModelExists(templateModel.Id))
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
            ViewData["TableMetadataId"] = new SelectList(context.Set<TableMetadataModel>(), "Id", "Alias", templateModel.TableMetadataId);
            return View(templateModel);
        }

        // GET: Template/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templateModel = await context.Template
                .Include(t => t.TableMetadata)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (templateModel == null)
            {
                return NotFound();
            }

            return View(templateModel);
        }

        // POST: Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var templateModel = await context.Template.FindAsync(id);
            if (templateModel != null)
            {
                context.Template.Remove(templateModel);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateModelExists(Guid id)
        {
            return context.Template.Any(e => e.Id == id);
        }
    }
}
