using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.Models;

public class TemplateModel : BaseModel
{
    [MaxLength(50)] [Required] public string Name { get; set; } = String.Empty;
    [MaxLength(60)] [Required] public string Code { get; set; } = string.Empty;
    [MaxLength(300)] public string Description { get; set; } = string.Empty;
    [Column(TypeName = "jsonb")] public string Json { get; set; } = "{}";
}