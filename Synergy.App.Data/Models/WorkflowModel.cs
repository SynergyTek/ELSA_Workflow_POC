using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Synergy.App.Data.Models;

public class WorkflowModel : BaseModel
{
    public string Title { get; set; } = string.Empty;

    [ForeignKey("AssignedToUser")] public Guid AssignedToUserId { get; set; }
    public User AssignedToUser { get; set; }
    [ForeignKey("AssignedByUser")] public Guid AssignedByUserId { get; set; }
    public User AssignedByUser { get; set; }
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Inactive;
}