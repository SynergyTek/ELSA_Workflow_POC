using Synergy.App.Data.Model;

namespace Synergy.App.Data.ViewModel;

public class WorkflowViewModel : WorkflowModel
{
    public string InstanceStatus { get; set; } = string.Empty;
    public string WorkflowName { get; set; } = string.Empty;
    public string WorkflowType { get; set; } = string.Empty;
    public string WorkflowStatus { get; set; } = string.Empty;
    public string BookmarkId { get; set; } = string.Empty;
}