using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class WorkflowBusiness(
    IContextBase<WorkflowViewModel, Workflow> repo,
    IUserContext userContext)
    : BaseBusiness<WorkflowViewModel, Workflow>(repo), IWorkflowBusiness
{
    public async Task<WorkflowViewModel?> AssignTaskToUser(string title, Guid userId)
    {

        var reviewModel = new WorkflowViewModel
        {
            CreatedBy = userContext.Id,
            LastUpdatedBy = userContext.Id,
            AssignedToUserId = userId,
            AssignedByUserId = userContext.Id,
            Title = title
        };

        var model = await Create(reviewModel);

        return model.Item;
    }

    public async Task<bool> AssignTaskToRole(string groupId)
    {
        // Simulate assigning a task to a role
        await Task.Delay(1000); // Simulate some delay
        return true;
    }
}