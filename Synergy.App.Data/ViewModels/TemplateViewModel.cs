using Synergy.App.Data.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Synergy.App.Data.ViewModels

{
    public class TemplateViewModel : TemplateModel
    {
        public bool Select { get; set; }
        public bool IsChildTable { get; set; }
        public DataActionEnum DataAction { get; set; }
        public string? RecordId { get; set; }
        public Guid ParentId { get; set; }
        [ValidateNever] public List<ColumnViewModel> ColumnList { get; set; }
        [ValidateNever] public bool EnableSaveButton { get; set; }
        [ValidateNever] public string SaveButtonText { get; set; }
        [ValidateNever] public string SaveButtonCss { get; set; }
        [ValidateNever] public bool EnableBackButton { get; set; }
        [ValidateNever] public string BackButtonText { get; set; }
        [ValidateNever] public string BackButtonCss { get; set; }
    }
}