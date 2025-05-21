using System.Text;
using AspNetCore.Authentication.ApiKey;
using AutoMapper;
using AutoMapper.Data;
using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;
using Elsa.Identity.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Synergy.App.Business;
using Synergy.App.Business.Implementation;
using Synergy.App.Data;
using Synergy.App.Data.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("PostgreConnection") ??
                       throw new InvalidOperationException("Connection string 'PostgreConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

services.AddIdentityCore<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

services.ConfigureOptions<ConfigureJwtBearerOptions>();
services.ConfigureOptions<ValidateIdentityTokenOptions>();
services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    })
    .AddApiKeyInAuthorizationHeader<DefaultApiKeyProvider>()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? "")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    })
    .AddPolicyScheme("ElsaScheme", "ElsaScheme", options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            return context.Request.Headers.Authorization.Any(x => x!.Contains(ApiKeyDefaults.AuthenticationScheme))
                ? ApiKeyDefaults.AuthenticationScheme
                : JwtBearerDefaults.AuthenticationScheme;
        };
    }).AddCookie(IdentityConstants.ExternalScheme,
        options => { options.ForwardSignOut = IdentityConstants.ExternalScheme; }).AddCookie(
        IdentityConstants.ApplicationScheme,
        options => { options.ForwardSignIn = IdentityConstants.ApplicationScheme; });

services.AddElsa(elsa =>
{
    // Configure Management layer to use EF Core.
    elsa.UseWorkflowManagement(management =>
            management.UseEntityFrameworkCore(ef => ef.UsePostgreSql(connectionString)))
        .UseWorkflowRuntime(runtime =>
            runtime.UseEntityFrameworkCore(ef => ef.UsePostgreSql(connectionString)))
        .UseIdentity(identity =>
        {
            identity.TokenOptions =
                options => options.SigningKey =
                    configuration["JWT:Secret"] ?? ""; // This key needs to be at least 256 bits long.
        })
        .UseWorkflowsApi()
        .UseJavaScript()
        .UseCSharp()
        .UseHttp(http =>
        {
            http.ConfigureHttpOptions = options => configuration.GetSection("Http").Bind(options);
            http.UseCache();
        })
        .UseScheduling().AddActivitiesFrom<Program>()
        .AddWorkflowsFrom<Program>();
});

// Configure CORS to allow designer app hosted on a different origin to invoke the APIs.
services.AddCors(cors => cors
    .AddDefaultPolicy(policy => policy
        .AllowAnyOrigin() // For demo purposes only. Use a specific origin instead.
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders(
            "x-elsa-workflow-instance-id"))); // Required for Elsa Studio in order to support running workflows from the designer. Alternatively, you can use the `*` wildcard to expose all headers.

// Add Health Checks.
services.AddHealthChecks();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddDataReaderMapping(false);
    var profile = new MappingProfile();
    profile.AddDataRecordMember();
    cfg.AddProfile(profile);
});
var mapper = mapperConfig.CreateMapper();
services.AddSingleton(mapper);
Elsa.EndpointSecurityOptions.DisableSecurity();
services.AddControllersWithViews();
services.AddRazorPages();

BusinessHelper.RegisterDependency(services);
// Build the web application.
var app = builder.Build();

// Configure web application's middleware pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseWorkflowsApi(); // Use Elsa API endpoints.
app.UseWorkflows(); // Use Elsa middleware to handle HTTP requests mapped to HTTP Endpoint activities.


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();