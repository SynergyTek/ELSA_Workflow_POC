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
//    public interface IColumnMetadataBusiness : IBaseBusiness<ColumnMetadataViewModel, ColumnMetadata>
//    {
//        Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(string tableMetadataId,  bool includeForiegnKeyTableColumns = true);
//        // Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(string tableMetadataId, bool includeForiegnKeyTableColumns = true);
//        Task<List<ColumnMetadataViewModel>> GetViewableColumnMetadataList(string schemaName, string tableName, bool includeForiegnKeyTableColumns = true);
//    }
//}
