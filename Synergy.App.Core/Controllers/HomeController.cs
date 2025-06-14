using ikvm.extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;
using Activity = System.Diagnostics.Activity;

namespace Synergy.App.Core.Controllers;

public class HomeController : Controller
{
    private readonly IUserContext _userContext;
    private readonly IContextBase<WorkflowViewModel, WorkflowModel> _workflowContext;
    // private readonly IElsaBusiness _elsaBusiness;
    private readonly IWorkflowBusiness _workflowBusiness;


    public HomeController(IUserContext userContext,
        IContextBase<WorkflowViewModel, WorkflowModel> workflowContext,
        IWorkflowBusiness workflowBusiness
        // IElsaBusiness elsaBusiness
        )
    {
        _userContext = userContext;
        _workflowContext = workflowContext;
        _workflowBusiness = workflowBusiness;
        // _elsaBusiness = elsaBusiness;
    }

    [Authorize]
    public IActionResult Index()
    {
        var tasks = _workflowContext.GetList(
            x => x.AssignedToUser.Id == _userContext.Id || x.AssignedByUser.Id == _userContext.Id,
            include: [x => x.AssignedToUser, x => x.AssignedByUser]).Result;
        return View(tasks);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public async Task<IActionResult> CompleteTask(WorkflowViewModel model)
    {
        // var status = await _elsaBusiness.ResumeWorkflow(model.Id.toString(), new()
        // {
        //     { "Status", "Approved" }
        // });
        // if (!status.IsSuccess) return RedirectToAction("Index");
        model.Status = WorkflowStatus.Completed;
        await _workflowBusiness.Edit(model);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RejectTask(WorkflowViewModel model)
    {
        // var status = await _elsaBusiness.ResumeWorkflow(model.Id.toString(), new()
        // {
        //     { "Status", "Approved" }
        // });
        // if (!status.IsSuccess) return RedirectToAction("Index");
        model.Status = WorkflowStatus.Completed;
        await _workflowBusiness.Edit(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CancelTask(WorkflowViewModel model)
    {
        // var status = await _elsaBusiness.ResumeWorkflow(model.Id.toString(), new()
        // {
        //     { "Status", "Approved" }
        // });
        // if (!status.IsSuccess) return RedirectToAction("Index");
        model.Status = WorkflowStatus.Completed;
        await _workflowBusiness.Edit(model);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTask()
    {
        return Json(new { Success = true });
    }

    public IActionResult SendReminder()
    {
        return Json(new { Success = true });
    }


    #region Applications

    public async Task<IActionResult> ApplyLeave()
    {
        var user = new UserViewModel()
        {
            Id = _userContext.Id,
            UserName = _userContext.UserName,
            Email = _userContext.Email
        };
        // var a = await _elsaBusiness.StartWorkflow("POST_LEAVE", new Dictionary<string, object>
        // {
        //     {
        //         "User", user
        //     }
        // });
        return Json(new { Success = true });
    }

    #endregion
}