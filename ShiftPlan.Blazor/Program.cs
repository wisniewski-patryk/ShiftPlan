using ShiftPlan.Blazor.Commons.Extensions;
using ShiftPlan.Blazor.Commons.Models;
using ShiftPlan.Blazor.Commons.Services;
using ShiftPlan.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

//test reading and saving jsonfile to temp folder
var jsonFileSingle = Path.Combine(Path.GetTempPath(), "test1.json");
var jsonFileAsList = Path.Combine(Path.GetTempPath(), "test2.json");
IReadService<Employee> test = new ServiceJson<Employee>();
var employee = new Employee("test1", 1);
test.SerializeFileSingle(employee, jsonFileSingle);

var employees = new List<Employee>();
employees.Add(employee);
employees.Add(new Employee("test2", 2));
test.SerializeFileAsList(employees, jsonFileAsList);

var name = test.DeserializeFileSingle(jsonFileSingle).Name;

test.DeserializeFileAsList(jsonFileAsList).ToList().ForEach(x => Console.WriteLine(x.Name));



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
