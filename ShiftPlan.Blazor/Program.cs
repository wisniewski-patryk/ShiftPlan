using ShiftPlan.Blazor.Client.Clients;
using ShiftPlan.Blazor.Client.Services;
using ShiftPlan.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents()
		.AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazorBootstrap();
var backendApiUrl = new Uri(builder.Configuration.GetConnectionString("ShiftPlanBackendApiUrl") ?? throw new Exception("Backend api url not available."));

builder.Services.AddHttpClient<IEmployeesClient, EmployeesClient>(client => client.BaseAddress = backendApiUrl);

builder.Services.AddHttpClient<IShiftsClient, ShiftsClient>(client => client.BaseAddress = backendApiUrl);

builder.Services.AddTransient<IEmployeesService, EmployeesService>();
builder.Services.AddTransient<IShiftsService, ShiftsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
	app.UseWebAssemblyDebugging();
// }
// else
// {
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
// }

app.UseHttpsRedirection();

app.Map("/health", appBuilder => 
	appBuilder.Run(async context => 
		await context.Response.WriteAsync($"Healthy, {backendApiUrl}")));

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode()
		.AddInteractiveWebAssemblyRenderMode()
		.AddAdditionalAssemblies(typeof(ShiftPlan.Blazor.Client._Imports).Assembly);

app.Run();
