using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services;
using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Moq;

namespace Test.Unit.Application
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock = new();

        public CustomerServiceTests()
        {
            var testData = new ServiceResponse<List<CustomerDto>>()
            {
                IsSuccess = true,
                Data = new List<CustomerDto>() {
                    new() { Id = 1, CompanyName = "Test Company 1" },
                    new() { Id = 2, CompanyName = "Test Company 2" },
                    new() { Id = 3, CompanyName = "Test Company 3" },
                }
            };
            _customerServiceMock.Setup(service => service.GetAll()).Returns(testData);
        }

        [Fact]
        public void Simple_Test()
        {
            Assert.Equal(4, Math.Pow(2, 2));
        }

        [Fact]
        public void Customer_Id_Should_Null_When_Id_LowerThanZero()
        {
            // Arrange
            var customerId = 0;

            // Act
            var service = new Customer2Service();
            var result = service.GetCustomer(customerId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Customer_Id_Should_CustomerDto_When_Id_UpperThanZero()
        {
            // Arrange
            var customerId = 5;

            // Act
            var service = new Customer2Service();
            var result = service.GetCustomer(customerId);

            // Assert
            Assert.IsType<CustomerDto>(result);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1), InlineData(2), InlineData(3)]
        public void Customer_Id_Should_CustomerDto_When_MultipleId_UpperThanZero(int customerId)
        {
            // Arrange
            //var customerId = ?;

            // Act
            var service = new Customer2Service();
            var result = service.GetCustomer(customerId);

            // Assert
            Assert.IsType<CustomerDto>(result);
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public void Customer_Should_Be_List_When_GetAll()
        {
            // Arrange

            // Act
            var result = _customerServiceMock.Object.GetAll();

            // Assert
            Assert.True(result.Data != null && result.Data.Count > 0);
        }
    }
}