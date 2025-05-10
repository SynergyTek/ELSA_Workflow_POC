//using Synergy.App.Common;
////using Synergy.App.DataModel;
////using MongoDB.Driver;
//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Dynamic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Synergy.App.Business.Interface
//{
//    public interface IRepositoryQueryBase<V> where V : class, new()
//    {
//        Task<V> ExecuteQuerySingle(string query, object prms);
//        Task<List<V>> ExecuteQueryList(string query, object prms);
//        Task<DataTable> ExecuteQueryDataTable(string query, object prms);
//        Task<dynamic> ExecuteQuerySingleDynamicObject(string query, object prms);
//        Task ExecuteCommand(string query, object prms);

//        Task<VM> ExecuteQuerySingle<VM>(string query, object prms) where VM : class, new();
//        Task<List<VM>> ExecuteQueryList<VM>(string query, object prms) where VM : class, new();


//        Task<VM> ExecuteScalar<VM>(string query, object prms);
//        Task<List<VM>> ExecuteScalarList<VM>(string query, object prms);
//        Task<DataRow> ExecuteQueryDataRow(string query, object prms);



//    }
//}
