using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Models;

public class Notification : BaseModel
{
    public required string Text { get; set; }
    [ForeignKey("User")] public Guid UserId { get; set; }
    public required User User { get; set; }

    public bool IsRead { get; set; }
}