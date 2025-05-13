using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface;

public interface IWorkflowBusiness
{
    Task<CommandResult<bool>> StartWorkflow(string name, Dictionary<string, object> input);
    Task<CommandResult<bool>> ResumeWorkflow(string bookmarkId, Dictionary<string, object> input);
}