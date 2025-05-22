using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.UI.Controllers;

public class TemplateController(ApplicationDbContext context, ITemplateBusiness business) : Controller
{
    public async Task<IActionResult> Index()
    {
        var templates = await business.GetList();
        return View(templates);
    }

    public IActionResult Create()
    {
        return View("Manage",null);
    }

    public IActionResult Edit(TemplateViewModel model)
    {
        return View("Manage", model);
    }

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}