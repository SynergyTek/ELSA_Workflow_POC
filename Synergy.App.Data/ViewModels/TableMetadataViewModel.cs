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
    public class TableMetadataViewModel:TableMetadata
    {
       // public Guid Id { get; set; }  // from BaseModel
       
        // ViewModel-specific properties
        public List<ColumnMetadataViewModel> ColumnMetadatas { get; set; }
        public List<ColumnMetadataViewModel> ColumnMetadataView { get; set; }

        public string? ModuleName { get; set; }
        public string? ColumnMetaDetails { get; set; }
        public string? Type { get; set; }
        public string OldName { get; set; }
        public string OldSchema { get; set; }

        public bool IgnorePermission { get; set; }
        public Guid TemplateId { get; set; }
        public string Json { get; set; }

        public List<TableMetadataViewModel> ChildTable { get; set; }
        public bool IsChildTable { get; set; }
        public string TemplateCode { get; set; }
        public DataActionEnum DataAction { get; set; }
        public long? SequenceOrder { get; set; }
    }
}
