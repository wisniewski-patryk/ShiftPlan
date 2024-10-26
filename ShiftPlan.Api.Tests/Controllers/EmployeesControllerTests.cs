using FluentAssertions;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftPlan.Api.Context;
using ShiftPlan.Api.Models;
using ShiftPlan.Api.Repository;
using ShiftPlan.UsersIdentity.Context;
using ShiftPlan.UsersIdentity.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ShiftPlan.Api.Tests.Controllers;

public class EmployeesControllerTests
{
	private readonly IRepository<Employee> employeesRepository = NSubstitute.Substitute.For<IRepository<Employee>>();

	[Fact]
	public async Task GET_return_200_On_Employee_GetAll_Endpoint()
	{
		var application = CreateApplication();

		var client = application.CreateClient();
		var response = await client.GetAsync("/api/employees");

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task GET_One_Employee_return_401()
	{
		var app = CreateApplication();
		var client = app.CreateClient();
		var response = await client.GetAsync("/api/employees/1");

		response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
	}


	[Fact]
	public async Task GET_One_Employee_return_200()
	{
		var app = CreateApplication();
		var l = app.Services;
		var client = app.CreateClient();
		//var loginData = await client.PostAsync("/api/identity/login", JsonContent.Create(new UserLoginRequest("test@mail.com","password")));
		//var s  = await loginData.Content.ReadAsStringAsync();
		//var token = (await loginData.Content.ReadFromJsonAsync<UserLoginRespond>()).AccessToken;
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "asdasdasd");
		var response = await client.GetAsync("/api/employees/1");

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	record UserLoginRequest(string Email, string Password);
	record UserLoginRespond(string TokenType, string AccessToken, string RefreshToken, int ExpiresIn);

	private WebApplicationFactory<Program> CreateApplication() => new WebApplicationFactory<Program>()
		.WithWebHostBuilder(builder =>
		{
			builder.ConfigureTestServices(services =>
			{
				string connectionString = "Host=db;Port=54321;Database=ShiftPlan;Username=postgres;Password=dev_password";
				var shiftPlanOptions = new DbContextOptionsBuilder<ShiftPlanContext>()
					.UseNpgsql(connectionString)
					.Options;
				services.AddSingleton(shiftPlanOptions);
				services.AddSingleton<ShiftPlanContext>();
				services.AddSingleton(this.employeesRepository);

				var identityUserOptions = new DbContextOptionsBuilder<IdentityUserContext>()
					.UseNpgsql(connectionString)
					.Options;
				services.AddSingleton(identityUserOptions);
				services.AddSingleton<IdentityUserContext>();

				services.AddAuthentication();
				services.AddIdentityApiEndpoints<User>()
					.AddRoles<IdentityRole>()
					.AddEntityFrameworkStores<IdentityUserContext>().AddDefaultTokenProviders();
				services.AddAuthorization();
			});
		});
}
