using System.ComponentModel.DataAnnotations.Schema;

namespace Synergy.App.Data.Models;

public class ColumnMetadataModel:BaseModel
{
     public string Name { get; set; }
        public bool IsDefaultDisplayColumn { get; set; }
        public string LabelName { get; set; }
        public string Alias { get; set; }
        public bool IsNullable { get; set; }
        public DataColumnTypeEnum DataType { get; set; }
        public UdfUITypeEnum UdfUIType { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsVirtualColumn { get; set; }
        public bool IsVirtualForeignKey { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsSystemColumn { get; set; }
        public bool IsUniqueColumn { get; set; }
        public bool IsLogColumn { get; set; }
        public bool IsMultiValueColumn { get; set; }
        public bool IsUdfColumn { get; set; }
        public bool IsHiddenColumn { get; set; }
        public bool HideForeignKeyTableColumns { get; set; }
        public bool IsReferenceColumn { get; set; }

        [ForeignKey("ForeignKeyTable")]
        public Guid ForeignKeyTableId { get; set; }
        public TableMetadataModel ForeignKeyTable { get; set; }
        public string ForeignKeyTableName { get; set; }
        public string ForeignKeyTableAliasName { get; set; }
        public string ForeignKeyTableSchemaName { get; set; }

        [ForeignKey("ForeignKeyColumn")]
        public Guid ForeignKeyColumnId { get; set; }
        public string ForeignKeyColumnName { get; set; }
        public ColumnMetadataModel ForeignKeyColumn { get; set; }
        public string ForeignKeyDisplayColumnLabelName { get; set; }
        public string ForeignKeyDisplayColumnAlias { get; set; }
        public DataColumnTypeEnum ForeignKeyDisplayColumnDataType { get; set; }
        public string ForeignKeyConstraintName { get; set; }

        [ForeignKey("TableMetadataModel")]
        public Guid TableMetadataId { get; set; }
        public TableMetadataModel TableMetadata { get; set; }

        public string[] EditableBy { get; set; }
        public string[] ViewableBy { get; set; }
        public string[] EditableContext { get; set; }
        public string[] ViewableContext { get; set; }

}