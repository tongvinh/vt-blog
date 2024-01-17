using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VTBlog.Core.ConfigOptions;
using VTBlog.Core.Domain.Identity;
using VTBlog.Data;
using VTBlog.WebApp.Helpers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
// Add services to the container.
builder.Services.AddControllersWithViews();

//Custom setup
builder.Services.Configure<SystemConfig>(configuration.GetSection("SystemConfig"));

builder.Services.AddDbContext<VTBlogContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<VTBlogContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<AppUser>,CustomClaimsPrincipalFactory>();
//Start pipeline
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
