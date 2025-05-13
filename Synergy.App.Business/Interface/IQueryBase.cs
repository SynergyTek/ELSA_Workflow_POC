using System.Data;

namespace Synergy.App.Business.Interface
{
    public interface IQueryBase<TV> where TV : class, new()
    {
        Task<TV?> ExecuteQuerySingle(string query, object parameters);
        Task<List<TV>> ExecuteQueryList(string query, object parameters);
        Task<DataTable> ExecuteQueryDataTable(string query, object? parameters);
        Task<dynamic> ExecuteQuerySingleDynamicObject(string query, object parameters);
        Task ExecuteCommand(string query, object? parameters);

        Task<TVm?> ExecuteQuerySingle<TVm>(string query, object parameters) where TVm : class, new();
        Task<List<TVm>> ExecuteQueryList<TVm>(string query, object parameters) where TVm : class, new();


        Task<TVm?> ExecuteScalar<TVm>(string query, object? parameters);
        Task<List<TVm>> ExecuteScalarList<TVm>(string query, object parameters);
        Task<DataRow?> ExecuteQueryDataRow(string query, object parameters);



    }
}
