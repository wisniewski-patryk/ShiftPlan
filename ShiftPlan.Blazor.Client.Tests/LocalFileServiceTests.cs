using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Tests
{
	public class LocalFileServiceTests
    {
	
		private LocalFileService<Employee> _sut { get; }

        public LocalFileServiceTests()
        {
            _sut = new LocalFileService<Employee>();
        }

        [Theory]
        [InlineData(1, "Test1")]
        [InlineData(2, "Test2")]
        public async void LoadFileAsList_WhenEmploeeNamesAreProperly_ReturnTrue(int id, string expectedName)
        {
			// Arrange
			string jsonEmployeeFile = Path.Combine("./TestData/Employee.json");
			var employee = await _sut.LoadFileAsList(jsonEmployeeFile) as List<Employee>;

            // Act
            var result = employee.Where(e=>e.Id == id).Select(e=>e.Name).FirstOrDefault();

            // Assert
            result.Should().Be(expectedName);
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("Test1")]
        public async void LoadFileAsSingle_WhenEmploeeNamesAreProperly_ReturnTrue(string expectedName)
        {
            // Arrange
            string jsonFile = Path.Combine("./TestData/Shift.json");
            var service = new LocalFileService<Shift>();
            var shift = await service.LoadFileAsSingle(jsonFile);

            // Act
            var result = shift.Employee.Name;

            // Assert
            result.Should().Be(expectedName);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void LoadFileAsList_WhenFileNotExist_ReturnFileNotFoundException()
        {
			// Arrange
			string notExistFile = Path.Combine("./TestData/notExist.json");

			// Act
			Func<Task> act = async () => await _sut.LoadFileAsList(notExistFile);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }

		[Fact]
		public async void LoadFileAsList_WhenEmptyFile_ReturnJsonException()
		{
			// Arrange
			string emptyFile = Path.Combine("./TestData/Empty.json");

			// Act
			Func<Task> act = async () => await _sut.LoadFileAsList(emptyFile);

			// Assert
			await act.Should().ThrowAsync<JsonException>();
		}

		[Fact]
		public async void LoadFileAsList_WhenNotCorrectFile_ReturnInvalidDataException()
		{
			// Arrange
			string jsonShiftFile = Path.Combine("./TestData/Shift.json");

			// Act
			Func<Task> act = async () => await _sut.LoadFileAsList(jsonShiftFile);

			// Assert
			await act.Should().ThrowAsync<InvalidDataException>();
		}
        
        [Fact]
        public async void SaveFileAsList_WhenFileWasCreated_ReturnTrue()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/create.json");

            // Act
            var result = await _sut.SaveFileAsList(new List<Employee>() { new Employee("Test1",1)}, createFile);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void SaveFileAsSingle_WhenFileWasCreated_ReturnTrue()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/create.json");

            // Act
            var result = await _sut.SaveFileAsSingle(new Employee("Test1", 1), createFile);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void SaveFileAsList_WhenFileWasNotCreated_ReturnThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsList(new List<Employee>(), createFile);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async void SaveFileAsSingle_WhenFileWasNotCreated_ReturnThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsSingle(new Employee("",1), createFile);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }


    }
}