using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using ShiftPlan.Api.Extensions;
using ShiftPlan.Api.Repository;
using ShiftPlan.UsersIdentity;
using Swashbuckle.AspNetCore.Filters;

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
builder.Services.AddSwaggerGen(options => {
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// health / isAlive endpoint

app.Map("/health", appBuilder => 
	appBuilder.Run(async context => 
		await context.Response.WriteAsync("Healthy")));

app.Run();


