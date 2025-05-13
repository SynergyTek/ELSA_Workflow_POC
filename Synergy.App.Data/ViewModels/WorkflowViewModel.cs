using Synergy.App.Data.Models;

namespace Synergy.App.Data.ViewModels;

public class WorkflowViewModel : Workflow
{
    public string InstanceStatus { get; set; } = string.Empty;
    public string WorkflowName { get; set; } = string.Empty;
    public string WorkflowType { get; set; } = string.Empty;
    public string WorkflowStatus { get; set; } = string.Empty;
    public string BookmarkId { get; set; } = string.Empty;
}