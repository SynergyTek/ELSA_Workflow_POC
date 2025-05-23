using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Synergy.App.Data.Models;

public class BaseModel
{
    [ScaffoldColumn(false)]
    [Key]
    public Guid Id { get; set; }

    [ScaffoldColumn(false)]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [ScaffoldColumn(false)]
    public Guid CreatedBy { get; set; }

    [ScaffoldColumn(false)]
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

    [ScaffoldColumn(false)]
    public Guid LastUpdatedBy { get; set; }

    [ScaffoldColumn(false)]
    public bool IsDeleted { get; set; }

    [ScaffoldColumn(false)]
    public StatusEnum Status { get; set; }
}