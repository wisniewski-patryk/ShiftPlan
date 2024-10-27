using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Tests
{
    public class LocalFileServiceTests
    {
	
		private LocalFileService _sut { get; }

        public LocalFileServiceTests()
        {
            _sut = new LocalFileService();
        }

		[Theory]
		[InlineData(1, "Mario")]
		public async void LoadLocalFileAsList_WhenEmploeeNameAreProperly_ReturnTrue(int id, string expectedName)
		{
			// Arrange
			string jsonEmployeeShiftsFile = Path.Combine("./TestData/employeeshifts.json");
			var employeeshifts = await _sut.LoadLocalFileAsSingle(jsonEmployeeShiftsFile);

			// Act
			var result = employeeshifts.Employees.Where(e => e.Id == id).Select(e => e.Name).FirstOrDefault();

			// Assert
			result.Should().Be(expectedName);
			result.Should().NotBeNull();
		}

		[Theory]
        [InlineData(1, "Test1")]
        [InlineData(2, "Test2")]
        public async void LoadFileAsList_WhenEmploeeNamesAreProperly_ReturnTrue(int id, string expectedName)
        {
			// Arrange
			string jsonEmployeeFile = Path.Combine("./TestData/Shifts.json");
			var shifts = await _sut.LoadFileAsList(jsonEmployeeFile);

            // Act
            var result = shifts.Where(e=>e.Id == id).Select(e=>e.Employee.Name).FirstOrDefault();

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
            var service = new LocalFileService();
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
        public async void SaveFileAsList_WhenFileWasCreated_NotThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/create.json");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsList(TestShifts(), createFile);

            // Assert
            await act.Should().NotThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async void SaveFileAsSingle_WhenFileWasCreated_NotThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/create.json");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsSingle(TestShift(), createFile);

            // Assert
            await act.Should().NotThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async void SaveFileAsList_WhenFileWasNotCreated_ReturnThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsList(new List<Shift>(), createFile);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async void SaveFileAsSingle_WhenFileWasNotCreated_ReturnThrowFileNotFoundException()
        {
            // Arrange
            string createFile = Path.Combine("./TestData/");

            // Act
            Func<Task> act = async () => await _sut.SaveFileAsSingle(TestShift(), createFile);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }

		[Fact]
		public async void SaveLocalFileAsSingle_WhenFileWasCorrectCreated_NotThrowFileNotFoundException()
		{
			// Arrange
			string createFile = Path.Combine("./TestData/employeeshifts.json");

			// Act
			Func<Task> act = async () => await _sut.SaveLocalFileAsSingle(TestEmployeeShifts(), createFile);

			// Assert
			await act.Should().NotThrowAsync<FileNotFoundException>();
		}

		private LocalDataObject TestEmployeeShifts()
		{
			return new LocalDataObject(
					new List<Employee> { new Employee("Mario", 1) },
					new List<Shift> { new Shift(new Employee("Mario", 1), DateOnly.FromDateTime(DateTime.Now), new TimeOnly(6, 00), new TimeOnly(14, 00), 1) }
				);
		}

		private Shift TestShift()
        {
            return new Shift(
                    new Employee("Mario", 1),
                    DateOnly.FromDateTime(DateTime.Now),
                    new TimeOnly(6, 00),
                    new TimeOnly(14, 00),
                    1
            );
        }

        private List<Shift> TestShifts()
        {
            return new List<Shift>() { new Shift(
                    new Employee("Mario", 1),
                    DateOnly.FromDateTime(DateTime.Now),
                    new TimeOnly(6, 00),
                    new TimeOnly(14, 00),
                    1
            )};
        }
    }
}