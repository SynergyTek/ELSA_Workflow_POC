using System.Data;
using Synergy.App.Data;
using Synergy.App.Data.Models;
using Synergy.App.Data.ViewModels;

namespace Synergy.App.Business.Interface
{
    public interface ICmsBusiness : IBusinessBase<TemplateViewModel, TemplateModel>
    {
        Task ManageTable(TableMetadataViewModel tableMetadata);
        //
        //
        // Task<CommandResult<TemplateViewModel>> ManageForm(TemplateViewModel model);
        //
        // Task<DataTable> GetData(string schemaName, string tableName, string? columns = null, string? filter = null,
        //     string? orderbyColumns = null, OrderByEnum orderby = OrderByEnum.Ascending, string where = null,
        //     bool ignoreJoins = false, string returnColumns = null, int? limit = null, int? skip = null,
        //     bool enableLocalization = false, string lang = null);
        //
        // void GetFormUdfDetails(TemplateViewModel model);
        // Task<TemplateViewModel> GetFormDetails(TemplateViewModel viewModel);
        // Task<DataTable> GetDataListByTemplate(string templateCode, Guid templateId, string? where = null);
        //
        // Task<Tuple<bool, string>> CreateForm(string data, string pageId, string? templateCode = null);
        // Task<Tuple<bool, string>> EditForm(Guid recordId, string data, string pageId, string? templateCode = null);

    }
}