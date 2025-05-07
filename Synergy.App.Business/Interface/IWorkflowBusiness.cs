using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface IWorkflowBusiness : IBaseBusiness<WorkflowViewModel,Workflow>
{
 Task<WorkflowViewModel?> AssignTaskToUser(string title,Guid userId);
 Task<bool> AssignTaskToRole(string groupId);
}