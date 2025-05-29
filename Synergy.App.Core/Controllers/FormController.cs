using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.ViewModel;
using Synergy.App.Data.Model;

namespace Synergy.App.Core.Controllers;

[Route("form/{templateCode}/[action]")]
public class FormController(
    IFormBusiness formBusiness,
    ITemplateBusiness templateBusiness,
    IContextBase<ColumnViewModel, ColumnModel> columnContext) : Controller
{
    public async Task<IActionResult> Index(string templateCode)
    {
        var template = await templateBusiness.GetSingle(x => x.Reference == templateCode,
            include: [x => x.CreatedBy, x => x.UpdatedBy]);
        if (template == null) return NotFound();
        var resultTask = formBusiness.GetList(templateCode);
        var columnTask = columnContext.GetList(x => x.Table.Template.Reference == templateCode && x.IsVisible);
        await Task.WhenAll(resultTask, columnTask);

        var columns = new List<ColumnViewModel>();
        columns.AddRange(GetBaseColumns());
        columns.AddRange(columnTask.Result);
        var model = resultTask.Result.Item;

        ViewBag.Name = template.Name;
        ViewBag.TemplateCode = templateCode;
        ViewBag.Json = template.Json;
        ViewBag.Columns = columns;
        return View(model);
    }

    private IEnumerable<ColumnViewModel> GetBaseColumns()
    {
        return new List<ColumnViewModel>
        {
            new()
            {
                Name = "Created By",
                Alias = "CreatedBy",
                IsVisible = true,
                DataType = DataColumnTypeEnum.Text
            },
            new()
            {
                Name = "Created At",
                Alias = "CreatedAt",
                IsVisible = true,
                DataType = DataColumnTypeEnum.DateTime
            },
            new()
            {
                Name = "Updated By",
                Alias = "UpdatedBy",
                IsVisible = true,
                DataType = DataColumnTypeEnum.Text,
                IsForeignKey = true
            },
            new()
            {
                Name = "Updated At",
                Alias = "UpdatedAt",
                IsVisible = true,
                DataType = DataColumnTypeEnum.DateTime
            },
        };
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