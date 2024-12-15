using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using System.Text.Json;

namespace ShiftPlan.Blazor.Client.Tests;
// TODO: Move to fortnend test project
public class LocalFileServiceTests
{

	private LocalFileService _sut { get; }

	public LocalFileServiceTests()
	{
		_sut = new LocalFileService();
	}

	[Theory]
	[InlineData(1, "Test1")]
	public async void LoadLocalFile_WhenEmploeeNameAreProperly_ReturnTrue(int id, string expectedName)
	{
		// Arrange
		string jsonEmployeeShiftsFile = Path.Combine("./TestData/employeeshifts.json");
		var employeeshifts = await _sut.LoadLocalFile(jsonEmployeeShiftsFile);

		// Act
		var result = employeeshifts.Employees.Where(e => e.Id == id).Select(e => e.Name).FirstOrDefault();

		// Assert
		result.Should().Be(expectedName);
		result.Should().NotBeNull();
	}

	[Fact]
	public async void LoadLocalFile_WhenFileNotExist_ReturnFileNotFoundException()
	{
		// Arrange
		string notExistFile = Path.Combine("./TestData/notExist.json");

		// Act
		Func<Task> act = async () => await _sut.LoadLocalFile(notExistFile);

		// Assert
		await act.Should().ThrowAsync<FileNotFoundException>();
	}

	[Fact]
	public async void LoadLocalFile_WhenEmptyFile_ReturnJsonException()
	{
		// Arrange
		string emptyFile = Path.Combine("./TestData/Empty.json");

		// Act
		Func<Task> act = async () => await _sut.LoadLocalFile(emptyFile);

		// Assert
		await act.Should().ThrowAsync<JsonException>();
	}

	[Fact]
	public async void LoadLocalFile_WhenNotCorrectFile_ReturnInvalidDataException()
	{
		// Arrange
		string jsonShiftFile = Path.Combine("./TestData/Shifts.json");

		// Act
		Func<Task> act = async () => await _sut.LoadLocalFile(jsonShiftFile);

		// Assert
		await act.Should().ThrowAsync<InvalidDataException>();
	}

	[Fact]
	public async void SaveLocalFile_WhenFileWasCreated_NotThrowFileNotFoundException()
	{
		// Arrange
		string createFile = Path.Combine("./TestData/create.json");

		// Act
		Func<Task> act = async () => await _sut.SaveLocalFile(TestEmployeeShifts(), createFile);

		// Assert
		await act.Should().NotThrowAsync<FileNotFoundException>();
	}

	[Fact]
	public async void SaveLocalFile_WhenFileWasNotCreated_ReturnThrowFileNotFoundException()
	{
		// Arrange
		string createFile = Path.Combine("./TestData/");

		// Act
		Func<Task> act = async () => await _sut.SaveLocalFile(new LocalShiftsAndEmployees(new(), new()), createFile);

		// Assert
		await act.Should().ThrowAsync<FileNotFoundException>();
	}

	private LocalShiftsAndEmployees TestEmployeeShifts()
	{
		return new LocalShiftsAndEmployees(
				new List<Employee> { new Employee("Test1", 1) },
				new List<Shift> { new Shift(new Employee("Test1", 1), DateOnly.FromDateTime(DateTime.Now), new TimeOnly(6, 00), new TimeOnly(14, 00), 1) }
			);
	}
}