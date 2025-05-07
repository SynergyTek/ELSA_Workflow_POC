using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Models;

public class Leave : BaseModel
{
    [ForeignKey("AppliedBy")] public Guid AppliedById { get; set; }
    public User AppliedBy { get; set; }
    public LeaveType LeaveType { get; set; }
    public string Reason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}