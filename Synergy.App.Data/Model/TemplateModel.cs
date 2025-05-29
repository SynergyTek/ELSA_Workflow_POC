using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.Model;

[DisplayColumn("Reference")]
public class TemplateModel : BaseModel
{
    [MaxLength(50)] [Required] public string Name { get; set; } = string.Empty;
    [MaxLength(60)] [Required] public string Reference { get; set; } = string.Empty;
    [MaxLength(60)] [Required] public string Key { get; set; } = string.Empty;
    [MaxLength(300)] public string Description { get; set; } = string.Empty;
    [Column(TypeName = "jsonb")] public string Json { get; set; } = "{}";
}