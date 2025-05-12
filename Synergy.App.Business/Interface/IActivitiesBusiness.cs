using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface IActivitiesBusiness: IBaseBusiness<WorkflowViewModel, Workflow>
{
    Task<WorkflowViewModel> AssignTaskToUser(string title, Guid userId, Guid byUserId);
    Task<WorkflowViewModel> AssignTaskToRole(string title, string roleCode, Guid byUserId);
}