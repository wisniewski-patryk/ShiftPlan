using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShiftPlan.Api.Extensions;
using ShiftPlan.Api.Repository;
using ShiftPlan.UsersIdentity;
using ShiftPlan.UsersIdentity.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS
var origins = "_devOrigin";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: origins,
		policy =>
			policy.WithOrigins("https://localhost:7057")
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials());
});

builder.Services.AddControllers();

var config = builder.Configuration;

// Add Entity Framework
var postgresConnectionString = config.GetConnectionString("PostgresqlConnection") ?? throw new NullReferenceException("ConnectionString is null.");
builder.Services.AddEntityFramework(postgresConnectionString);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add User Identity
builder.Services.AddUserIdentity(postgresConnectionString);

builder.Services.AddSwagger();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(origins);
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapGroup("api/identity")
	.WithTags("Identity")
	.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// health / isAlive endpoint
app.Map("/health", appBuilder =>
	appBuilder.Run(async context =>
		await context.Response.WriteAsync("Healthy")));

// provide an endpoint to clear the cookie for logout
//
// For more information on the logout endpoint and antiforgery, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
app.MapPost("/api/identity/logout", async (SignInManager<User> signInManager, [FromBody] object empty) =>
{
	if (empty is not null)
	{
		await signInManager.SignOutAsync();

		return Results.Ok();
	}

	return Results.Unauthorized();
}).RequireAuthorization();

app.Run();

public partial class Program
{ }
