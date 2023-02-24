using Company.Crm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Crm.Entityframework.Seeders;

public class CustomerSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<Customer>().HasData(new List<Customer>
        {
            new() { Id = 1, UserId = 1, RegionId = 1, IdentityNumber = "1234", GenderId = 1, StatusTypeId = 1, BirthDate = new DateTime(1990, 1, 1), CompanyName = "Test 1", TitleId = 1 },
            new() { Id = 2, UserId = 2, RegionId = 1, IdentityNumber = "4567", GenderId = 1, StatusTypeId = 1, BirthDate = new DateTime(1990, 1, 1), CompanyName = "Test 2", TitleId = 1 }
        });
    }
}