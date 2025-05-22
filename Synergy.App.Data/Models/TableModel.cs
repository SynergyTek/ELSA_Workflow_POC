namespace Synergy.App.Data.Models;

public class TableModel : BaseModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public string Schema { get; set; }


    public bool CreateTable { get; set; }
    public string Query { get; set; }
}