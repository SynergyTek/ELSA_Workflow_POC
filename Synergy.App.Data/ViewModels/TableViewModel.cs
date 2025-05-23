using Synergy.App.Data.Models;
//using Synergy.App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Synergy.App.Data.ViewModels


{
    public class TableViewModel : TableModel
    {
        public List<ColumnViewModel> Columns { get; set; }

        public string OldName { get; set; }
        public string OldSchema { get; set; }

        public bool IgnorePermission { get; set; }
        public Guid TemplateId { get; set; }
        public string Json { get; set; }

        public List<TableViewModel> ChildTable { get; set; }
        public bool IsChildTable { get; set; }
        public string TemplateCode { get; set; }
        public DataActionEnum DataAction { get; set; }
        public long? SequenceOrder { get; set; }
        public StatusEnum Status { get; set; }
    }
}