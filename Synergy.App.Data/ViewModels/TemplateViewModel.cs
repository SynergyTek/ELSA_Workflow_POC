using Synergy.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.App.Data.ViewModels

{
    public class TemplateViewModel:Template
    {
      
        public string? TemplateCategoryName { get; set; }

      
        public string ?TableMetadataName { get; set; } 

       
        public string ?UdfTemplateName { get; set; } 

       
        public string? UdfTableMetadataName { get; set; }
        public List<ColumnMetadataViewModel> ColumnList { get; set; }

        // Extra ViewModel-only properties (optional, based on your use case)
        public string CategoryCode { get; set; } = string.Empty;
        public string TemplateColor { get; set; } = string.Empty;
        public string ModuleCodes { get; set; } = string.Empty;
        public bool Select { get; set; }
        public bool IsChildTable { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DataActionEnum DataAction { get; set; }
        public long? SequenceOrder { get; set; }
    }
}
