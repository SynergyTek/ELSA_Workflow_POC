using Synergy.App.Data.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.ViewModel

{
    public class TemplateViewModel : TemplateModel
    {
        public DataActionEnum DataAction { get; set; }
    }
}