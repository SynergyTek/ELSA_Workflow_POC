using System.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface ICmsQueryBusiness : IBusinessBase<TemplateViewModel, TemplateModel>
    {
        #region CmsBusiness
        Task<bool> ManageTableExists(TableViewModel existingTable);
        Task<bool?> ManageTableRecordExists(TableViewModel existingTable);
        Task EditTableSchema(TableViewModel table);
        Task CreateTableSchema(TableViewModel table, bool dropTable);
        Task TableQueryExecute(string tableQuery);
        Task<DataTable> GetQueryDataTable(string selectQuery);

        Task<DataTable> GetEditFormTableExistColumn(TableViewModel table);
        Task<DataTable> GetEditFormTableConstraints(TableViewModel table);
        Task<List<ColumnViewModel>> GetForeignKeyColumnByTableMetadata(TableViewModel tableMetaData);
        Task<DataTable> GetDataByColumn(ColumnViewModel column, object columnValue, TableViewModel tableMetaData, Guid excludeId);

        Task<string?> GetLatestMigrationScript();
      //  Task<TemplateViewModel> ExecuteMigrationScript(string script);
        Task<List<string>> GetAllMigrationsList();
      //  Task<List<TemplateViewModel>> GetTemplateListByTemplateType(int i);
        #endregion






        Task<TableViewModel?> GetViewableColumnMetadataListData(string schemaName, string tableName);
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForFormData(Guid tableMetadataId);
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForNoteData(Guid tableMetadataId);
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForTaskData(Guid tableMetadataId);
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForTaskData2();
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForServiceData(Guid tableMetadataId);
        Task<List<ColumnViewModel>> GetViewableForeignKeyColumnListForServiceData1(Guid tableMetadataId);

        Task<DataTable> GetTableData(Guid tableMetadataId, string recordId, string name, string schema);
        Task<DataTable> GetTableDataByColumnData(string schema, string name, string udfName, string udfValue);
        Task<DataTable> GetTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
        Task<DataTable> DeleteTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
        Task EditTableDataByHeaderIdData(string schema, string name, List<string> columnKeys, string fieldName, string headerId);
        Task<DataTable> GetViewColumnByTableNameData(string schema, string tableName);

    }
}
