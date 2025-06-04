using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Options;
using Microsoft.AspNetCore.Mvc;
using Synergy.App.Business;

namespace Synergy.Elsa.Server.Controllers;

[Route("elsa/[controller]/[action]")]
[ApiController]
public class CoreController(
    IHttpContextAccessor accessor,
    IWorkflowStarter workflowStarter,
    IWorkflowDefinitionService workflowDefinitionService,
    IBookmarkQueue bookmarkQueue
) : ControllerBase
{
    [HttpGet]
    public IActionResult Version()
    {
        // This is a placeholder for the actual implementation.
        // You can return a view or data as needed.
        return Ok("Synergy Core with Elsa Version 1.0");
    }

    [HttpPost]
    public async Task<CommandResult<bool>> StartWorkflow(string name, [FromBody] Dictionary<string, object> input)
    {
        var preWorkflow = await workflowDefinitionService.FindWorkflowDefinitionAsync(new WorkflowDefinitionFilter
        {
            Name = name
        });
        if (preWorkflow == null) return CommandResult<bool>.Instance(false);
        var request = new StartWorkflowRequest
        {
            WorkflowDefinitionHandle = new WorkflowDefinitionHandle
            {
                DefinitionId = preWorkflow.DefinitionId
            },
            Input = input
        };
        //Header table
        await workflowStarter.StartWorkflowAsync(request);

        return CommandResult<bool>.Instance();
    }

    [HttpGet]
    public async Task<CommandResult<bool>> ResumeWorkflow(string bookmarkId, Dictionary<string, object> input)
    {
        var bookmarkQueueItem = new NewBookmarkQueueItem
        {
            BookmarkId = bookmarkId,
            Options = new ResumeBookmarkOptions
            {
                Input = input
            }
        };
        await bookmarkQueue.EnqueueAsync(bookmarkQueueItem);
        return CommandResult<bool>.Instance(false);
    }
}