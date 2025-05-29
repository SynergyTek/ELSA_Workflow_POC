using System.Text.Json.Nodes;
using Synergy.App.Data.Model;

namespace Synergy.App.Data.ViewModel;

public class FormViewModel : BaseModel
{
    public TemplateModel Template { get; set; } = new();
    public Dictionary<string,object> Data { get; set; } = new();
}