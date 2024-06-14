using ShiftPlan.Api.Extensions;
using ShiftPlan.Api.Repository;

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
builder.Services.AddEntityFramework(config);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/health", appBuilder => 
	appBuilder.Run(async context => 
		await context.Response.WriteAsync("Healthy")));

app.Run();


