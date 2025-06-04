using AutoMapper;
using AutoMapper.Data;
using Elsa.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Business;
using Synergy.App.Data;
using Synergy.App.Data.Model;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("PostgreConnection") ??
                       throw new InvalidOperationException("Connection string 'PostgreConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);
services.AddDefaultIdentity<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

services.ConfigureOptions<ConfigureJwtBearerOptions>();
services.ConfigureOptions<ValidateIdentityTokenOptions>();

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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();