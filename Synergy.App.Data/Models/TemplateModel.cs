using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Models;

public class TemplateModel : BaseModel
{

    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    [ForeignKey("TableMetadata")] public Guid TableMetadataId { get; set; }
    public TableMetadataModel TableMetadata { get; set; }


    public string Json { get; set; }
}