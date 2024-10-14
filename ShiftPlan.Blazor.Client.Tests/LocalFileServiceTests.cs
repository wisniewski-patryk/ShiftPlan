using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Client.Tests
{
    public class LocalFileServiceTests
    {
        private string jsonFile = Path.Combine("./TestData/Employee.json");
        private ILoadSaveService<Employee> _sut { get; }

        public LocalFileServiceTests()
        {
            _sut = new LocalFileService<Employee>();
        }

        [Theory]
        [InlineData(1, "Test1")]
        public async void LoadFileAsList_WhenEmploeeNameIsProperly_ReturnTrue(int id, string expectedName)
        {
            // Arrange
            var employee = await _sut.LoadFileAsList(jsonFile) as List<Employee>;

            // Act
            var result = employee.Where(e=>e.Id == id).Select(e=>e.Name).FirstOrDefault();

            // Assert
            result.Should().Be(expectedName);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void LoadFileAsList_WhenNotBeEmpty_ReturnTrue()
        {
            // Arrange

            // Act
            var result = await _sut.LoadFileAsList(jsonFile);

            // Assert
            result.Should().NotBeEmpty();
        }


        [Fact]
        public async void LoadFileAsList_IFileNotExist_ReturnFileNotFoundException()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _sut.LoadFileAsList(@"");

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }
    }
}