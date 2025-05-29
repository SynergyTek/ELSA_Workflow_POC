using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Core.Controllers;

public class TemplateController(ApplicationDbContext context, ITemplateBusiness business) : Controller
{
    public async Task<IActionResult> Index()
    {
        var templates = await business.GetList(x=>true,y=> y.CreatedBy, y => y.UpdatedBy);
        return View(templates);
    }

    public IActionResult Create()
    {
        return View("Manage", null);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(TemplateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Manage", model);
        }

        if (model.Id == Guid.Empty)
        {
            await business.Create(model);
        }
        else
        {
            await business.Edit(model);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(TemplateViewModel model)
    {
        return View("Manage", model);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await business.Delete(id);
        return RedirectToAction("Index");
    }
}