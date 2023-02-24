using Company.Crm.Domain.Repositories;
using Company.Crm.Entityframework.Repositories;
using Company.Crm.Entityframework.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Crm.Entityframework;

public static class ServiceRegistrations
{
    public static void AddEntityFrameworkRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            })
        );

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITitleRepository, TitleRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();

        // Usr Repos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserEmailRepository, UserEmailRepository>();
        services.AddScoped<IUserAddressRepository, UserAddressRepository>();
        services.AddScoped<IUserPhoneRepository, UserPhoneRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();

        // Lst Repos
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<IUserStatusRepository, UserStatusRepository>();
        services.AddScoped<IRequestStatusRepository, RequestStatusRepository>();
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
        services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
        services.AddScoped<IOfferStatusRepository, OfferStatusRepository>();
        services.AddScoped<IGenderRepository, GenderRepository>();

        var provider = services.BuildServiceProvider();
        DbSeeder.Seed(provider);
    }
}