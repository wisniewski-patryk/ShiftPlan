using FluentAssertions;
using Moq;
using ShiftPlan.Blazor.Commons.Clients;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Commons.Tests;

public class EmployeesServiceTests
{
    private readonly EmployeesService _sut;
    private readonly Mock<IEmployeesClient> _mockEmployeesClient = new Mock<IEmployeesClient>();

    public EmployeesServiceTests()
    {
        _sut = new EmployeesService(_mockEmployeesClient.Object);
    }

    [Fact]
    public async Task GetAll_AllNamesShouldNotHaveStringEmpty_ReturnTrue()
    {
        // Arrange
        _mockEmployeesClient.Setup(x => x.GetAll()).ReturnsAsync(GetEmployeesClient());

        // Act
        var users = await _sut.GetAll();
        var result = users.All(x => x.Name != string.Empty);

        // Assert
        result.Should().BeTrue();
    }

    private IEnumerable<Employee> GetEmployeesClient()
    {
        return new List<Employee>() {
                new Employee("Test1",1),
                new Employee("Test2",2),
            };
    }
}
