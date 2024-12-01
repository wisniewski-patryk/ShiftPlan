using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShiftPlan.Blazor.Client.Identity;
using ShiftPlan.Blazor.WebAssembly;
using ShiftPlan.Blazor.WebAssembly.Clients;
using ShiftPlan.Blazor.WebAssembly.Identity;
using ShiftPlan.Blazor.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register the cookie handler
builder.Services.AddTransient<CookieHandler>();

// set up authorization
builder.Services.AddAuthorizationCore();

// register the custom state provider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();


// register the account management interface
builder.Services.AddScoped(
	sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());



builder.Services.AddBlazorBootstrap();

builder.Services.AddTransient<IEmployeesClient, EmployeesClient>();
builder.Services.AddTransient<IShiftsClient, ShiftsClient>();

builder.Services.AddTransient<IEmployeesService, EmployeesService>();
builder.Services.AddTransient<IShiftsService, ShiftsService>();


// set base address for default host
builder.Services.AddScoped(sp =>
	new HttpClient { BaseAddress = new Uri("https://localhost:5001/api") });

// configure client for auth interactions
builder.Services.AddHttpClient(
	"Auth",
	opt => opt.BaseAddress = new Uri("https://localhost:5001/api"))
	.AddHttpMessageHandler<CookieHandler>();

// set base address for default host
builder.Services.AddScoped(sp =>
	(HttpClient)sp.GetRequiredService<IHttpClientFactory>().CreateClient("Auth"));

await builder.Build().RunAsync();
