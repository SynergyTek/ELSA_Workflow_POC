using Synergy.App.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.ViewModels

{
    public class TemplateViewModel : TemplateModel
    {
        public DataActionEnum DataAction { get; set; }
    }
}