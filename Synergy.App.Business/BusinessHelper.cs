using Microsoft.AspNetCore.Http;
using Synergy.App.Business.Implementation;
using Synergy.App.Business.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Synergy.App.Business;

public static class BusinessHelper
{
    public static void RegisterDependency(IServiceCollection services)
    {
        services.AddScoped<INotificationBusiness, NotificationBusiness>();
        services.AddScoped<IWorkflowBusiness, WorkflowBusiness>();
        services.AddScoped<ILeaveBusiness,LeaveBusiness>();
        services.Add(new ServiceDescriptor(typeof(IContextBase<,>), typeof(ContextBase<,>), ServiceLifetime.Scoped));
        services.Add(new ServiceDescriptor(typeof(IBaseBusiness<,>), typeof(BaseBusiness<,>), ServiceLifetime.Scoped));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserContext, UserContext>();

    }
}