using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface IColumnMetadataBusiness : IBusinessBase<ColumnViewModel, ColumnModel>
    {
        Task<List<ColumnViewModel>> GetViewableColumnMetadataList(Guid tableMetadataId,
            bool includeForeignKeyTableColumns = true);

        Task<List<ColumnViewModel>> GetViewableColumnMetadataList(string schemaName, string tableName,
            bool includeForeignKeyTableColumns = true);
    }
}