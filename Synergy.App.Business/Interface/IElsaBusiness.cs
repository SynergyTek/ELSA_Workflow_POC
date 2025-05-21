using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface IElsaBusiness: IBusinessBase<WorkflowViewModel, WorkflowModel>
{
    Task<WorkflowViewModel> AssignTaskToUser(string title, string email, Guid byUserId);
    Task<WorkflowViewModel> AssignTaskToRole(string title, string roleCode, Guid byUserId);
}