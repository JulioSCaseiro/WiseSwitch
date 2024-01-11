using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Identity;
using WiseSwitch.Data.Repository;
using WiseSwitch.Data.Repository.Interfaces;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection here

builder.Services.AddDbContext<DataContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(cfg =>
{
    cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ¿‡¡·¬‚ƒ‰…È»ËÕÌÃÏŒÓœÔ”Û“Ú‘Ù÷ˆ⁄˙Ÿ˘€˚‹¸—Ò«Á›˝ -_.@";

    cfg.Password.RequireDigit = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequiredLength = 1;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<IIdentityManager, IdentityManager>();

builder.Services.AddScoped<IDataUnit, DataUnit>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<DataService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
