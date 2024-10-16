using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Tests
{
	public class LocalFileServiceTests
    {
        private string jsonEmployeeFile = Path.Combine("./TestData/Employee.json");
		private string jsonShiftFile = Path.Combine("./TestData/Shift.json");
		private string emptyFile = Path.Combine("./TestData/Empty.json");
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
            var employee = await _sut.LoadFileAsList(jsonEmployeeFile) as List<Employee>;

            // Act
            var result = employee.Where(e=>e.Id == id).Select(e=>e.Name).FirstOrDefault();

            // Assert
            result.Should().Be(expectedName);
            result.Should().NotBeNull();
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

		[Fact]
		public async void LoadFileAsList_WhenEmptyFile_ReturnJsonException()
		{
			// Arrange

			// Act
			Func<Task> act = async () => await _sut.LoadFileAsList(emptyFile);

			// Assert
			await act.Should().ThrowAsync<JsonException>();
		}

		[Fact]
		public async void LoadFileAsList_WhenNotCorrectFile_ReturnInvalidDataException()
		{
			// Arrange

			// Act
			Func<Task> act = async () => await _sut.LoadFileAsList(jsonShiftFile);

			// Assert
			await act.Should().ThrowAsync<InvalidDataException>();
		}
	}
}