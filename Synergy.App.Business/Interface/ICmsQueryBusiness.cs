using System.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface ICmsQueryBusiness : IBaseBusiness<TemplateViewModel, Template>
    {
        #region CmsBusiness
        Task<bool> ManageTableExists(TableMetadataViewModel existingTableMetadata);
        Task<bool?> ManageTableRecordExists(TableMetadataViewModel existingTableMetadata);
        Task EditTableSchema(TableMetadataViewModel tableMetadata);
        Task CreateTableSchema(TableMetadataViewModel tableMetadata, bool dropTable);
        Task TableQueryExecute(string tableQuery);
        Task<DataTable> GetQueryDataTable(string selectQuery);

        Task<DataTable> GetEditFormTableExistColumn(TableMetadataViewModel tableMetadata);
        Task<DataTable> GetEditFormTableConstraints(TableMetadataViewModel tableMetadata);
        Task<List<ColumnMetadataViewModel>> GetForeignKeyColumnByTableMetadata(TableMetadataViewModel tableMetaData);
        Task<DataTable> GetDataByColumn(ColumnMetadataViewModel column, object columnValue, TableMetadataViewModel tableMetaData, Guid excludeId);

        Task<string> GetLatestMigrationScript();
      //  Task<TemplateViewModel> ExecuteMigrationScript(string script);
        Task<List<string>> GetAllMigrationsList();
      //  Task<List<TemplateViewModel>> GetTemplateListByTemplateType(int i);
        #endregion






        Task<TableMetadataViewModel> GetViewableColumnMetadataListData(string schemaName, string tableName);
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForFormData(Guid tableMetadataId);
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForNoteData(Guid tableMetadataId);
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForTaskData(Guid tableMetadataId);
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForTaskData2();
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForServiceData(Guid tableMetadataId);
        Task<List<ColumnMetadataViewModel>> GetViewableForeignKeyColumnListForServiceData1(Guid tableMetadataId);

        Task<DataTable> GetTableData(Guid tableMetadataId, string recordId, string name, string schema);
        Task<DataTable> GetTableDataByColumnData(string schema, string name, string udfName, string udfValue);
        Task<DataTable> GetTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
        Task<DataTable> DeleteTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
        Task EditTableDataByHeaderIdData(string schema, string name, List<string> columnKeys, string fieldName, string headerId);
        Task<DataTable> GetViewColumnByTableNameData(string schema, string tableName);

    }
}
