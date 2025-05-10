//using Synergy.App.Business.Interface;
//using Synergy.App.Common;
//using Synergy.App.Data;
//using Synergy.App.Data.ViewModels;

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Synergy.App.Data.Models;

//namespace Synergy.App.Business
//{

//    public interface ICmsBusiness : IBaseBusiness<TemplateViewModel, Template>
//    {
//        Task ManageTable(TableMetadataViewModel tableMetadata);
  

    
//        Task<CommandResult<FormTemplateViewModel>> ManageForm(FormTemplateViewModel model);
//        Task<DataTable> GetData(string schemaName, string tableName, string columns = null, string filter = null, string orderbyColumns = null, OrderByEnum orderby = OrderByEnum.Ascending, string where = null, bool ignoreJoins = false, string returnColumns = null, int? limit = null, int? skip = null, bool enableLocalization = false, string lang = null);
      
//        void GetFormUdfDetails(FormTemplateViewModel model);
//        Task<FormTemplateViewModel> GetFormDetails(FormTemplateViewModel viewModel);
//        Task<DataTable> GetDataListByTemplate(string templateCode, string templateId, string where = null);
     
//        Task<Tuple<bool, string>> CreateForm(string data, string pageId, string templateCode = null);
//        Task<Tuple<bool, string>> EditForm(string recordId, string data, string pageId, string templateCode = null);
  
//        Task<string> GetLatestMigrationScript();
//        Task<string> ExecuteMigrationScript(string script);
     
//        Task<List<string>> GetAllMigrationsList();
//        Task ManageTable(TableMetadataViewModel model);
//    }
//}
