using Company.Crm.MongoDb;
using Company.Crm.Redis;
using Company.Crm.Web.Api.Jobs;
using FluentScheduler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Company.Crm.Web.Api;

public static class ServiceRegistrations
{
    public static void AddApiRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditLogRepository>();
        services.AddSingleton<ExceptionLogRepository>();

        if (Environment.GetEnvironmentVariable("IS_REDIS_CACHE_ACTIVE") == "1")
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("MyRedisConStr");
                options.InstanceName = "Crm";
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }

        services.AddSingleton<RedisService>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: "CrmCors", policy =>
            {
                policy
                    .WithOrigins(configuration["App:ClientUrls"].Split(','))
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials();

                // Tüm adreslere izin verme
                //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Yetkilendirmeyi yapan
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Auth:Jwt:Issuer"],

                    // Yetkilendirmeyi kullanan client
                    ValidateAudience = true,
                    ValidAudience = configuration["Auth:Jwt:Audience"],

                    // Token zaman doğrulaması
                    ValidateLifetime = true,
                    //ClockSkew = TimeSpan.FromMinutes(1),
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Jwt:SecurityKey"]))
                };
            });


        // Hosted Services
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services
        //services.AddHostedService<TimedHostedService>();
        //services.AddHostedService<WeeklyEmailHostedService>();

        #region Fluent Scheduler

        // https://fluentscheduler.github.io/creating-schedules/

        var sp = services.BuildServiceProvider();
        var logger = sp.GetRequiredService<ILogger<TimedHostedService>>();
        var job = new WeeklyEmailJob(logger, sp);

        var registry = new Registry();
        registry.Schedule(job).ToRunNow().AndEvery(20).Seconds();

        //JobManager.JobException += info => Console.WriteLine("An error just happened with a scheduled job: " + info.Exception);
        //JobManager.Initialize(registry);

        //JobManager.Initialize();
        //JobManager.AddJob(
        //    () => Console.WriteLine("5 minutes just passed."),
        //    s => s.ToRunNow().AndEvery(1).Weeks().On(DayOfWeek.Sunday).At(12, 0)
        //);

        #endregion
    }
}