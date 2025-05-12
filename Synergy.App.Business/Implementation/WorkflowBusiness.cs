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
    UserManager<User> userManager)
    : IWorkflowBusiness
{
    public async Task<CommandResult<bool>> StartWorkflow(WorkflowViewModel workflow)
    {
        return CommandResult<bool>.Instance(false);
    }

    public async Task<CommandResult<bool>> ResumeWorkflow(WorkflowViewModel workflow)
    {
        return CommandResult<bool>.Instance(false);
    }
}