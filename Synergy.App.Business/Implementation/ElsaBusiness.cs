using Microsoft.AspNetCore.Identity;
using Synergy.App.Business.Interface;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Implementation;

public class ElsaBusiness(
    IContextBase<WorkflowViewModel, WorkflowModel> repo,
    IServiceProvider sp,
    UserManager<User> userManager)
    : BusinessBase<WorkflowViewModel, WorkflowModel>(repo, sp), IElsaBusiness
{
    public async Task<WorkflowViewModel> AssignTaskToUser(string title, string email, User byUser)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception($"No user with given email {email} found");
        }

        var reviewModel = new WorkflowViewModel
        {
            CreatedBy = byUser,
            UpdatedBy = byUser,
            AssignedToUser = user,
            AssignedByUser = byUser,
            Title = title
        };

        var model = await Create(reviewModel);

        return model.Item;
    }

    public async Task<WorkflowViewModel> AssignTaskToRole(string title, string roleCode, User byUser)
    {
        var user = userManager.GetUsersInRoleAsync(roleCode).Result.First();
        if (user == null)
        {
            throw new Exception($"No user in given role {roleCode} found");
        }

        var reviewModel = new WorkflowViewModel
        {
            CreatedBy = byUser,
            UpdatedBy = byUser,
            AssignedToUser = user,
            AssignedByUser = byUser,
            Title = title,
            Status = WorkflowStatus.Inprogress
        };

        var model = await Create(reviewModel);

        return model.Item;
    }
}