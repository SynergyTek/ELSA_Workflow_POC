using AutoMapper;
using Npgsql;

namespace Synergy.App.Data;

public static class Extension
{
    public static string GetFullName(this string name)
    {
        return name;
    }

    public static string GetFullName(this string name, string? lastName)
    {
        return $"{name} {lastName}";
    }

    public static string GetFullName(this string name, string? lastName, string? middleName)
    {
        return $"{name} {middleName} {lastName}";
    }
    public static bool IsNullOrEmpty(this string s)
    {
        return String.IsNullOrEmpty(s);
    }
    public static bool IsNotNullAndNotEmpty(this string s)
    {
        return !string.IsNullOrEmpty(s);
    }
    public static bool IsNullOrEmptyOrValue(this string s, string val)
    {
        return String.IsNullOrEmpty(s) || s == val;
    }
    public static List<TVm> ToViewModelList<TVm, TDm>(this List<TDm> source, IMapper autoMapper)
    {
        return autoMapper.Map<List<TDm>, List<TVm>>(source);
    }
    public static TVm ToViewModel<TVm, TDm>(this TDm source, IMapper autoMapper)
    {
        return autoMapper.Map<TDm, TVm>(source);
    }
    public static void AddParameters(this NpgsqlParameterCollection queryParam, Dictionary<string, object>? prms)
    {
        if (prms == null) return;
        foreach (var item in prms)
        {
            queryParam.Add(new NpgsqlParameter { ParameterName = item.Key, Value = item.Value });
        }

    }
}
