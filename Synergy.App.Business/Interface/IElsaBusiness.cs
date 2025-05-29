using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface;

public interface IElsaBusiness: IBusinessBase<WorkflowViewModel, WorkflowModel>
{
    Task<WorkflowViewModel> AssignTaskToUser(string title, string email, User byUser);
    Task<WorkflowViewModel> AssignTaskToRole(string title, string roleCode, User byUser);
}