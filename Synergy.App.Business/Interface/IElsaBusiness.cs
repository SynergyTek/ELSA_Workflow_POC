using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface;

public interface IElsaBusiness
{
    Task<CommandResult<bool>> StartWorkflow(string name, Dictionary<string, object> input);
    Task<CommandResult<bool>> ResumeWorkflow(string bookmarkId, Dictionary<string, object> input);
    Task<CommandResult<List<WorkflowViewModel>>> GetInstances();
    Task<CommandResult<WorkflowViewModel>> GetInstanceById(string id);
}