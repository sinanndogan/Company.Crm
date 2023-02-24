using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Entities.Usr;
using Microsoft.EntityFrameworkCore;
using Task = Company.Crm.Domain.Entities.Task;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Entityframework;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    // SqlServer ve bağlantı metnini appsettings içerisiniden alarak Program.cs içerisinde konfigre ediyoruz.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<UserEmail> UserEmails { get; set; }
    public DbSet<UserPhone> UserPhones { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Sale> Sales { get; set; }

    #region USR Tables

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    #endregion

    #region LST Tables

    public DbSet<Title> Titles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }
    public DbSet<StatusType> StatusTypes { get; set; }
    public DbSet<OfferStatus> OfferStatuses { get; set; }
    public DbSet<TaskStatus> TaskStatuses { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<RequestStatus> RequestStatuses { get; set; }
    
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Program.cs içerisinde DbContext tanımı aşağıdaki gibi olursa;
        // builder.Services.AddDbContext<AppDbContext>()
        // Buradaki konfigrasyon çalışacaktır.
        if (!builder.IsConfigured)
        {
            var connectionString = "Server=(localdb)\\MsSqlLocalDb; Database=SF2_CRM_Dev; Trusted_Connection=True;";
            builder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Fluent API ile entity configuration

        //var customer = modelBuilder.Entity<Customer>();
        //customer.Property(c => c.UserId).IsRequired();
        //customer.Property(c => c.BirthDate).IsRequired();
        //customer.Property(c => c.CompanyName)
        //    .IsRequired()
        //    .HasMaxLength(500);

        #endregion

        // Tek tek configuration tanımlama
        //modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        // Bir assembly içerisindeki tüm configuration sınıflarını otomatik tanımlama
        //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Singular Table Names
        //https://www.meziantou.net/entity-framework-core-naming-convention.htm
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType != null && !entityType.HasSharedClrType)
            {
                modelBuilder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name, entityType.GetSchema());
            }
        }

        // Seeders (HasData)
        //CustomerSeeder.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}