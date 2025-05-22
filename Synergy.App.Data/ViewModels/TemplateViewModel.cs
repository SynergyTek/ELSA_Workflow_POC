using Synergy.App.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.App.Data.ViewModels

{
    public class TemplateViewModel : TemplateModel
    {
        public List<ColumnMetadataViewModel> ColumnList { get; set; }
        public bool Select { get; set; }
        public bool IsChildTable { get; set; }
        public DataActionEnum DataAction { get; set; }
        public string? RecordId { get; set; }
        public Guid ParentId { get; set; }
        public bool EnableSaveButton { get; set; }
        public string SaveButtonText { get; set; }
        public string SaveButtonCss { get; set; }
        public bool EnableBackButton { get; set; }
        public string BackButtonText { get; set; }
        public string BackButtonCss { get; set; }
    }
}