using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Core.Controllers;

[Route("form/{templateCode}/[action]")]
public class FormController(IFormBusiness formBusiness, ITemplateBusiness templateBusiness) : Controller
{
    public async Task<IActionResult> Index(string templateCode)
    {
        var template = await templateBusiness.GetSingle(x => x.Reference == templateCode,
            include: [x => x.CreatedBy, x => x.UpdatedBy]);
        if (template == null) return NotFound();
        var result = await formBusiness.GetList(templateCode);

        ViewBag.Name = template.Name;
        ViewBag.TemplateCode = templateCode;
        ViewBag.Json = template.Json;
        ViewBag.Columns = result.Item.FirstOrDefault()?.Keys ?? new List<string>();
        var model = result.Item;
        return View(model);
    }

    public async Task<IActionResult> Create(string templateCode, FormViewModel model)
    {
        var template = await templateBusiness.GetSingle(x => x.Reference == templateCode);
        if (template == null) return NotFound();
        ViewBag.Title = "Create " + template.Name;
        model.Template = template;
        return View("Manage", model);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(string templateCode, [FromBody] Dictionary<string, object> json)
    {
        var template = await templateBusiness.GetSingle(x => x.Reference == templateCode);
        if (template == null) return NotFound();
        var model = new FormViewModel
        {
            Template = template,
            Data = json
        };
        if (!ModelState.IsValid)
        {
            return View("Manage", model);
        }

        var result = await formBusiness.Create(model);
        if (result.IsSuccess)
        {
            return Ok(new
            {
                item = result.Item
            });
        }

        ModelState.AddModelError(string.Empty, result.Message);
        return BadRequest(new
        {
            message = result.Message,
            model = model
        });
    }
}