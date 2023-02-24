using Bogus;
using Company.Crm.Domain.Entities;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Domain.Entities.Usr;
using Company.Crm.Domain.Enums;
using Company.Framework.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;
using UserStatus = Company.Crm.Domain.Entities.Lst.UserStatus;

namespace Company.Crm.Entityframework.Seeders;

public static class DbSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        var options = serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>();
        var context = new AppDbContext(options);

        SeedLstTables(context);
        SeedRoles(context);
        SeedAdminUser(context);
        context.SaveChanges();

        SeedCustomers(context);
        context.SaveChanges();

        SeedUserDetails(context);
        SeedNotifications(context);
        SeedSales(context);

        context.SaveChanges();
    }

    private static void SeedCustomers(AppDbContext context)
    {
        if (!context.Customers.Any())
        {
            var companySet = new Bogus.DataSets.Company("tr");
            var nameSet = new Bogus.DataSets.Name("tr");

            var userFaker = new Faker<User>()
                .RuleFor(o => o.Name, f => nameSet.FirstName())
                .RuleFor(o => o.Surname, f => nameSet.LastName())
                .RuleFor(o => o.Username, (f, o) => o.Name.ToLower() + "." + o.Surname.ToLower())
                .RuleFor(o => o.Email, (f, o) => o.Name.ToLower() + "." + o.Surname.ToLower() + "@company.com")
                .RuleFor(o => o.Password, SecurityHelper.HashCreate("123qwe"))
                .RuleFor(o => o.UserStatusId, f => f.Random.Int(1, 2));

            var userRole = context.Roles.FirstOrDefault(e => e.Id == 2);
            if (userRole != null)
                userFaker.RuleFor(o => o.Roles, new List<Role> { userRole });

            var customerFaker = new Faker<Customer>()
                .RuleFor(o => o.CompanyName, f => companySet.CompanyName())
                .RuleFor(o => o.IdentityNumber, f => f.Random.Long(11111111111, 99999999999).ToString())
                .RuleFor(o => o.BirthDate, f =>
                    f.Date.Between(new DateTime(1960, 1, 1), DateTime.Now))
                .RuleFor(o => o.StatusTypeId, f => f.Random.Int(1, 2))
                .RuleFor(o => o.GenderId, f => f.Random.Int(1, 2))
                .RuleFor(o => o.TitleId, f => f.Random.Int(1, 100))
                .RuleFor(o => o.CustomerType, f => (CustomerTypeEnum)f.Random.Int(1, 2))
                .RuleFor(o => o.UserFk, () => userFaker)
                .RuleFor(o => o.UserId, (f, o) => o.UserFk?.Id);

            var customers = Enumerable.Range(1, 200)
                .Select(e => customerFaker.Generate())
                .ToList();

            context.Customers.AddRange(customers);
        }
    }

    private static void SeedRoles(AppDbContext context)
    {
        if (!context.Roles.Any())
            context.Roles.AddRange(new List<Role>
            {
                new() { Name = "Administrator" },
                new() { Name = "User" },
                new() { Name = "SalesManager" },
                new() { Name = "AccountManager" }
            });
    }

    private static void SeedAdminUser(AppDbContext context)
    {
        if (!context.Users.Any(e => e.Username == "admin"))
        {
            var user = new User
            {
                Username = "admin",
                Email = "admin@site.com",
                Password = SecurityHelper.HashCreate("admin"),
                Name = "admin",
                Surname = "admin",
                UserStatusId = (int)UserStatusEnum.Active,
                Roles = new List<Role>()
            };

            var userRole = context.Roles.FirstOrDefault(e => e.Id == 1);
            if (userRole != null)
            {
                user.Roles.Add(userRole);
            }

            context.Users.Add(user);
        }
    }

    private static void SeedUserDetails(AppDbContext context)
    {
        var addressSet = new Bogus.DataSets.Address("tr");
        var phoneSet = new Bogus.DataSets.PhoneNumbers("tr");

        var users = context.Users.ToList();

        foreach (var user in users)
        {
            if (!context.UserAddresses.Any())
            {
                var addressFaker = new Faker<UserAddress>()
                    .RuleFor(e => e.UserId, user.Id)
                    .RuleFor(e => e.Description, c => addressSet.FullAddress())
                    .RuleFor(e => e.AddressType, c => (AddressTypeEnum)c.Random.Int(1, 2));

                var address = addressFaker.Generate();

                context.UserAddresses.Add(address);
            }

            if (!context.UserPhones.Any())
            {
                var phoneFaker = new Faker<UserPhone>()
                    .RuleFor(e => e.UserId, user.Id)
                    .RuleFor(e => e.PhoneNumber, c => phoneSet.PhoneNumber())
                    .RuleFor(e => e.PhoneType, c => (PhoneTypeEnum)c.Random.Int(1, 4));

                var phone = phoneFaker.Generate();

                context.UserPhones.Add(phone);
            }

            if (!context.UserEmails.Any())
            {
                var emailFaker = new Faker<UserEmail>()
                    .RuleFor(e => e.UserId, user.Id)
                    .RuleFor(e => e.EmailAddress, user.Email)
                    .RuleFor(e => e.EmailType, c => (EmailTypeEnum)c.Random.Int(1, 4));

                var email = emailFaker.Generate();

                context.UserEmails.Add(email);
            }
        }
    }

    private static void SeedNotifications(AppDbContext context)
    {
        if (!context.Notifications.Any())
        {
            var randomSet = new Bogus.DataSets.Lorem("tr");
            var notificationFaker = new Faker<Notification>();

            notificationFaker
                .RuleFor(x => x.IsRead, f => f.Random.Bool())
                .RuleFor(x => x.Title, f => randomSet.Slug())
                .RuleFor(x => x.Description, f => randomSet.Word());

            var notifications = Enumerable.Range(1, 10)
                .Select(e => notificationFaker.Generate())
                .ToList();

            foreach (var notification in notifications)
            {
                var random = new Random().Next(1, 1000);
                notification.CreatedAt = DateTime.Now;
                notification.CreatedBy = random;
                notification.UserId = random;
            }

            context.Notifications.AddRange(notifications);
        }
    }

    private static void SeedSales(AppDbContext context)
    {
        if (!context.Sales.Any())
        {
            var randomSet = new Bogus.DataSets.Lorem("tr");
            var saleFaker = new Faker<Sale>();

            saleFaker
                .RuleFor(x => x.EmployeeUserId, f => f.Random.Int(1, 100))
                .RuleFor(x => x.RequestId, f => f.Random.Int(1, 100))
                .RuleFor(x => x.SaleAmount, f => f.Random.Int(1, 10000))
                .RuleFor(x => x.Description, f => randomSet.Word())
                .RuleFor(x => x.SaleDate, f => f.Date.Past());

            var sales = Enumerable.Range(1, 10)
                .Select(e => saleFaker.Generate())
                .ToList();

            context.Sales.AddRange(sales);
        }
    }

    private static void SeedLstTables(AppDbContext context)
    {
        if (!context.Departments.Any())
        {
            context.Departments.AddRange(new List<Department>
            {
                new() { Name = "Administration" },
                new() { Name = "Sale" },
                new() { Name = "Marketing" },
                new() { Name = "Accounting" },
                new() { Name = "Technical" }
            });
        }

        if (!context.Genders.Any())
        {
            context.Genders.AddRange(new List<Gender>
            {
                new() { Name = "Male" },
                new() { Name = "Female" }
            });
        }

        if (!context.OfferStatuses.Any())
        {
            context.OfferStatuses.AddRange(new List<OfferStatus>
            {
                new() { Name = "Active" },
                new() { Name = "In Progress" },
                new() { Name = "Closed" }
            });
        }

        if (!context.Regions.Any())
        {
            context.Regions.AddRange(new List<Region>
            {
                new() { Name = "Istanbul-Avrupa" },
                new() { Name = "Istanbul-Anadolu" },
                new() { Name = "Ankara" }
            });
        }

        if (!context.StatusTypes.Any())
        {
            context.StatusTypes.AddRange(new List<StatusType>
            {
                new() { Name = "Active" },
                new() { Name = "Archive" },
                new() { Name = "Black Listed" }
            });
        }

        if (!context.UserStatuses.Any())
        {
            context.UserStatuses.AddRange(new List<UserStatus>
            {
                new() { Name = "Active" },
                new() { Name = "Not Activated" },
                new() { Name = "Passive" }
            });
        }

        if (!context.Titles.Any())
        {
            var nameSet = new Bogus.DataSets.Name("tr");

            var titleFaker = new Faker<Title>()
                .RuleFor(o => o.Name, f => nameSet.JobTitle());

            var titles = Enumerable.Range(1, 100)
                .Select(e => titleFaker.Generate())
                .ToList();

            context.Titles.AddRange(titles);
        }

        if (!context.TaskStatuses.Any())
        {
            context.TaskStatuses.AddRange(new List<TaskStatus>
            {
                new() { Name = "Open" },
                new() { Name = "In Progress" },
                new() { Name = "Resolved" },
                new() { Name = "Closed" }
            });
        }

        if (!context.DocumentTypes.Any())
        {
            context.DocumentTypes.AddRange(new List<DocumentType>
            {
                new() { Name = "Word" },
                new() { Name = "PDF" },
                new() { Name = "Video" },
                new() { Name = "Audio" }
            });
        }

        if (!context.OfferStatuses.Any())
        {
            context.OfferStatuses.AddRange(new List<OfferStatus>
            {
                new() { Name = "Open" },
                new() { Name = "In Progress" },
                new() { Name = "Resolved" },
                new() { Name = "Closed" }
            });
        }

        if (!context.RequestStatuses.Any())
        {
            context.RequestStatuses.AddRange(new List<RequestStatus>
            {
                new() { Name = "Open" },
                new() { Name = "In Progress" },
                new() { Name = "Resolved" },
                new() { Name = "Closed" }
            });
        }
    }
}