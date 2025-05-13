using Elsa.Common.Models;
using Elsa.Workflows;
using Elsa.Workflows.Helpers;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business.Interface;
using Synergy.App.Data.ViewModels;
using Activity = System.Diagnostics.Activity;
using WorkflowStatus = Synergy.App.Data.WorkflowStatus;

namespace Synergy.App.UI.Controllers;

public class HomeController : Controller
{
    private readonly IUserContext _userContext;
    private readonly IWorkflowBusiness _workflowBusiness;
    private readonly IBookmarkQueue _bookmarkQueue;
    private readonly IStimulusHasher _stimulusHasher;
    private readonly IWorkflowStarter _workflowStarter;

    public HomeController(IUserContext userContext,
        IWorkflowBusiness workflowBusiness,
        IBookmarkQueue bookmarkQueue,
        IStimulusHasher stimulusHasher,
        IWorkflowStarter workflowStarter)
    {
        _userContext = userContext;
        _workflowBusiness = workflowBusiness;
        _bookmarkQueue = bookmarkQueue;
        _stimulusHasher = stimulusHasher;
        _workflowStarter = workflowStarter;
    }

    [Authorize]
    public IActionResult Index()
    {
        // var tasks = workflowBusiness
        //     .GetList(x => x.AssignedToUserId == userContext.Id || x.AssignedByUserId == userContext.Id).Result;
        var tasks = new List<WorkflowViewModel>();
        return View(tasks);
    }

    private async Task ResumeBookmarkAsync(Guid id, CancellationToken c = default)
    {
        var stimulus = new CreateTaskStimulus(id);

        var activityTypeName = ActivityTypeNameHelper.GenerateTypeName<AssignTaskToUser>();

        var bookmarkQueueItem = new NewBookmarkQueueItem
        {
            ActivityTypeName = activityTypeName,
            StimulusHash = _stimulusHasher.Hash(activityTypeName, stimulus),
        };
        await _bookmarkQueue.EnqueueAsync(bookmarkQueueItem, c);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public async Task<IActionResult> CompleteTask(Guid taskId, CancellationToken cancellationToken)
    {
        var user = new UserViewModel()
        {
            Id = _userContext.Id,
            UserName = _userContext.UserName,
            Email = _userContext.Email
        };
        await _workflowBusiness.StartWorkflow("POST_LEAVE", new Dictionary<string, object>
        {
            {
                "User", user
            }
        });
        return RedirectToAction("Index");
    }

    public IActionResult RejectTask()
    {
        return Json(new { Success = true });
    }

    public IActionResult CancelTask()
    {
        return Json(new { Success = true });
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
        var request = new StartWorkflowRequest
        {
            WorkflowDefinitionHandle = new WorkflowDefinitionHandle()
            {
                DefinitionId = "77c066a255cc46ea"
            },
            Input = new Dictionary<string, object>
            {
                { "Title", "Leave Application" }
            }
        };
        await _workflowStarter.StartWorkflowAsync(request);
        return Json(new { Success = true });
    }

    public IActionResult RaiseComplaint()
    {
        return Json(new { Success = true });
    }

    public IActionResult ApplyVisa()
    {
        return Json(new { Success = true });
    }

    #endregion
}