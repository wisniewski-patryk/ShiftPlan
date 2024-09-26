using ShiftPlan.Blazor.Commons.Extensions;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;
using ShiftPlan.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

IReadService<Employee> test = new ServiceJson<Employee>();
var jsonFile = Path.Combine(Path.GetTempPath(), "test1.json");

var employee = new Employee("test1", 1);
test.SerializeFileSingle(employee, jsonFile);

var employees = new List<Employee>();
employees.Add(employee);
employees.Add(new Employee("test2", 2));
test.SerializeFileAsList(employees, Path.Combine(Path.GetTempPath(), "test2.json"));

var name = test.DeserializeFileSingle(jsonFile).Name;

test.DeserializeFileAsList(Path.Combine(Path.GetTempPath(), "test2.json")).ToList().ForEach(x => Console.WriteLine(x.Name));



// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(ShiftPlan.Blazor.Client._Imports).Assembly);

app.Run();
