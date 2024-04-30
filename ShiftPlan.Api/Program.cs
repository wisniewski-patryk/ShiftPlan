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

// Add Entity Framework
builder.Services.AddEntityFramework();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


