using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class WorkflowBusiness(
    IContextBase<WorkflowViewModel, Workflow> repo,
    IServiceProvider sp,
    UserManager<User> userManager,
    IWorkflowDefinitionService workflowDefinitionService,
    IWorkflowStarter workflowStarter,
    IBookmarkQueue bookmarkQueue
)
    : IWorkflowBusiness
{
    public async Task<CommandResult<bool>> StartWorkflow(string name, Dictionary<string, object> input)
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
        await workflowStarter.StartWorkflowAsync(request);

        return CommandResult<bool>.Instance(true);
    }

    public async Task<CommandResult<bool>> ResumeWorkflow(string bookmarkId, Dictionary<string, object> input)
    {
        var bookmarkQueueItem = new NewBookmarkQueueItem
        {
            BookmarkId = bookmarkId
        };
        await bookmarkQueue.EnqueueAsync(bookmarkQueueItem);
        return CommandResult<bool>.Instance(false);
    }
}