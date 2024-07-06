using Microsoft.AspNetCore.Identity;
using ShiftPlan.Api.Extensions;
using ShiftPlan.Api.Repository;
using ShiftPlan.UsersIdentity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add CORS
var origins = "_devOrigin";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: origins,
		policy =>
		{
			policy.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin();
		});
});

builder.Services.AddControllers();

var config = builder.Configuration;

// Add Entity Framework
var postgresConnectionString = config.GetConnectionString("PostgresqlConnection") ?? throw new NullReferenceException("ConnectionString is null.");
builder.Services.AddEntityFramework(postgresConnectionString);
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

// Add User Identity
builder.Services.AddUserIdentity(postgresConnectionString);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<IdentityUser>();

// health / isAlive endpoint

app.Map("/health", appBuilder => 
	appBuilder.Run(async context => 
		await context.Response.WriteAsync("Healthy")));

app.Run();


