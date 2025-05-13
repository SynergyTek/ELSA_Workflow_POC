using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface IColumnMetadataBusiness : IBusinessBase<ColumnMetadataViewModel, ColumnMetadata>
    {
        Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(Guid tableMetadataId,
            bool includeForeignKeyTableColumns = true);

        Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(string schemaName, string tableName,
            bool includeForeignKeyTableColumns = true);
    }
}