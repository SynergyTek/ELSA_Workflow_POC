using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.Models;

public class BaseModel
{
    [ScaffoldColumn(false)] [Key] public Guid Id { get; set; }

    [ScaffoldColumn(false)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ScaffoldColumn(false)]
    [ValidateNever]
    public User CreatedBy { get; set; }


    [ScaffoldColumn(false)] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ScaffoldColumn(false)]
    [ValidateNever]
    public User UpdatedBy { get; set; }

    [ScaffoldColumn(false)] public bool IsDeleted { get; set; }
}