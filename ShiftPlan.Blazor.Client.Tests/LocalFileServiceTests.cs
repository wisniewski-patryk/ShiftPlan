using FluentAssertions;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;

namespace ShiftPlan.Blazor.Client.Tests
{
    public class LocalFileServiceTests
    {
        private string jsonFile = Path.Combine( Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName,"Employee.json");
        private ILoadSaveService<Employee> _sut { get; }

        public LocalFileServiceTests()
        {
            _sut = new LocalFileService<Employee>();
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