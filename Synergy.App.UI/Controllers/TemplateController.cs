using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;
using Synergy.App.Business.Interface;
using Synergy.App.Business.Implementation;
using Synergy.App.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Synergy.App.UI.Controllers
{
 
    public class TemplateController : Controller
    {
        private  ITemplateBusiness _templateBusiness;


        public TemplateController(ITemplateBusiness templateBusiness)
        {
            _templateBusiness = templateBusiness;

        }
        public IActionResult Index()
        {
            return View();
        }



        public async Task<ActionResult> ReadData()
        {
            var model = await _templateBusiness.GetList();
            return Json(model);
        }
        public IActionResult Create()
        {
            return View("Manage", new TemplateViewModel
            {
                DataAction = DataActionEnum.Create,
                //Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),

            });
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var member = await _templateBusiness.GetSingleById(Id);

            if (member != null)
            {
             member.DataAction = DataActionEnum.Edit;
                return View("Manage", member);
            }
            return View("Manage", new TemplateViewModel());

        }

        public async Task<IActionResult> Delete(Guid Id)
        {


            await _templateBusiness.Delete(Id);
            return Json(true);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(TemplateViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.DataAction == DataActionEnum.Create)
                {
                   // model.Users = model.UserId.ToArray();
                    var result = await _templateBusiness.Create(model);
                    if (result.IsSuccess)
                    {
                        ViewBag.Success = true;

                    }
                    else
                    {
                       // ModelState.AddModelErrors(result.Messages);
                        return Json(new { success = false, error = ModelState.SerializeErrors().ToHtmlError() });

                    }
                }
                else if (model.DataAction == DataActionEnum.Edit)
                {
             
                    var result = await _templateBusiness.Edit(model);
                    if (result.IsSuccess)
                    {
                        ViewBag.Success = true;

                    }
                    else
                    {
                        // ModelState.AddModelErrors(result.Messages);
                        return Json(new { success = false, error = ModelState.SerializeErrors().ToHtmlError() });
                    }
                }
            }

            return View("Manage", model);
        }

      
    }
}
