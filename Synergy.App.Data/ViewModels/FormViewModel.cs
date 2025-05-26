using System.Text.Json.Nodes;
using Synergy.App.Data.Models;

namespace Synergy.App.Data.ViewModels;

public class FormViewModel : BaseModel
{
    public TemplateModel Template { get; set; } = new();
    public Dictionary<string,object> Data { get; set; } = new();
}