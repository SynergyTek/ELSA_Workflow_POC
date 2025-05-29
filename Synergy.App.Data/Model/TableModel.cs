using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Synergy.App.Common;

namespace Synergy.App.Data.Model;

public class TableModel : BaseModel
{
    public TemplateModel Template { get; set; }
    [MaxLength(16)] public string Schema { get; set; } = ApplicationConstant.Database.Schema.Form;


    public bool CreateTable { get; set; }
}