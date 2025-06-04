using AutoMapper;
using AutoMapper.Data;
using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;
using Elsa.Workflows;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business;
using Synergy.App.Data;
using Synergy.App.Data.Model;
using Synergy.Elsa.Server;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var elsaConfiguration = configuration.GetSection("Elsa");
var connectionString = configuration.GetConnectionString("PostgreConnection") ??
                       throw new InvalidOperationException("Connection string 'PostgreConnection' not found.");

builder.WebHost.UseStaticWebAssets();
services.AddHttpContextAccessor();
services.AddControllers();
services.AddSwaggerDocument();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);
services.AddScoped<IPropertyUIHandler, CustomDropDownOptionsProvider>();

// BusinessHelper.RegisterDependency(services);
services
    .AddElsa(elsa => elsa
        .UseIdentity(identity =>
        {
            identity.TokenOptions = options => options.SigningKey = "large-signing-key-for-signing-JWT-tokens";
            identity.UseAdminUserProvider();
        })
        .UseDefaultAuthentication()
        .UseWorkflowManagement(management =>
            management.UseEntityFrameworkCore(ef => ef.UsePostgreSql(connectionString)))
        .UseWorkflowRuntime(runtime =>
            runtime.UseEntityFrameworkCore(ef => ef.UsePostgreSql(connectionString)))

        .UseScheduling()
        .UseJavaScript()
        .UseLiquid()
        .UseCSharp()
        .UseHttp(http => http.ConfigureHttpOptions = options => elsaConfiguration.GetSection("Http").Bind(options))
        .UseWorkflowsApi()
        .AddActivitiesFrom<Program>()
        .AddWorkflowsFrom<Program>()
    );

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddDataReaderMapping(false);
    var profile = new MappingProfile();
    profile.AddDataRecordMember();
    cfg.AddProfile(profile);
});
var mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);
services.AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*")));
services.AddRazorPages(options => options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseRouting();
app.UseCors();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseWorkflowsApi();
app.UseWorkflows();
app.UseOpenApi();
app.UseSwaggerUI();
app.MapControllers();
app.MapFallbackToPage("/_Host");
app.Run();