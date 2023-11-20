using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Identity;
using WiseSwitch.Data.Repository;
using WiseSwitch.Data.Repository.Interfaces;

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

builder.Services.AddTransient<InitDb>();

builder.Services.AddScoped<IIdentityManager, IdentityManager>();

builder.Services.AddScoped<IDataUnit, DataUnit>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

await InitDatabase(app);
async Task InitDatabase(IHost host)
{
    var scopedFactory = host.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory?.CreateScope();

    var initDb = scope?.ServiceProvider.GetService<InitDb>();

    await initDb.SeedAsync();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
