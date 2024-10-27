using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShiftPlan.Api.Context;
using ShiftPlan.Api.Models;
using ShiftPlan.UsersIdentity.Context;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ShiftPlan.Api.Tests.Controllers;

public class EmployeesControllerTests
{
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
		// Arrange
		var app = CreateApplication();
		using var services = app.Services.CreateScope();
		var ctx = services.ServiceProvider.GetRequiredService<ShiftPlanContext>();
		ctx.Add(new Employee() { Id = 1, Name = "Stefan" });
		ctx.SaveChanges();
		// Act
		var client = app.CreateClient();
		var loginByte = Encoding.ASCII.GetBytes("Test:test");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(loginByte));
		var response = await client.GetAsync("/api/employees/1");
		var employee = await response.Content.ReadFromJsonAsync<Employee>();

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
		employee.Should().NotBeNull();
		employee!.Name.Should().Be("Stefan");
	}

	private WebApplicationFactory<Program> CreateApplication() => new WebApplicationFactory<Program>()
		.WithWebHostBuilder(builder =>
		{
			builder.ConfigureTestServices(services =>
			{
				var shiftPlanOptions = new DbContextOptionsBuilder<ShiftPlanContext>()
					.UseInMemoryDatabase("TestDatabase")
					.Options;
				services.AddSingleton(shiftPlanOptions);
				services.AddSingleton<ShiftPlanContext>();

				var identityUserOptions = new DbContextOptionsBuilder<IdentityUserContext>()
					.UseInMemoryDatabase("TestDatabase")
					.Options;
				services.AddSingleton(identityUserOptions);
				services.AddSingleton<IdentityUserContext>();

				services.AddAuthentication("Basic")
					.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

				services.AddAuthorization(config =>
				{
					config.DefaultPolicy = new AuthorizationPolicyBuilder(config.DefaultPolicy)
						.AddAuthenticationSchemes("Basic")
						.Build();
				});
			});
		});
}

public class BasicAuthenticationHandler(
	IOptionsMonitor<AuthenticationSchemeOptions> options,
	ILoggerFactory logger,
	UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
	protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		if (!this.Request.Headers.ContainsKey("Authorization"))
		{
			return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
		}

		try
		{
			var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers.Authorization.ToString());
			if (authHeader.Scheme != "Basic")
				return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Scheme"));
			if (string.IsNullOrEmpty(authHeader.Parameter))
				return Task.FromResult(AuthenticateResult.Fail("Missing Credentials"));
			var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
			var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
			var username = credentials[0];
			var password = credentials[1];

			if (username == "Test" && password == "test")
			{
				var claims = new[] { new Claim(ClaimTypes.Name, username) };
				var roles = new[] { new IdentityRole("Admin") };
				var identity = new ClaimsIdentity(claims, this.Scheme.Name, "Basic", roles[0].Name);
				var principal = new ClaimsPrincipal(identity);
				var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

				return Task.FromResult(AuthenticateResult.Success(ticket));
			}
			else
			{
				return Task.FromResult(AuthenticateResult.Fail("Invalid Username or Password"));
			}
		}
		catch
		{
			return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
		}
	}
}
