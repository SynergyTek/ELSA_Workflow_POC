using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Synergy.App.Data.Model;

public class WorkflowModel : BaseModel
{
    public string Title { get; set; } = string.Empty;

    public User AssignedToUser { get; set; }
    public User AssignedByUser { get; set; }
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Inactive;
}