using ShiftPlan.Api.Extensions;
using ShiftPlan.Api.Repository;
using ShiftPlan.UsersIdentity.Extensions;
using ShiftPlan.UsersIdentity.Models;

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
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add User Identity
builder.Services.AddUserIdentity(postgresConnectionString);

builder.Services.AddSwagger();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapGroup("api/identity")
	.WithTags("Identity")
	.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseCors(origins);

app.UseAuthorization();

app.MapControllers();

// health / isAlive endpoint
app.Map("/health", appBuilder =>
	appBuilder.Run(async context =>
		await context.Response.WriteAsync("Healthy")));

app.Run();


