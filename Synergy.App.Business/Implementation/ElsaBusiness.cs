using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class ElsaBusiness(
    IContextBase<WorkflowViewModel, Workflow> repo,
    IServiceProvider sp,
    UserManager<User> userManager)
    : BusinessBase<WorkflowViewModel, Workflow>(repo, sp), IElsaBusiness
{

    public async Task<WorkflowViewModel> AssignTaskToUser(string title, string email, Guid byUserId)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception($"No user with given email {email} found");
        }
        var reviewModel = new WorkflowViewModel
        {
            CreatedBy = byUserId,
            LastUpdatedBy = byUserId,
            AssignedToUserId = user.Id,
            AssignedByUserId = byUserId,
            Title = title
        };

        var model = await Create(reviewModel);

        return model.Item;
    }

    public async Task<WorkflowViewModel> AssignTaskToRole(string title, string roleCode, Guid byUserId)
    {
        var user = userManager.GetUsersInRoleAsync(roleCode).Result.First();
        if (user == null)
        {
            throw new Exception($"No user in given role {roleCode} found");
        }
        var reviewModel = new WorkflowViewModel
        {
            CreatedBy = byUserId,
            LastUpdatedBy = byUserId,
            AssignedToUserId = user.Id,
            AssignedByUserId = byUserId,
            Title = title
        };

        var model = await Create(reviewModel);

        return model.Item;
    }
}