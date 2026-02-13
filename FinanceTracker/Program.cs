using FinanceTracker.Data.Context;
using MasterData.Data.Context;
using Microsoft.EntityFrameworkCore;
using Security.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<FinanceTrackerContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connection, x => x.MigrationsHistoryTable("__IdentityMigrationHistory", "dbo"));
});

builder.Services.AddDbContext<SecurityContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connection, x => x.MigrationsHistoryTable("__IdentityMigrationHistory", "sc"));
});

builder.Services.AddDbContext<MasterDataContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(connection, x => x.MigrationsHistoryTable("__IdentityMigrationHistory", "md"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors((options) =>
{
    options.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
});

app.UseAuthorization();
app.UseAuthentication();

//app.UseMiddleware<TestMiddleware>();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action=Index}/{id?}");

app.Run();
