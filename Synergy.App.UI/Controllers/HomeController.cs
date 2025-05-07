using Elsa.Common.Models;
using Elsa.Workflows;
using Elsa.Workflows.Helpers;
using Elsa.Workflows.Management;
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

public class HomeController(
    ILogger<HomeController> logger,
    IUserContext userContext,
    IWorkflowBusiness workflowBusiness,
    IBookmarkQueue bookmarkQueue,
    IStimulusHasher stimulusHasher,
    IEventPublisher eventPublisher,
    IWorkflowStarter workflowStarter,
    IWorkflowDefinitionImporter workflowDefinitionImporter,
    IWorkflowDefinitionManager workflowDefinitionManager,
    IWorkflowDefinitionStore workflowDefinitionStore,
    IWorkflowDefinitionService workflowDefinitionService
) : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        var tasks = workflowBusiness
            .GetList(x => x.AssignedToUserId == userContext.Id || x.AssignedByUserId == userContext.Id).Result;
        return View(tasks);
    }

    private async Task ResumeBookmarkAsync(Guid id, CancellationToken c = default)
    {
        var stimulus = new CreateTaskStimulus(id);

        var activityTypeName = ActivityTypeNameHelper.GenerateTypeName<AssignTaskToUser>();

        var bookmarkQueueItem = new NewBookmarkQueueItem
        {
            ActivityTypeName = activityTypeName,
            StimulusHash = stimulusHasher.Hash(activityTypeName, stimulus),
        };
        await bookmarkQueue.EnqueueAsync(bookmarkQueueItem, c);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    public async Task<IActionResult> CompleteTask(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await workflowBusiness.GetSingleById(taskId);
        await ResumeBookmarkAsync(taskId, cancellationToken);
        task.Status = WorkflowStatus.Completed;
        await workflowBusiness.Edit(task);
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
        await workflowStarter.StartWorkflowAsync(request);
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