//using Synergy.App.Common;
////using Synergy.App.DataModel;
//using Synergy.App.Repository;
////using Synergy.App.ViewModel;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using Synergy.App.Business.Interface;
//using Synergy.App.Data.Models;
//using Synergy.App.Data.ViewModels;


//namespace Synergy.App.Business
//{
//    public interface ICmsQueryBusiness : IBaseBusiness<TemplateViewModel, Template>
//    {
//        #region CmsBusiness
//        Task<bool> ManageTableExists(TableMetadataViewModel existingTableMetadata);
//        Task<bool?> ManageTableRecordExists(TableMetadataViewModel existingTableMetadata);
//        Task EditTableSchema(TableMetadataViewModel tableMetadata);
//        Task CreateTableSchema(TableMetadataViewModel tableMetadata, bool dropTable);
//        Task TableQueryExecute(string tableQuery);
//        Task<DataTable> GetQueryDataTable(string selectQuery);
      
//        Task<DataTable> GetEditFormTableExistColumn(TableMetadataViewModel tableMetadata);
//        Task<DataTable> GetEditFormTableConstraints(TableMetadataViewModel tableMetadata);
//        Task<List<ColumnMetadataViewModel>> GetForeignKeyColumnByTableMetadata(TableMetadataViewModel tableMetaData);
//        Task<DataTable> GetDataByColumn(ColumnMetadataViewModel column, object columnValue, TableMetadataViewModel tableMetaData, string excludeId);
   
//        Task<string> GetLatestMigrationScript();
//      //  Task<TemplateViewModel> ExecuteMigrationScript(string script);
//        Task<List<string>> GetAllMigrationsList();
//      //  Task<List<TemplateViewModel>> GetTemplateListByTemplateType(int i);
//        #endregion


       

      
    
//        Task<TableMetadataViewModel> GetViewableColumnMetadataListData(string schemaName, string tableName);
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForFormData(string tableMetadataId);
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForNoteData(string tableMetadataId);
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForTaskData(string tableMetadataId);
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForTaskData2();
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForServiceData(string tableMetadataId);
//        Task<List<ColumnMetadataViewModel>> GetViewableForiegnKeyColumnListForServiceData1(string tableMetadataId);
      
//        Task<DataTable> GetTableData(string tableMetadataId, string recordId, string name, string schema);
//        Task<DataTable> GetTableDataByColumnData(string schema, string name, string udfName, string udfValue);
//        Task<DataTable> GetTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
//        Task<DataTable> DeleteTableDataByHeaderIdData(string schema, string name, string fieldName, string headerId);
//        Task EditTableDataByHeaderIdData(string schema, string name, List<string> columnKeys, string fieldName, string headerId);
//        Task<DataTable> GetViewColumnByTableNameData(string schema, string tableName);
      
//    }
//}
