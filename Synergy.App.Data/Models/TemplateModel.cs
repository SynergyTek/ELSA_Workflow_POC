using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.Models;

public class TemplateModel : BaseModel
{
    [Required] public string Name { get; set; }
    [Required] public string Code { get; set; }
    public string Description { get; set; } = string.Empty;

    [ForeignKey("Table")] public Guid TableId { get; set; }

    [ValidateNever] public TableModel Table { get; set; }

    [Column(TypeName = "jsonb")] public string Json { get; set; } = "{}";
}