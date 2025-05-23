using Synergy.App.Data.Models;
using Synergy.App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Synergy.App.Data.ViewModels
{
    public class ColumnViewModel : ColumnModel
    {
        [UIHint("ForeignKeyTable")] public new Guid ForeignKeyTableId { get; set; }
        public new string ForeignKeyTableName { get; set; }

        [UIHint("ForeignKeyColumn")] public new Guid ForeignKeyColumnId { get; set; }
        public new string ForeignKeyColumnName { get; set; }

        [UIHint("ForeignKeyDisplayColumn")] public Guid ForeignKeyDisplayColumnId { get; set; }
        public string ForeignKeyDisplayColumnName { get; set; }

        [UIHint("DataColumnType")] public string DataTypeString { get; set; }

        public object Value { get; set; }

        public string NtsStatusCode { get; set; }
        public bool IsChecked { get; set; }

        public bool IgnorePermission { get; set; }
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }

        public string ForeignKeyBaseColumnName { get; set; }
        public string DataTypestr { get; set; }

        public bool IsForeignKeyTableColumn { get; set; }

        public string TableName { get; set; }
        public string TableSchemaName { get; set; }
        public string TableAliasName { get; set; }
        public string TableMetadataName { get; set; }
        public DataActionEnum DataAction { get; set; }
        public long? SequenceOrder { get; set; }

        public StatusEnum Status { get; set; }
    }
}