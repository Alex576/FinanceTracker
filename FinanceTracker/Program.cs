using FinanceTracker.Core.Services;
using FinanceTracker.Core.Services.Interfaces;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Middlewares;
using FinanceTracker.Models;
using MasterData.Data.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Security.Core;
using Security.Data.DBContext;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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

var jwtConfig = builder.Configuration.GetSection("JWT").Get<JWTModel>() ?? new JWTModel();

builder.Services.Configure<ConfigModel>(builder.Configuration.GetSection("Config"));
builder.Services.Configure<JWTModel>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = jwtConfig.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = jwtConfig.Audience,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecretKey)),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"];
                // Здесь удобно смотреть, пришел ли токен вообще
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                // А здесь можно поймать причину, почему токен отклонен (истек, кривой ключ и т.д.)
                Console.WriteLine("Ошибка: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

SecurityServiceHelper.InitializeServices(builder.Services);

builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<FinanceTracker.Core.Services.Interfaces.IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<ILoginService, LoginService>();

var app = builder.Build();

app.UseExceptionHandler();
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
    options.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserProtectionMiddleware>();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller}/{action=Index}/{id?}");

app.Run();
