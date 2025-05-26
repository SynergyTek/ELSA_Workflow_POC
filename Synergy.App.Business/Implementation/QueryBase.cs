using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Synergy.App.Business.Interface;
using Synergy.App.Data;

namespace Synergy.App.Business.Implementation;

public class QueryBase<TV>(IUserContext userContext, IConfiguration configuration)
    : IQueryBase<TV>
    where TV : class, new()
{
    private const string Connection = "PostgreConnection";
    public IUserContext UserContext { get; set; } = userContext;


    public async Task<TV?> ExecuteQuerySingle(string query, object prms)
    {
        return await ExecuteQuerySingle<TV>(query, prms);
    }

    public async Task<List<TV>> ExecuteQueryList(string query, object prms)
    {
        return await ExecuteQueryList<TV>(query, prms);
    }

    protected virtual IDbConnection DbConnection()
    {
        SqlMapper.AddTypeHandler(typeof(TimeSpan), new TimeSpanHandler());
        var connStr = configuration.GetConnectionString(Connection);
        if (connStr != null && connStr.IsNullOrEmpty())
        {
            connStr = configuration.GetSection(Connection).Value;
        }

        var conn = new NpgsqlConnection(connStr);
        conn.Open();
        return conn;
    }

    public async Task<DataTable> ExecuteQueryDataTable(string query, object? prms)
    {
        using var conn = DbConnection();
        var result = await conn.QueryAsync(query, prms);
        return result.ToList().ToDataTable();
    }

    public async Task<DataRow?> ExecuteQueryDataRow(string query, object prms)
    {
        using var conn = DbConnection();
        var result = await conn.QueryAsync(query, prms);
        var dt = result.ToList().ToDataTable();
        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
    }

    public async Task ExecuteCommand(string query, object? prms)
    {
        using var conn = DbConnection();
        await conn.ExecuteAsync(query, prms);
    }

    public async Task<TVm?> ExecuteQuerySingle<TVm>(string query, object prms)
        where TVm : class, new()
    {
        using var conn = DbConnection();
        IEnumerable<TVm?> result = await conn.QueryAsync<TVm>(query, prms);
        return result.FirstOrDefault();
    }

    public async Task<List<TVm>> ExecuteQueryList<TVm>(string query, object prms)
        where TVm : class, new()
    {
        using var conn = DbConnection();
        var result = await conn.QueryAsync<TVm>(query, prms);
        return result.ToList();
    }

    public async Task<List<IDictionary<string, object>>> GetRows(string query, object prms)
    {
        using var conn = DbConnection();
        var result = await conn.QueryAsync(query, prms);
        return result.Select(x => (IDictionary<string, object>)x).ToList();
    }


    public async Task<TVm?> ExecuteScalar<TVm>(string query, object? prms)
    {
        using var conn = DbConnection();
        IEnumerable<TVm?> result = await conn.QueryAsync<TVm>(query, prms);
        return result.FirstOrDefault();
    }

    public async Task<dynamic> ExecuteQuerySingleDynamicObject(string query, object prms)
    {
        var dt = await ExecuteQueryDataTable(query, prms);
        if (dt.Rows.Count <= 0) return null;
        var dy = dt.Rows[0].ToDynamicObject();
        return dy;
    }

    public async Task<List<TVm>> ExecuteScalarList<TVm>(string query, object prms)
    {
        using var conn = DbConnection();
        var result = await conn.QueryAsync<TVm>(query, prms);
        return result.ToList();
    }
}

public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
{
    public override TimeSpan Parse(object value)
    {
        if (value.GetType().Name == "TimeSpan")
        {
            return (TimeSpan)value;
        }

        return TimeSpan.Parse((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, TimeSpan value)
    {
        parameter.Value = Convert.ToString(value);
    }
}