using System.Data;
using Newtonsoft.Json.Linq;
using Synergy.App.Data.Model;
using Synergy.App.Data.ViewModel;

namespace Synergy.App.Business.Interface
{
    public interface ITableBusiness : IBusinessBase<TableViewModel, TableModel>
    {
        Task<CommandResult<TableViewModel>> ManageTemplateTable(TemplateViewModel model, bool ignorePermission,
            Guid parentTemplateId);

        Task UpdateStaticTables(string? tableName);
        Task<List<ColumnViewModel>> GetTableData(string tableMetadataId, string recordId);
        Task<DataRow> GetTableDataByColumn(string templateCode, Guid templateId, string udfName, string udfValue);
        Task<DataRow> GetTableDataByHeaderId(Guid templateId, string headerId);
        Task<DataRow> DeleteTableDataByHeaderId(string templateCode, Guid templateId, string headerId);
        Task<ColumnViewModel> GetColumnByTableName(string schema, string tableName, string columnName);
        Task<List<ColumnViewModel>> GetViewColumnByTableName(string schema, string tableName);

        Task EditTableDataByHeaderId(string templateCode, Guid templateId, string headerId,
            Dictionary<string, object> columnsToUpdate);

        public Task ChildComp(JArray comps, TableViewModel table, int seqNo);
    }
}