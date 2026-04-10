using FinanceTracker.Core;
using FinanceTracker.Core.Models;
using FinanceTracker.Data.DBContext;
using FinanceTracker.Middlewares;
using MasterData.Data.DBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Security.Core;
using Security.Data.DBContext;
using System.Text;

public partial class Program
{
    private static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

        try
        {
            InitializeApplication(logger, args);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to start application");
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    private static void InitializeApplication(Logger logger, string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Logging.ClearProviders();
        builder.Host.UseNLog();
        builder.Services.AddMemoryCache();
        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        });

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<FinanceTrackerContext>(options => SetDbOptions(options, connection, "dbo"));
        builder.Services.AddDbContext<SecurityContext>(options => SetDbOptions(options, connection, "sc"));
        builder.Services.AddDbContext<MasterDataContext>(options => SetDbOptions(options, connection, "md"));

        var jwtConfig = builder.Configuration.GetSection("JWT").Get<JWTModel>() ?? new JWTModel();

        builder.Services.Configure<ConfigModel>(builder.Configuration.GetSection("Config"));
        builder.Services.Configure<JWTModel>(builder.Configuration.GetSection("JWT"));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.SecretKey)),
                    ValidateIssuerSigningKey = true,
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var token = context.Request.Headers["Authorization"];
                //        // Здесь удобно смотреть, пришел ли токен вообще
                //        return Task.CompletedTask;
                //    },
                //    OnAuthenticationFailed = context =>
                //    {
                //        // А здесь можно поймать причину, почему токен отклонен (истек, кривой ключ и т.д.)
                //        Console.WriteLine("Ошибка: " + context.Exception.Message);
                //        return Task.CompletedTask;
                //    }
                //};
            });

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        SecurityServiceHelper.InitializeServices(builder.Services);

        FinancesServiceHelper.InitializeServices(builder.Services);

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
    }

    private static void SetDbOptions(DbContextOptionsBuilder options, string? connection, string scheme)
    {
        options.UseLazyLoadingProxies();
        options.UseSqlServer(connection, x =>
        {
            x.MigrationsHistoryTable("__IdentityMigrationHistory", scheme);
            x.UseHierarchyId();
            x.EnableRetryOnFailure();
        });
    }
}