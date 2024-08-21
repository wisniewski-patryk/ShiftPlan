using ShiftPlan.Blazor.Components;
using ShiftPlan.Blazor.Commons.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.RegisterExternalLibrary();
var backendApiUrl = new Uri(builder.Configuration.GetConnectionString("ShiftPlanBackendApiUrl") ?? throw new Exception("Backend api url not available."));
builder.Services.RegisterHttpClient(backendApiUrl);
builder.Services.RegisterServices();

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
