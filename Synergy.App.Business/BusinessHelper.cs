using Microsoft.AspNetCore.Http;
using Synergy.App.Business.Implementation;
using Synergy.App.Business.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.App.Data;

namespace Synergy.App.Business;

public static class BusinessHelper
{
    public static void RegisterDependency(IServiceCollection services)
    {
        services.AddScoped<IWorkflowBusiness, WorkflowBusiness>();
        services.AddScoped<IElsaBusiness, ElsaBusiness>();
        services.Add(new ServiceDescriptor(typeof(IContextBase<,>), typeof(ContextBase<,>), ServiceLifetime.Scoped));
        services.Add(new ServiceDescriptor(typeof(IBusinessBase<,>), typeof(BusinessBase<,>), ServiceLifetime.Scoped));
        services.Add(new ServiceDescriptor(typeof(IQueryBase<>), typeof(QueryBase<>), ServiceLifetime.Scoped));
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ICmsBusiness, CmsBusiness>();
		services.AddScoped<ITemplateBusiness, TemplateBusiness>();

	}
    public static object ConvertToDbValue(object s, bool isSystemColumn, DataColumnTypeEnum dataType)
        {

            if (s == null || Convert.ToString(s) == "")
            {
                return "null";
            }

            if (!isSystemColumn)
            {
                var udf = Convert.ToString(s);
                if (udf.Contains("\"storage\":") && udf.Contains("["))
                {
                    udf = udf.Replace("[", "").Replace("]", "");
                    var data = (JObject)JsonConvert.DeserializeObject(udf);
                    string fileId = data["id"].Value<string>();
                    return @$"'{fileId}'";


                }

                if (dataType == DataColumnTypeEnum.DateTime && s.ToString().Contains("Invalid date"))
                {
                    return "null";
                }

                if (dataType == DataColumnTypeEnum.DateTime)
                {
                    //return @$"'{((DateTime)s).ToDatabaseDateFormat()}'";
                    var date = Convert.ToDateTime(s);
                    var dbDate = date.ToDatabaseDateFormat();
                    return @$"'{dbDate}'";
                }

                if (dataType == DataColumnTypeEnum.Text)
                {
                    return @$"'{udf.Replace("'", "''")}'";
                }

                if (dataType == DataColumnTypeEnum.TextArray)
                {
                    udf = udf.Replace("\r", "");
                    udf = udf.Replace("\n", "");
                    return $"'{udf}'";
                }
                return $"'{s}'";

            }
            switch (dataType)
            {
                case DataColumnTypeEnum.Bool:
                    return @$"{(((bool)s) ? "true" : "false")}";
                case DataColumnTypeEnum.DateTime:
                    return @$"'{((DateTime)s).ToDatabaseDateFormat()}'";
                case DataColumnTypeEnum.TextArray:
                    var array = (string[])s;
                    var text = "";
                    foreach (var item in array)
                    {
                        text = @$"{text}""{item}"",";
                    }
                    text = $"'{{{text.Trim(',')}}}'";
                    return text;
                case DataColumnTypeEnum.Text:
                    var udf = Convert.ToString(s);
                    if (udf.Contains("\"storage\":") && udf.Contains("["))
                    {
                        udf = udf.Replace("[", "").Replace("]", "");
                        var data = (JObject)JsonConvert.DeserializeObject(udf);
                        string fileId = data["id"].Value<string>();
                        return @$"'{fileId}'";

                    }

                    return @$"'{s}'";

                default:
                    return s;
            }
        }
}