using Company.Crm.Application.Dtos;

namespace Company.Crm.Application.Services;

public interface ICustomer2Service
{
    CustomerDto? GetCustomer(int id);
}

public class Customer2Service
{
    public CustomerDto? GetCustomer(int id)
    {
        if (id <= 0)
            return null;
        else
            return new CustomerDto() { Id = id, CompanyName = "Test Company " + id };
    }
}
