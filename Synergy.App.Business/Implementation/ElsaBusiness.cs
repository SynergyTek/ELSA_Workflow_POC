using Elsa.Workflows.Management;
using Elsa.Workflows.Management.Filters;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Options;
using Synergy.App.Business.Interface;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Implementation;

public class ElsaBusiness(
    IQueryBase<WorkflowViewModel?> repo,
    IWorkflowDefinitionService workflowDefinitionService,
    IWorkflowStarter workflowStarter,
    IBookmarkQueue bookmarkQueue)
    : IElsaBusiness
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
            BookmarkId = bookmarkId,
            Options = new ResumeBookmarkOptions
            {
                Input = input
            }
        };
        await bookmarkQueue.EnqueueAsync(bookmarkQueueItem);
        return CommandResult<bool>.Instance(false);
    }

    public async Task<CommandResult<List<WorkflowViewModel>>> GetInstances()
    {
        var query = """
                    select wi."Status" WorkflowStatus, wi."SubStatus" InstanceStatus, b."Id" BookmarkId 
                    from "Elsa"."Bookmarks" b
                    left join "Elsa"."WorkflowInstances" wi on b."WorkflowInstanceId" = wi."Id"
                    """;
        var result = await repo.ExecuteQueryList(query, new { });
        return CommandResult<List<WorkflowViewModel>>.Instance(result);
    }

    public async Task<CommandResult<WorkflowViewModel>> GetInstanceById(string id)
    {
        var query = """
                    select wi."Status", wi."SubStatus", b."Id" from "Elsa"."Bookmarks" b
                    left join "Elsa"."WorkflowInstances" wi on b."WorkflowInstanceId" = wi."Id"
                    where wi."Id"== @id
                    """;
        var result = await repo.ExecuteQuerySingle(query, new { id });
        return CommandResult<WorkflowViewModel>.Instance(result);
    }
}