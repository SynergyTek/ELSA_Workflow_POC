using Synergy.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synergy.App.Data.ViewModels
{
    public class TemplateCategoryViewModel : TemplateCategory
    {
        public LayoutModeEnum? LayoutMode { get; set; }
        public string PopupCallbackMethod { get; set; } = string.Empty;
       // public NtsViewTypeEnum? ViewType { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public string ModuleCode { get; set; } = string.Empty;
        public bool CopyInPage { get; set; }
        public bool CopyInForm { get; set; }
        public bool CopyInNote { get; set; }
        public bool CopyInTask { get; set; }
        public bool CopyInService { get; set; }
        public bool CopyInCustom { get; set; }
        public int ModuleOrder { get; set; }
        public DataActionEnum DataAction { get; set; }
        public long? SequenceOrder { get; set; }
    }

}
