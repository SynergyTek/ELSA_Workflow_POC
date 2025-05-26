using System.Data;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Synergy.App.Business.Interface;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Implementation;

public class TableBusiness(
    IContextBase<TableViewModel, TableModel> repo,
    IServiceProvider sp)
    : BusinessBase<TableViewModel, TableModel>(repo, sp), ITableBusiness
{
    public async Task<CommandResult<TableViewModel>> ManageTemplateTable(TemplateViewModel model, bool ignorePermission,
        Guid parentTemplateId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateStaticTables(string? tableName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ColumnViewModel>> GetTableData(string tableMetadataId, string recordId)
    {
        throw new NotImplementedException();
    }

    public async Task<DataRow> GetTableDataByColumn(string templateCode, Guid templateId, string udfName,
        string udfValue)
    {
        throw new NotImplementedException();
    }

    public async Task<DataRow> GetTableDataByHeaderId(Guid templateId, string headerId)
    {
        throw new NotImplementedException();
    }

    public async Task<DataRow> DeleteTableDataByHeaderId(string templateCode, Guid templateId, string headerId)
    {
        throw new NotImplementedException();
    }

    public async Task<ColumnViewModel> GetColumnByTableName(string schema, string tableName, string columnName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ColumnViewModel>> GetViewColumnByTableName(string schema, string tableName)
    {
        throw new NotImplementedException();
    }

    public async Task EditTableDataByHeaderId(string templateCode, Guid templateId, string headerId,
        Dictionary<string, object> columnsToUpdate)
    {
        throw new NotImplementedException();
    }

    public async Task ChildComp(JArray comps, TableViewModel table, int seqNo)
    {
        throw new NotImplementedException();
    }
}