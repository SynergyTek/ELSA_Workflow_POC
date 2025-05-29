using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Model;

[DisplayColumn("Alias")]
public class ColumnModel : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Alias { get; set; } = string.Empty;
    public TableModel Table { get; set; }

    public bool IsNullable { get; set; }
    public DataColumnTypeEnum DataType { get; set; }
    public bool IsForeignKey { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsSystemColumn { get; set; }
    public bool IsUniqueColumn { get; set; }
    public bool IsVisible { get; set; } = true;

}